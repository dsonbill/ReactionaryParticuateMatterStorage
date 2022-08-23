using System;
using UnityEngine;

namespace UniversalMachine
{
    public class Particle : MonoBehaviour
    {
        public static float Nearest = 1.4013e-25f;
        public static float EnergeticResistance = 10f;
        public static float DiscernabilityRestraint = 10f;
        public static float KineticEasing = 100f;
        public static float ContactWindow = 0.01f;

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

        public enum EffectorFolding
        {
            Energy,
            Position,
            Force,
            Torque,
            NonSystematic
        }


        public Vector4 Ascription = new Vector4(0,0,0, 0);
        public Vector4 Attunement = new Vector4(0,0,0, 0);

        Vector4 Conductance = new Vector4(0,0,0, 0);
        Vector4 Assertation = new Vector4(0,0,0, 0);

        //Energetic, Solid, Spacial, Mechanical
        public Vector4 Descriptor = new Vector4();

        public float Age { get; private set; }

        public int SlamEvents = 0;

        public double SlamConsideration;

        public float ContactDepth;

        public delegate void OnDestroyAction();
        public OnDestroyAction onDestroy;

        public Func<double, double> PrimaryReduction
        {
            get
            {
                return new Func<double, double>((regionalArea) =>
                 {
                     double syphon = 1 / ((Vector3)Ascription).magnitude * (ContactDepth / regionalArea);

                     Ascription = new Vector4(
                         Ascription.x - Ascription.x * (float)syphon,
                         Ascription.y - Ascription.y * (float)syphon,
                         Ascription.z - Ascription.z * (float)syphon,
                         Ascription.w - Ascription.w * (float)syphon < 0 ? 0 : Ascription.w - Ascription.w * (float)syphon);

                     return syphon * ContactWindow;
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
                    
                    Ascription = new Vector4(
                        Ascription.x - Ascription.x * (float)syphon,
                        Ascription.y - Ascription.y * (float)syphon,
                        Ascription.z - Ascription.z * (float)syphon,
                        Ascription.w - Ascription.w * (float)syphon < 0 ? 0 : Ascription.w - Ascription.w * (float)syphon);
                    
                    return syphon * ContactWindow;
                });
            }
        }

        public Func<double, double> TertiaryReduction
        {
            get
            {
                return new Func<double, double>((regionalArea) =>
                {
                    double fAvg = (Conductance.x + Conductance.y + Conductance.z) / 3;
                    double syphon = 1 / fAvg * ContactDepth / regionalArea;

                    Ascription = new Vector4(
                        Ascription.x - Ascription.x * (float)syphon,
                        Ascription.y - Ascription.y * (float)syphon,
                        Ascription.z - Ascription.z * (float)syphon,
                        Ascription.w - Ascription.w * (float)syphon < 0 ? 0 : Ascription.w - Ascription.w * (float)syphon);

                    return syphon * ContactWindow;
                });
            }
        }

        public Func<Vector3, Vector3, Vector3, Vector3> Project;

        System.Random r = new System.Random();

        public float GetDiscernmentRatio(float discernment, float deltaTime)
        {
            if (float.IsNaN(discernment) || discernment == 0)
            {
                //Debug.Log("Lowered Dimensional Number: " + 0);
                return deltaTime;
            }

            return discernment * deltaTime;
        }

        public void AddForce(Vector3 f, Vector3 point, float deltaTime)
        {
            Vector3 contactForce;
            float energyMagnitude = PointEnergy(deltaTime).magnitude;

            if (energyMagnitude == 0)
            {
                contactForce = f / KineticEasing * ContactDepth;
            }
            else
            {
                contactForce = f / (KineticEasing * (energyMagnitude / EnergeticResistance)) * ContactDepth;
            }
 

            float totalDiscernment = Attunement.w * Ascription.w * Mathf.Pow(Assertation.w, 2) * Mathf.Pow(Conductance.w, 3);
            float discernmentRatio = GetDiscernmentRatio(totalDiscernment, deltaTime);


            Vector3 pf = PointForce(deltaTime);
            Conductance = new Vector4(
                pf.x + contactForce.x,
                pf.y + contactForce.y,
                pf.z + contactForce.z,
                1f);

            Vector3 pt = PointTorque(deltaTime);
            Assertation = new Vector4(
                pt.x + point.x,
                pt.y + point.y,
                pt.z + point.z,
                1f);
        }

        

        public Vector4 Cross(Vector4 vector1, Vector4 vector2)
        {
            return new Vector4(
                vector1.y * vector2.z - vector1.z * vector2.y,
                vector1.z * vector2.x - vector1.x * vector2.z,
                vector1.x * vector2.y - vector1.y * vector2.x,
                vector1.w * vector2.w);
        }

        public Vector3 Mul(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                vector1.x * vector2.x,
                vector1.y * vector2.y,
                vector1.z * vector2.z
                );
        }

        public float GetDiscernmentDelta(Vector4 detail, float deltaTime)
        {
            float discernment = 1 / detail.w * deltaTime;

            if (float.IsNaN(discernment))
            {
                //Debug.Log("Lowered Dimensional Number: " + 0);
                return 0;
            }
            else if (float.IsInfinity(discernment))
            {
                //Debug.Log("Lowered Dimensional Number: " + 1);
                return 1;
            }

            return discernment / DiscernabilityRestraint;
        }
        
        public Vector3 PointPosition(float deltaTime)
        {
            float t = GetDiscernmentDelta(Attunement, deltaTime);
            Vector3 pPosition =  new Vector3(
                Attunement.x * t,
                Attunement.y * t,
                Attunement.z * t
                );
            return pPosition;
        }

        public Vector3 PointEnergy(float deltaTime)
        {
            float t = GetDiscernmentDelta(Ascription, deltaTime);
            Vector3 pEnergy = new Vector3(
                Ascription.x * t,
                Ascription.y * t,
                Ascription.z * t
                );
            return pEnergy;
        }

        public Vector3 PointForce(float deltaTime)
        {
            float t = GetDiscernmentDelta(Conductance, deltaTime);
            Vector3 pForce = new Vector3(
                Conductance.x * t,
                Conductance.y * t,
                Conductance.z * t
                );
            return pForce;
        }

        public Vector3 PointTorque(float deltaTime)
        {
            float t = GetDiscernmentDelta(Assertation, deltaTime);
            Vector3 pTorque = new Vector3(
                Assertation.x * t,
                Assertation.y * t,
                Assertation.z * t
                );
            return pTorque;
        }

        public Vector3 TotalEnergy()
        {
            return new Vector3(Ascription.x * Ascription.w, Ascription.y * Ascription.w, Ascription.z * Ascription.w);
        }

        public Vector3 TotalForce()
        {
            return new Vector3(Conductance.x * Conductance.w, Conductance.y * Conductance.w, Conductance.z * Conductance.w);
        }

        public Vector3 TotalTorque()
        {
            return new Vector3(Assertation.x * Assertation.w, Assertation.y * Assertation.w, Assertation.z * Assertation.w);
        }

        public void Simulate(float deltaTime)
        {
            //Debug.Log("A: " + Position + " | " + Energy + " | " + Force + " | " + Torque);
            //float forceOffset = 1 / Force.w * deltaTime;
            //float torqueOffset = 1 / Torque.w  * deltaTime;

            float totalDiscernment = Conductance.w * Assertation.w;
            float discernmentRatio = GetDiscernmentRatio(totalDiscernment, deltaTime);

            Vector3 pPos = PointPosition(deltaTime);
            Vector3 pEnergy = PointEnergy(deltaTime);
            Vector3 pTorque = PointTorque(deltaTime);
            Vector3 pForce = PointForce(deltaTime);

            Vector3 pos = new Vector3(
                pPos.x + ((pForce.x * pTorque.x)), // / pEnergy.x),
                pPos.y + ((pForce.y * pTorque.y)), // / pEnergy.y),
                pPos.z + ((pForce.z * pTorque.z)) // / pEnergy.z)
                );

            Vector3 projectedPos = Project(pPos, pos, pEnergy);

            float discrnUp = deltaTime / pPos.magnitude * projectedPos.magnitude;

            IndiscernProperty(
                pPos,
                new Vector4(projectedPos.x, projectedPos.y, projectedPos.z, discrnUp),
                deltaTime,
                (v) => { Attunement = v; },
                () => { return Attunement; });

            IndiscernProperty(
                pForce,
                Vector4.zero,
                deltaTime,
                (v) => { Conductance = v; },
                () => { return Conductance; });


            IndiscernProperty(
                pTorque,
                Vector4.zero,
                deltaTime,
                (v) => { Assertation = v; },
                () => { return Assertation; });

        }

        float Indiscern(float range)
        {
            return r.Next(-1, 1) * (float)r.NextDouble() * range;
        }

        public void IndiscernProperty(Vector3 discernment, Vector4 resultant, float deltaTime, Action<Vector4> record, Func<Vector4> describe)
        {
            Vector3 contained = ((Vector3)describe()) - discernment;

            float resultantMagnitude = ((Vector3)resultant).magnitude;

            Vector3 indiscernable = new Vector3(
                Indiscern(resultantMagnitude),
                Indiscern(resultantMagnitude),
                Indiscern(resultantMagnitude));

            float indiscernment = Vector3.Distance(discernment, indiscernable);

            float dimensionality = 1 / (describe().w - resultant.w) * indiscernment;

            record(new Vector4(
                contained.x + indiscernable.x,
                contained.y + indiscernable.y,
                contained.z + indiscernable.z,
                describe().w - resultant.w + dimensionality));
        }
        
        public void Fold(EffectorFolding target, Vector3 foldingDelta)
        {
            switch (target)
            {
                case EffectorFolding.Energy:
                    Ascription = EnergyEffector(foldingDelta);
                    break;
                case EffectorFolding.Position:
                    Attunement = PositionEffector();
                    break;
                case EffectorFolding.Torque:
                    Assertation = AngularEffector();
                    break;
                case EffectorFolding.Force:
                    Conductance = ForceEffector();
                    break;
                case EffectorFolding.NonSystematic:
                    Expostulate(Time.deltaTime);
                    break;
            }
        }

        public Folding Postulate()
        {
            float Dimensionality = Ascription.w + Attunement.w + Assertation.w + Conductance.w;

            Vector3 ener = new Vector3(Ascription.x * Ascription.w, Ascription.y * Ascription.w, Ascription.z * Ascription.w);
            Ascription = new Vector4();

            Vector3 pos = new Vector3(Attunement.x * Attunement.w, Attunement.y * Attunement.w, Attunement.z * Attunement.w);
            Attunement = new Vector4();

            Vector3 tor = new Vector3(Assertation.x * Assertation.w, Assertation.y * Assertation.w, Assertation.z * Assertation.w);
            Assertation = new Vector4();

            Vector3 four = new Vector3(Conductance.x * Conductance.w, Conductance.y * Conductance.w, Conductance.z * Conductance.w);
            Conductance = new Vector4();

            Folding information = new Folding();
            information.Energy = ener;
            information.Position = pos;
            information.Torque = tor;
            information.Force = four;
            
            return information;
        }

        public Vector4 EnergyEffector(Vector3 effectorDelta)
        {
            float dimensionality = (Attunement.w + Assertation.w + Conductance.w) / 3 * effectorDelta.magnitude;

            Vector3 pos = new Vector3(
                Attunement.x * Attunement.w * effectorDelta.x,
                Attunement.y * Attunement.w * effectorDelta.y,
                Attunement.z * Attunement.w * effectorDelta.z
                );

            float posDisc = Attunement.w / ((Vector3)Attunement).magnitude * pos.magnitude;
            Attunement = Attunement - new Vector4(pos.x, pos.y, pos.z, posDisc);

            Vector3 tor = new Vector3(
                Assertation.x * Assertation.w * effectorDelta.x,
                Assertation.y * Assertation.w * effectorDelta.y,
                Assertation.z * Assertation.w * effectorDelta.z
                );

            float torDisc = Assertation.w / ((Vector3)Assertation).magnitude * tor.magnitude;
            Assertation = Assertation - new Vector4(tor.x, tor.y, tor.z, torDisc);

            Vector3 forc = new Vector3(
                Conductance.x * Conductance.w * effectorDelta.x,
                Conductance.y * Conductance.w * effectorDelta.y,
                Conductance.z * Conductance.w * effectorDelta.z
                );

            float forDisc = Conductance.w / ((Vector3)Conductance).magnitude * forc.magnitude;
            Conductance = Conductance - new Vector4(forc.x, forc.y, forc.z, forDisc);

            Vector4 specifics =  pos + (Mul(tor, forc));
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 PositionEffector()
        {
            float dimensionality = Ascription.w + Assertation.w + Conductance.w;

            Vector3 ener = new Vector3(Ascription.x * Ascription.w, Ascription.y * Ascription.w, Ascription.z * Ascription.w);
            Ascription = new Vector4();

            Vector3 tor = new Vector3(Assertation.x * Assertation.w, Assertation.y * Assertation.w, Assertation.z * Assertation.w);
            Assertation = new Vector4();

            Vector3 four = new Vector3(Conductance.x * Conductance.w, Conductance.y * Conductance.w, Conductance.z * Conductance.w);
            Conductance = new Vector4();

            Vector4 specifics = tor + four;
            specifics = new Vector4(specifics.x * ener.x, specifics.y * ener.y, specifics.z * ener.z);
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 AngularEffector()
        {
            float dimensionality = Ascription.w + Attunement.w + Conductance.w;

            Vector3 ener = new Vector3(Ascription.x * Ascription.w, Ascription.y * Ascription.w, Ascription.z * Ascription.w);
            Ascription = new Vector4();

            Vector3 pos = new Vector3(Attunement.x * Attunement.w, Attunement.y * Attunement.w, Attunement.z * Attunement.w);
            Attunement = new Vector4();

            Vector3 four = new Vector3(Conductance.x * Conductance.w, Conductance.y * Conductance.w, Conductance.z * Conductance.w);
            Conductance = new Vector4();

            Vector4 specifics = new Vector3(ener.x * four.x, ener.y * four.y, ener.z * four.z) + pos;
            specifics.w = dimensionality;
            return specifics;
        }

        public Vector4 ForceEffector()
        {
            float dimensionality = Ascription.w + Attunement.w + Assertation.w;

            Vector3 ener = new Vector3(Ascription.x * Ascription.w, Ascription.y * Ascription.w, Ascription.z * Ascription.w);
            Ascription = new Vector4();

            Vector3 pos = new Vector3(Attunement.x * Attunement.w, Attunement.y * Attunement.w, Attunement.z * Attunement.w);
            Attunement = new Vector4();

            Vector3 tor = new Vector3(Assertation.x * Assertation.w, Assertation.y * Assertation.w, Assertation.z * Assertation.w);
            Assertation = new Vector4();

            Vector4 specifics = new Vector3(ener.x * tor.x, ener.y * tor.y, ener.z * tor.z) + pos;
            specifics.w = dimensionality;
            return specifics;
        }

        //ReinitializationalParamaterlessFunctionalitySystem
        //A.K.A. Role-Playing File System
        //A.K.A. Regional Protection Forwarding Service
        //A.K.A. Relentless Persona Friction State
        //A.K.A. Only William Really Knows What It Does! Till Now :)
        public float Expostulate(float deltaTime)
        {
            float dimensionality = Ascription.w + Attunement.w + Assertation.w + Conductance.w;

            float primordials = Mathf.Pow(Ascription.x, deltaTime);
            primordials += Mathf.Pow(Ascription.y, deltaTime);
            primordials += Mathf.Pow(Ascription.z, deltaTime);
            
            primordials += Mathf.Pow(Attunement.x, deltaTime);
            primordials += Mathf.Pow(Attunement.y, deltaTime);
            primordials += Mathf.Pow(Attunement.z, deltaTime);

            primordials += Mathf.Pow(Assertation.x, deltaTime);
            primordials += Mathf.Pow(Assertation.y, deltaTime);
            primordials += Mathf.Pow(Assertation.z, deltaTime);

            primordials += Mathf.Pow(Conductance.x, deltaTime);
            primordials += Mathf.Pow(Conductance.y, deltaTime);
            primordials += Mathf.Pow(Conductance.z, deltaTime);

            primordials /= Mathf.Pow(deltaTime, 3);
            primordials *= Mathf.Pow(dimensionality, 5);

            primordials *= Mathf.Pow(1, deltaTime);

            return primordials;
        }

        public void Advance(float deltaTime)
        {
            float energySolve = Descriptor.x * Ascription.w * deltaTime;
            Ascription = new Vector4(Ascription.x * energySolve, Ascription.y * energySolve, Ascription.z * energySolve, Ascription.w - energySolve);

            float positionSolve = Descriptor.y * Attunement.w * deltaTime;
            Attunement = new Vector4(Attunement.x * positionSolve, Attunement.y * positionSolve, Attunement.z * positionSolve, Attunement.w - positionSolve);

            float forceSolve = Descriptor.z * Conductance.w * deltaTime;
            Conductance = new Vector4(Conductance.x * forceSolve, Conductance.y * forceSolve, Conductance.z * forceSolve, Conductance.w - forceSolve);

            float torqueSolve = Descriptor.w * Assertation.w * deltaTime;
            Assertation = new Vector4(Assertation.x * torqueSolve, Assertation.y * torqueSolve, Assertation.z * torqueSolve, Assertation.w - torqueSolve);
        }

        public void Reduce(float delta, Func<double, double, double> algorithmic, Func<Particle> stream)
        {
            
        }

        public void SetPosition(float deltaTime)
        {
            transform.localPosition = PointPosition(deltaTime);
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

            SetPosition(Time.deltaTime);

            //transform.localPosition = (Vector3)Move(Time.deltaTime);
        }

        void OnDestroy()
        {
            onDestroy?.Invoke();
        }
    }
}