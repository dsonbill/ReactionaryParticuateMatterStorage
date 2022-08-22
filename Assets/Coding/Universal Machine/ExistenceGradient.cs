using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class ExistenceGradient : MonoBehaviour
    {
        //11:49 PM - 12/30/2021
        // Overwhelimg /blank mind/ -< -shift-  sense of repeating pattern
        //12:12 12/31/2021

        public Transform Primary;
        public Transform Secondary;

        public MeshRenderer PrimaryMesh;
        public MeshRenderer SecondaryMesh;

        Vector3 PrimaryPosition;
        Vector3 SecondaryPosition;
        Vector3 PrimaryScale;
        Vector3 SecondaryScale;

        public double Height;
        public double ContactRatio;

        public Gradient ColorGradient;
        public Gradient SecondaryColorGradient;

        public float EnergyDensity;

        float pTime;
        float sTime;

        // Start is called before the first frame update
        void Start()
        {
            PrimaryPosition = Primary.transform.localPosition;
            SecondaryPosition = Secondary.transform.localPosition;
            PrimaryScale = Primary.transform.localScale;
            SecondaryScale = Secondary.transform.localScale;

            //ColorGradient = new Gradient();
            //colorKey = new GradientColorKey[Colors.Count];
            //alphaKey = new GradientAlphaKey[Colors.Count];

            //for (int i = 0; i < Colors.Count; i++)
            //{
            //    colorKey[i].color = Colors[i];
            //    colorKey[i].time = 1 / Colors.Count * i;
            //}
            //
            //float densityRatio = (float)ContactRatio / Colors.Count;
            //for (int i = 0; i < Colors.Count; i++)
            //{
            //    //alphaKey[i].alpha = 1.0f / Colors.Count * i * (densityRatio * i);
            //    alphaKey[i].alpha = 1f;
            //    alphaKey[i].time = 1 / Colors.Count * i;
            //}

            //ColorGradient.SetKeys(colorKey, alphaKey);
            //ColorGradient.mode = GradientMode.Blend;

            sTime = 0.84f;
        }

        void CalculateEnergy()
        {
            float positionOffset = Vector3.Distance(Primary.localPosition, Secondary.localPosition);
            float rotationalOffset = Quaternion.Angle(Primary.localRotation, Secondary.localRotation);

            Color offsetColor = PrimaryMesh.material.color - SecondaryMesh.material.color;
            float stateOffset = (offsetColor.r + offsetColor.g + offsetColor.b) / 3;

            EnergyDensity = positionOffset * rotationalOffset;
        }


        void Update()
        {
            pTime += Time.deltaTime;
            sTime -= Time.deltaTime;
            if (pTime >= 1)
            {
                pTime = 0;
            }
            if (sTime <= 0)
            {
                sTime = 1;
            }

            Manifest();

            CalculateEnergy();
        }

        void Manifest()
        {
            Primary.localPosition = new Vector3(PrimaryPosition.x, PrimaryPosition.y + (float)Height / 2, PrimaryPosition.z);

            Secondary.localPosition = new Vector3(SecondaryPosition.x, SecondaryPosition.y + ((float)Height / 2 * (float)ContactRatio), SecondaryPosition.z);
            Secondary.localScale = new Vector3(SecondaryScale.x, SecondaryScale.y + ((float)Height / 4 * (float)ContactRatio), SecondaryScale.z);

            PrimaryMesh.material.color = ColorGradient.Evaluate(pTime);
            SecondaryMesh.material.color = SecondaryColorGradient.Evaluate(sTime);
        }

        public void Friction(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                Vector3 particlePosition = particle.PointPosition(Time.deltaTime);
                Vector3 distance = particlePosition - Primary.localPosition;

                //Vector3 rot = new Vector3(Secondary.localRotation.x / 360, Secondary.localRotation.y / 360, Secondary.localRotation.z / 360);
                Vector3 onset = distance +
                    (Primary.localPosition + (new Vector3(Primary
.localScale.x, Primary.localScale.y, Primary.localScale.z) / 2));

                particle.AddForce(-onset.normalized * onset.magnitude, Vector3.zero, Time.deltaTime);

                //Left by that fucking bastard - I guess I was right after all!!
            }



        }
    }
}