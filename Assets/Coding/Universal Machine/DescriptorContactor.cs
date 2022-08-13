                                                                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class DescriptorContactor : MonoBehaviour
    {
        public class Modes
        {
            public static double Alpha = 1.0;
            public static double Beta = 0.1;
            public static double Gamma = 0.01;
        }

        public enum Mode
        {
            Alpha,
            Beta,
            Gamma
        }

        public Transform DC;
        public float Diameter;

        public double EnergyDensity;

        public Mode MassMode;

        public double ParticleMass { get; private set; }

        public int Particles;

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

        void ParticulateCoDensifier()
        {
            switch (MassMode)
            {
                case Mode.Alpha:
                    ParticleMass = Modes.Alpha * EnergyDensity / Particles;
                    break;
                case Mode.Beta:
                    ParticleMass = Modes.Beta * EnergyDensity / Particles;
                    break;
                case Mode.Gamma:
                    ParticleMass = Modes.Gamma * EnergyDensity / Particles;
                    break;
            }
        }

        

        // Update is called once per frame
        void Update()
        {
            DC.localScale = new Vector3((float)Diameter, (float)Diameter, (float)Diameter);
            DC.localPosition = new Vector3(DC.localPosition.x, -(float)EnergyDensity, DC.localPosition.z);

            EnergyDensity = Mathf.Pow((float)ParticleMass, 2) * (Diameter * 2) * Particles;

            Light.range = (float)Diameter * 3;
            Light.intensity = (float)EnergyDensity;

            ParticulateCoDensifier();
        }
    }
}