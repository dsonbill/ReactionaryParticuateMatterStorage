using System.Collections;
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

            Position();
        }

        void Position()
        {
            Primary.localPosition = new Vector3(PrimaryPosition.x, PrimaryPosition.y + (float)Height / 2, PrimaryPosition.z);
            Primary.localScale = new Vector3(PrimaryScale.x, PrimaryScale.y + (float)Height / 4, PrimaryScale.z);

            Secondary.localPosition = new Vector3(SecondaryPosition.x, SecondaryPosition.y + ((float)Height / 2 * (float)ContactRatio), SecondaryPosition.z);
            Secondary.localScale = new Vector3(SecondaryScale.x, SecondaryScale.y + ((float)Height / 4 * (float)ContactRatio), SecondaryScale.z);

            PrimaryMesh.material.color = ColorGradient.Evaluate(pTime);
            SecondaryMesh.material.color = ColorGradient.Evaluate(sTime);
        }

        public void Friction(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                Vector3 particlePosition = new Vector3(particle.Position.x, particle.Position.y, particle.Position.z);
                Vector3 offset = particlePosition - Primary.position;

                Vector3 rot = new Vector3(Secondary.rotation.x / 360, Secondary.rotation.y / 360, Secondary.rotation.z / 360);
                Vector3 onset = offset + (Secondary.position + (rot * ((Secondary.localScale.x + Secondary.localScale.y) / 2)));

                particle.AddForce(-onset.normalized * onset.magnitude, Vector3.one, Time.deltaTime);

                //Left by that fucking bastard
            }
        }
    }
}