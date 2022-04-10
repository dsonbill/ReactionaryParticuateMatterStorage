using System;
using System.Text;
using UnityEngine;

namespace UniversalMachine
{
    public class Particle : MonoBehaviour
    {
        public static float Nearest = 1.4013e-25f;

        public class Folding
        {
            public Vector3 Energy;
            public Vector3 Position;
            public Vector3 Force;
            public Vector3 Torque;
            public float Dimensionality;
        }

        public enum StateFocus
        {
            Energy = 1,
            Position = -1
        }

        public enum FoldTarget
        {
            Energy,
            Position,
            Force,
            Torque,
            Unknown
        }


        public Vector4 Energy = new Vector4(0,0,0, 0);
        public Vector4 Position = new Vector4(0,0,0, 0);

        Vector4 Force = new Vector4(0,0,0, 0);
        Vector4 Torque = new Vector4(0,0,0, 0);

        //Energetic, Solid, Spacial, Mechanical
        public Vector4 Descriptor = new Vector4();

        public float Age { get; private set; }

        public int SlamEvents = 0;

        public double SlamConsideration;

        public double ContactDepth;

        public Func<double, double> PrimaryReduction
        {
            get
            {
                return new Func<double, double>((regionalArea) =>
                 {
                     double syphon = 1 / Energy.magnitude * ContactDepth / regionalArea;
                     Energy = new Vector4(Energy.x, Energy.y, Energy.z, Energy.w > 2 ? 2 : Energy.w + (float)syphon);

                     return 1 / Energy.magnitude * ContactDepth * regionalArea - SlamConsideration;
                 });
            }
        }

        public Func<double, double> SecondaryReduction
        {
            get
            {
                return new Func<double, double>((regionalArea) =>
                {
                    double syphon = 1 / Age * ContactDepth * regionalArea;
                    Energy = new Vector4(Energy.x, Energy.y, Energy.z, Energy.w > 2 ? 2 : Energy.w + (float)syphon);

                    return 1 / Energy.magnitude * ContactDepth / regionalArea - SlamConsideration;
                });
            }
        }

        public Func<double, double> TertiaryReduction
        {
            get
            {
                return new Func<double, double>((regionalArea) =>
                {
                    double fAvg = (Force.x + Force.y + Force.z) / 3;
                    double syphon = 1 /  * ContactDepth * regionalArea;
                    Energy = new Vector4(Energy.x, Energy.y, Energy.z, Energy.w > 2 ? 2 : Energy.w + (float)syphon);

                    return 1 / Energy.magnitude * ContactDepth / regionalArea - SlamConsideration;
                });
            }
        }

        public StateFocus Focus = StateFocus.Energy;

        public Func<Vector3, Vector3, Vector3, Vector3> Project;

        System.Random r = new System.Random();

        public float GetDiscernmentRatio(float discernment, float deltaTime)
        {
            if (float.IsNaN(discernment) || discernment == 0)
            {
                //Debug.Log("Lowered Dimensional Number: " + 0);
                return 1 * deltaTime;
            }

            return 1 / discernment * deltaTime;
        }

        public void AddForce(Vector3 f, Vector3 point, float deltaTime)
        {
            //StringBuilder report = new StringBuilder();
            //report.AppendLine("[Force Report]");
            //report.AppendLine();

            //report.AppendLine("Point Energy: " + PointEnergy(deltaTime));
            //report.AppendLine();

            Vector3 contactForce;
            float energyMagnitude = PointEnergy(deltaTime).magnitude;

            if (energyMagnitude == 0)
            {
                contactForce = f;
            }
            else
            {
                contactForce = f / energyMagnitude;
            }

            //report.AppendLine("Contact Force: " + contactForce);
            //report.AppendLine();

            float totalDiscernment = Position.w * Energy.w * Mathf.Pow(Torque.w, 2) * Mathf.Sqrt(Force.w);
            float discernmentRatio = GetDiscernmentRatio(totalDiscernment, deltaTime);
            //float discernmentRatio = 1 / totalIndiscernment;
            //float currentDiscernment = discernmentRatio * deltaTime;

            //report.AppendLine("Total Indiscernment:" + totalIndiscernment);
            //report.AppendLine("Discernment Ratio: " + discernmentRatio);
            //report.AppendLine("Current Discernment: " + currentDiscernment);
            //report.AppendLine();

            Vector3 pf = PointForce(deltaTime);
            Vector4 forceDiscerned = new Vector4(pf.x, pf.y, pf.z, 0.5f);
            Force = forceDiscerned + new Vector4(contactForce.x, contactForce.y, contactForce.z, 0.5f);

            //report.AppendLine("Force Discerned: " + forceDiscerned);
            //report.AppendLine("Force: " + Force);
            //report.AppendLine();

            Vector3 pt = PointTorque(deltaTime);
            Vector4 torqueDiscerned = new Vector4(pt.x, pt.y, pt.z, 0.5f);
            Torque = torqueDiscerned + Cross(new Vector4(point.x, point.y, point.z, 1), new Vector4(f.x, f.y, f.z, 0.5f));

            //report.AppendLine("Torque Discerned: " + torqueDiscerned);
            //report.AppendLine("Torque: " + Torque);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            //Debug.Log(report.ToString());

            IndiscernPositioning(discernmentRatio, deltaTime);
        }

        public void IndiscernPositioning(float ratio, float deltaTime)
        {
            float x = r.Next(-1, 1) * (float)r.NextDouble() * Position.x * ratio;
            float y = r.Next(-1, 1) * (float)r.NextDouble() * Position.y * ratio;
            float z = r.Next(-1, 1) * (float)r.NextDouble() * Position.z * ratio;

            Vector3 position = new Vector3(Position.x + x, Position.y + y, Position.z + z);

            float discernment = Mathf.Sqrt(ratio / deltaTime);
            float positionDimensionNumber = Position.w - discernment > 0 ? Position.w - discernment : 0;
            float energyDimensionNumber = Energy.w - discernment > 0 ? Energy.w - discernment : 0;

            Energy.w = energyDimensionNumber;
            Position = new Vector4(position.x, position.y, position.z, positionDimensionNumber);
        }

        public Vector4 Cross(Vector4 vector1, Vector4 vector2)
        {
            return new Vector4(
                vector1.y * vector2.z - vector1.z * vector2.y,
                vector1.z * vector2.x - vector1.x * vector2.z,
                vector1.x * vector2.y - vector1.y * vector2.x,
                vector1.w * vector2.w);
        }

        public float GetLoweredDimensionalNumber(Vector4 detail, float deltaTime)
        {
            float theTime = 1 / 0.01f * deltaTime;
            float number = 1 / detail.w * theTime;

            if (float.IsNaN(number))
            {
                //Debug.Log("Lowered Dimensional Number: " + 0);
                return 0;
            }
            else if (float.IsInfinity(number))
            {
                //Debug.Log("Lowered Dimensional Number: " + 1);
                return 1;
            }
            
            return number;
        }

        public float GetDiscernmentNumber(float loweredDimensionalNumber)
        {
            //return Mathf.Sin(Age) * loweredDimensionalNumber;
            return loweredDimensionalNumber;
        }
        
        public Vector3 PointPosition(float deltaTime)
        {
            float t = GetLoweredDimensionalNumber(Position, deltaTime);
            Vector3 pos = new Vector3(Position.x, Position.y, Position.z);
            return pos - new Vector3(Position.x * GetDiscernmentNumber(t),
                Position.y * GetDiscernmentNumber(t),
                Position.z * GetDiscernmentNumber(t));
        }

        public Vector3 PointEnergy(float deltaTime)
        {
            float t = GetLoweredDimensionalNumber(Energy, deltaTime);
            Vector3 poe = new Vector3(Energy.x, Energy.y, Energy.z);
            return poe - new Vector3(Energy.x * GetDiscernmentNumber(t),
                Energy.y * GetDiscernmentNumber(t),
                Energy.z * GetDiscernmentNumber(t));
        }

        public Vector3 PointForce(float deltaTime)
        {
            float t = GetLoweredDimensionalNumber(Force, deltaTime);
            Vector3 foe = new Vector3(Force.x, Force.y, Force.z);
            return foe - new Vector3(Force.x * GetDiscernmentNumber(t),
                Force.y * GetDiscernmentNumber(t),
                Force.z * GetDiscernmentNumber(t));
        }

        public Vector3 PointTorque(float deltaTime)
        {
            float t = GetLoweredDimensionalNumber(Torque, deltaTime);
            Vector3 torq = new Vector3(Torque.x, Torque.y, Torque.z);
            return torq - new Vector3(Torque.x * GetDiscernmentNumber(t),
                Torque.y * GetDiscernmentNumber(t),
                Torque.z * GetDiscernmentNumber(t));
        }

        public Vector3 TotalEnergy()
        {
            return new Vector3(Energy.x * Energy.w, Energy.y * Energy.w, Energy.z * Energy.w);
        }

        public Vector3 TotalForce()
        {
            return new Vector3(Force.x * Force.w, Force.y * Force.w, Force.z * Force.w);
        }

        public Vector3 TotalTorque()
        {
            return new Vector3(Torque.x * Torque.w, Torque.y * Torque.w, Torque.z * Torque.w);
        }

        public void Simulate(float deltaTime)
        {
            //Debug.Log("A: " + Position + " | " + Energy + " | " + Force + " | " + Torque);
            //float forceOffset = 1 / Force.w * deltaTime;
            //float torqueOffset = 1 / Torque.w  * deltaTime;

            float totalDiscernment = Force.w * Torque.w;
            float discernmentRatio = GetDiscernmentRatio(totalDiscernment, deltaTime);

            Vector3 force = new Vector3((Force.x * discernmentRatio),
                (Force.y * discernmentRatio),
                (Force.z * discernmentRatio));

            Vector3 torque = new Vector3((Torque.x * discernmentRatio),
                (Torque.y * discernmentRatio),
                (Torque.z * discernmentRatio));

            Vector3 pPos = PointPosition(deltaTime);
            //Debug.Log("A.0: " + pPos);

            Vector3 pos = new Vector3(pPos.x + force.x * torque.x, pPos.y + force.y * torque.y, pPos.z + force.z * torque.z);
            //Debug.Log("A.1: " + pos);
            Vector3 projectedPos = Project(pPos, pos, PointEnergy(deltaTime));

            Position = new Vector4(projectedPos.x, projectedPos.y, projectedPos.z, 1);
            Energy = new Vector4(Energy.x, Energy.y, Energy.z, 1);

            IndiscernForceAndTorque(discernmentRatio, deltaTime);

            //Debug.Log("B: " + Position + " | " + Energy + " | " + Force + " | " + Torque);
        }

        public void IndiscernForceAndTorque(float ratio, float deltaTime)
        {
            float x = r.Next(-1, 1) * (float)r.NextDouble() * Force.x * ratio;
            float y = r.Next(-1, 1) * (float)r.NextDouble() * Force.y * ratio;
            float z = r.Next(-1, 1) * (float)r.NextDouble() * Force.z * ratio;

            Vector3 force = new Vector3(Force.x + x, Force.y + y, Force.z + z);

            x = r.Next(-1, 1) * (float)r.NextDouble() * Torque.x * ratio;
            y = r.Next(-1, 1) * (float)r.NextDouble() * Torque.y * ratio;
            z = r.Next(-1, 1) * (float)r.NextDouble() * Torque.z * ratio;

            Vector3 torque = new Vector3(Torque.x + x, Torque.y + y, Torque.z + z);

            float discernment = Mathf.Sqrt(ratio / deltaTime);
            float forceDimensionNumber = Force.w - discernment > 0 ? Force.w - discernment : 0;
            float torqueDimensionNumber = Torque.w - discernment > 0 ? Torque.w - discernment : 0;

            Force = new Vector4(force.x, force.y, force.z, forceDimensionNumber);
            Torque = new Vector4(torque.x, torque.y, torque.z, torqueDimensionNumber);
        }
        
        public void Fold(FoldTarget target)
        {
            switch (target)
            {
                case FoldTarget.Energy:
                    Energize();
                    break;
                case FoldTarget.Position:
                    Particulate();
                    break;
                case FoldTarget.Torque:
                    Spin();
                    break;
                case FoldTarget.Force:
                    Move();
                    break;
                case FoldTarget.Unknown:
                    RPFS(Time.deltaTime);
                    break;
            }
        }

        public Folding Postulate()
        {
            float Dimensionality = Energy.w + Position.w + Torque.w + Force.w;

            Vector3 ener = new Vector3(Energy.x * Energy.w, Energy.y * Energy.w, Energy.z * Energy.w);
            Energy = new Vector4();

            Vector3 pos = new Vector3(Position.x * Position.w, Position.y * Position.w, Position.z * Position.w);
            Position = new Vector4();

            Vector3 tor = new Vector3(Torque.x * Torque.w, Torque.y * Torque.w, Torque.z * Torque.w);
            Torque = new Vector4();

            Vector3 four = new Vector3(Force.x * Force.w, Force.y * Force.w, Force.z * Force.w);
            Force = new Vector4();

            Folding information = new Folding();
            information.Energy = ener;
            information.Position = pos;
            information.Torque = tor;
            information.Force = four;
            
            return information;
        }

        public Vector4 Energize()
        {
            float dimensionality = Position.w + Torque.w + Force.w;

            Vector3 pos = new Vector3(Position.x * Position.w, Position.y * Position.w, Position.z * Position.w);
            Position = new Vector4();

            Vector3 tor = new Vector3(Torque.x * Torque.w, Torque.y * Torque.w, Torque.z * Torque.w);
            Torque = new Vector4();

            Vector3 four = new Vector3(Force.x * Force.w, Force.y * Force.w, Force.z * Force.w);
            Force = new Vector4();

            Vector4 specifics =  pos + tor + four;
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 Particulate()
        {
            float dimensionality = Energy.w + Torque.w + Force.w;

            Vector3 ener = new Vector3(Energy.x * Energy.w, Energy.y * Energy.w, Energy.z * Energy.w);
            Energy = new Vector4();

            Vector3 tor = new Vector3(Torque.x * Torque.w, Torque.y * Torque.w, Torque.z * Torque.w);
            Torque = new Vector4();

            Vector3 four = new Vector3(Force.x * Force.w, Force.y * Force.w, Force.z * Force.w);
            Force = new Vector4();

            Vector4 specifics = ener + tor + four;
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 Spin()
        {
            float dimensionality = Energy.w + Position.w + Force.w;

            Vector3 ener = new Vector3(Energy.x * Energy.w, Energy.y * Energy.w, Energy.z * Energy.w);
            Energy = new Vector4();

            Vector3 pos = new Vector3(Position.x * Position.w, Position.y * Position.w, Position.z * Position.w);
            Position = new Vector4();

            Vector3 four = new Vector3(Force.x * Force.w, Force.y * Force.w, Force.z * Force.w);
            Force = new Vector4();

            Vector4 specifics = ener + pos + four;
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 Move()
        {
            float dimensionality = Energy.w + Position.w + Torque.w;

            Vector3 ener = new Vector3(Energy.x * Energy.w, Energy.y * Energy.w, Energy.z * Energy.w);
            Energy = new Vector4();

            Vector3 pos = new Vector3(Position.x * Position.w, Position.y * Position.w, Position.z * Position.w);
            Position = new Vector4();

            Vector3 tor = new Vector3(Torque.x * Torque.w, Torque.y * Torque.w, Torque.z * Torque.w);
            Torque = new Vector4();

            Vector4 specifics = ener + pos + tor;
            specifics.w = dimensionality;
            return specifics;
        }

        //ReinitializationalParamaterlessFunctionalitySystem
        //A.K.A. Role-Playing File System
        //A.K.A. Regional Protection Forwarding Service
        //A.K.A. Relentless Persona Friction State
        //A.K.A. Only William Really Knows What It Does! Till Now :)
        public float RPFS(float deltaTime)
        {
            float dimensionality = Energy.w + Position.w + Torque.w + Force.w;

            float overload = Mathf.Pow(Energy.x, deltaTime);
            overload += Mathf.Pow(Energy.y, deltaTime);
            overload += Mathf.Pow(Energy.z, deltaTime);

            overload = Mathf.Pow(Position.x, deltaTime);
            overload += Mathf.Pow(Position.y, deltaTime);
            overload += Mathf.Pow(Position.z, deltaTime);

            overload = Mathf.Pow(Torque.x, deltaTime);
            overload += Mathf.Pow(Torque.y, deltaTime);
            overload += Mathf.Pow(Torque.z, deltaTime);

            overload = Mathf.Pow(Force.x, deltaTime);
            overload += Mathf.Pow(Force.y, deltaTime);
            overload += Mathf.Pow(Force.z, deltaTime);

            overload *= Mathf.Pow(deltaTime, 3);
            overload *= Mathf.Pow(dimensionality, 5);

            overload *= Mathf.Pow(1, deltaTime);

            return overload;
        }

        public void Advance(float deltaTime)
        {
            float energySolve = (int)Focus / Energy.w * deltaTime;
            Energy = new Vector4(Energy.x * energySolve, Energy.y * energySolve, Energy.z * energySolve, Energy.w - energySolve);

            float positionSolve = -(int)Focus / Position.w * deltaTime;
            Position = new Vector4(Position.x * positionSolve, Position.y * positionSolve, Position.z * positionSolve, Position.w - positionSolve);

            float forceSolve = (int)Focus / Force.w * deltaTime;
            Force = new Vector4(Force.x * forceSolve, Force.y * forceSolve, Force.z * forceSolve, Force.w - forceSolve);

            float torqueSolve = -(int)Focus / Torque.w * deltaTime;
            Torque = new Vector4(Torque.x * torqueSolve, Torque.y * torqueSolve, Torque.z * torqueSolve, Torque.w - torqueSolve);
        }

        public void Reduce(float delta, Func<double, double, double> algorithmic, Func<Particle> stream)
        {
            
        }

        public void SetPosition(float deltaTime)
        {
            transform.position = PointPosition(deltaTime);
        }

        

        float pause;

        // Update is called once per frame
        void FixedUpdate()
        {

            //if (pause < 1f)
            //{
            //    pause += Time.deltaTime;
            //    return;
            //}
            //pause = 0f;

            Age += Time.deltaTime;

            Simulate(Time.deltaTime);
            //Advance(Time.deltaTime);

            //SetPosition(Time.deltaTime);
        }
    }
}