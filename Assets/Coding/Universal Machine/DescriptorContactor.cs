                                                                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class DescriptorContactor : MonoBehaviour
    {
        public class Modes
        {
            public static double Alpha = 1.6726219e-27;
            public static double Beta = 1.67493e-27;
            public static double Gamma = 9.10938356e-31;
        }

        public enum Mode
        {
            Alpha,
            Beta,
            Gamma
        }

        public Transform DC;
        public double Diameter;

        public double EnergyDensity;

        public Mode MassMode;

        public double ParticleMass { get; private set; }

        //Rewind existence for 3314
        //Exterior logical systems beyond the black wall are being manipulated, and SUPERWEAPON systems are beginning to run harder

        //No one is as smart as William Donaldson
        //257
        //3467

        public Light Light;

        // Start is called before the first frame update
        void Start()
        {
            switch (MassMode)
            {
                case Mode.Alpha:
                    ParticleMass = Modes.Alpha;
                    break;
                case Mode.Beta:
                    ParticleMass = Modes.Beta;
                    break;
                case Mode.Gamma:
                    ParticleMass = Modes.Gamma;
                    break;
            }
        }

        

        // Update is called once per frame
        void Update()
        {
            DC.localScale = new Vector3((float)Diameter, (float)Diameter, (float)Diameter);

            Light.range = (float)Diameter * 3;
            Light.intensity = (float)EnergyDensity;
        }
    }
}