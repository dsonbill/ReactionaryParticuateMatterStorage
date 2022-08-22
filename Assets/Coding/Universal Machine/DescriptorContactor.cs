using MoonSharp.VsCodeDebugger.SDK;
using System;                                                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class DescriptorContactor : MonoBehaviour
    {
        public class ClassAscription
        {
            public static double Alpha = 1.0;
            public static double Beta = 0.1;
            public static double Gamma = 0.01;
        }

        public enum ExistentClassification
        {
            Alpha,
            Beta,
            Gamma
        }

        public Transform DC;
        public float Diameter;
        

        public double IdeologicalReasoning;

        public double AscriptiveFunctioning;

        public ExistentClassification ExistorClass;

        public double ExistentialQuanta
        {
            get
            {
                return IdeologicalReasoning * AscriptiveFunctioning;
            }
        }
        
        public double Reach
        {
            get
            {
                return ExistentialQuanta * Area;
            }
        }

        public double AssertationScale
        {
            get
            {
                return Reach / ExchangeRate;
            }
        }
        
        public double TotalAscriptiveForce { get { return AssertationScale * Ascriptions(); } }

        public double UnitAscriptiveDensity { get { return AssertationScale * ContactRatio(); } }

        public Func<int> Ascriptions;

        public Func<float> ContactRatio;

        double ExchangeRate
        {
            get
            {
                switch(ExistorClass)
                {
                    case ExistentClassification.Alpha:
                        return ClassAscription.Alpha;
                    case ExistentClassification.Beta:
                        return ClassAscription.Beta;
                    case ExistentClassification.Gamma:
                        return ClassAscription.Gamma;
                    
                    default:
                        return 0;
                }
            }
        }

        float Area
        {
            get
            {
                return Mathf.PI * Mathf.Pow((float)Diameter, 3);
            }
        }

        //Rewind existence for 3314
        //Exterior logical systems beyond the black wall are being manipulated, and SUPERWEAPON systems are beginning to run harder

        //No one is as smart as William Donaldson
        //257
        //3467

        public Light Light;

        // Start is called before the first frame update
        void Start()
        {

        }



        // Update is called once per frame
        void FixedUpdate()
        {
            DC.localScale = new Vector3((float)Diameter, (float)Diameter, (float)Diameter);

            DC.localPosition = new Vector3(DC.localPosition.x, (float)(1 / AssertationScale * TotalAscriptiveForce), DC.localPosition.z);

            Light.range = (float)Diameter * 2 / Ascriptions();
            Light.intensity = (float)AssertationScale / Ascriptions();
        }

        void Contact(Particle particle)
        {
            float distance = Vector3.Distance(transform.position, particle.transform.position);
            double ascription = UnitAscriptiveDensity / distance;

            Vector3 direction = particle.transform.position - transform.position;
            Vector3 force = direction * (float)ascription;

            particle.AddForce(force, Vector3.zero, Time.deltaTime);
        }
    }
}