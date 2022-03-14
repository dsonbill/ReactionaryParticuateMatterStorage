using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class Contactor : MonoBehaviour
    {
        public GameObject Particle;

        public int InitialParticles;

        public EnscribedDisc Meaning;
        public LightSource Radiance;

        public ExistenceGradient Zone;

        public PathMarker Marker;

        public ForceExchange Contacts;

        public FluidDispensary Well;

        public DescriptorContactor Source;
        public PotentialHole Depths;

        public List<Particle> Particles = new List<Particle>();

        System.Random r = new System.Random();

        void Awake()
        {
            Well.OnDestroy = () => { BeginParticle(); };

            for (int i = 0; i < InitialParticles; i++)
            {
                BeginParticle();
            }
        }

        public void BeginParticle()
        {
            

            float x = (float)Source.Diameter / 2 * (float)r.NextDouble();
            float y = (float)Source.Diameter / 2 * (float)r.NextDouble();
            float z = (float)Source.Diameter / 2 * (float)r.NextDouble();

            float fx = (float)Source.ParticleMass * (float) r.NextDouble();
            float fy = (float)Source.ParticleMass * (float)r.NextDouble();
            float fz = (float)Source.ParticleMass * (float)r.NextDouble();

            float ex = (float)Source.EnergyDensity * (float)r.NextDouble();
            float ey = (float)Source.EnergyDensity * (float)r.NextDouble();
            float ez = (float)Source.EnergyDensity * (float)r.NextDouble();

            Vector3 bang = Source.transform.up * (float)Source.EnergyDensity * (float)Source.ParticleMass * (float)r.NextDouble();

            Vector3 initialForce = bang + new Vector3(fx, fy, fz);
            Vector3 initialPosition = Source.transform.position + new Vector3(x, y, z);
            Vector3 initialEnergy = new Vector3(ex, ey, ez);

            GameObject particle = Instantiate(Particle, initialPosition, Quaternion.identity);
            particle.SetActive(true);

            Particle p = particle.GetComponent<Particle>();

            p.Project = Marker.Project;

            p.Position = new Vector4(particle.transform.position.x, particle.transform.position.y, particle.transform.position.z, 1);
            p.Energy = new Vector4(initialEnergy.x, initialEnergy.y, initialEnergy.z, 1);
            
            p.AddForce(initialForce, Vector3.zero, Time.deltaTime);

            Particles.Add(p);
        }

        void Update()
        {
            foreach (Particle particle in Particles)
            {
                double energy = r.NextDouble() * Meaning.Energy();
                double offset = r.NextDouble() * Meaning.Offset();
                particle.AddForce(Vector3.one * (float)energy, Vector3.one * (float)offset, Time.deltaTime);
            }
        
            foreach (Particle particle in Particles)
            {
                float distance = Vector3.Distance(Meaning.transform.position, particle.PointPosition(Time.deltaTime));
                double energy = Radiance.GetIntensity(distance);
                Vector3 offset = Meaning.transform.right * (float)Radiance.GetReactivity(distance) * (float)energy;
                particle.AddForce(offset, Vector3.zero, Time.deltaTime);
            }

            foreach (Particle particle in Particles)
            {
                float distance = Vector3.Distance(Source.transform.position, particle.PointPosition(Time.deltaTime));
                double energy = Source.EnergyDensity * Source.ParticleMass * distance;
                Vector3 force = Source.transform.up * (float)energy;
                particle.AddForce(force, Vector3.zero, Time.deltaTime);
            }

            Contacts.Exchange(Particles);

            Zone.Height = Meaning.Height;
            Zone.Friction(Particles);
        }
    }
}