using MoonSharp.VsCodeDebugger.SDK;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace UniversalMachine
{
    public class FluidDispensary : MonoBehaviour
    {
        public GameObject Prefabricant;

        public Action OnDestroy;

        public int Quanta;

        public int Range;

        public Func<float> Approach;

        public Func<float> SafetyZone;

        public Func<float> ExistentCapacity;

        public Func<Func<Vector3, Vector3, Vector3, Vector3>> ProjectionReceivance;

        public Func<float> ContactDepth;

        public Action<Particle> IndexParticle;
        public Action<Particle> DeindexParticle;



        System.Random r = new System.Random();

        // 12/30/2021 : 11:34 PM
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Particle>() != null)
            {
                DeindexParticle(collision.gameObject.GetComponent<Particle>());
                Destroy(collision.gameObject);
            }
        }

        void Update()
        {
            for (int i = Quanta; i < Range; i++)
            {
                BeginParticle();
            }
            Quanta = Range;

        }

        public void BeginParticle()
        {
            float x = 0; // (float)Source.Diameter / 2 * (float)r.NextDouble();
            float y = SafetyZone() * (float)r.NextDouble();
            float z = 0; // (float)Source.Diameter / 2 * (float)r.NextDouble();

            float fx = Approach() * (float)r.NextDouble();
            float fy = Approach() * (float)r.NextDouble();
            float fz = Approach() * (float)r.NextDouble();

            float ex = ExistentCapacity() * (float)r.NextDouble();
            float ey = ExistentCapacity() * (float)r.NextDouble();
            float ez = ExistentCapacity() * (float)r.NextDouble();


            //Vector3 bang = Source.transform.up * (float)Source.EnergyDensity * (float)Source.ParticleMass * (float)r.NextDouble();

            Vector3 initialForce = new Vector3(fx, fy, fz);
            Vector3 initialPosition = new Vector3(x, y, z);
            Vector3 initialEnergy = new Vector3(ex, ey, ez);

            //Debug.Log(initialEnergy);

            GameObject particle = Instantiate(Prefabricant, initialPosition, Quaternion.identity);
            particle.transform.parent = transform;
            particle.transform.localPosition = initialPosition;
            particle.SetActive(true);

            Particle p = particle.GetComponent<Particle>();

            p.Project = ProjectionReceivance();

            //ds967',[;/\'

            p.Position = new Vector4(initialPosition.x, initialPosition.y, initialPosition.z, 1);
            p.Energy = new Vector4(initialEnergy.x, initialEnergy.y, initialEnergy.z, 1);

            p.ContactDepth = ContactDepth() * (float)r.NextDouble();

            p.AddForce(initialForce, Vector3.zero, Time.deltaTime);

            IndexParticle(p);

            p.onDestroy += () => { OnDestroy(); } ;
        }
    }
}