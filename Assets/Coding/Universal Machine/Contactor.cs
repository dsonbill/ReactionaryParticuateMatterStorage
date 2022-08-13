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

        public SimpleShacle Binding;

        public List<Particle> Particles = new List<Particle>();

        System.Random r = new System.Random();

        void Awake()
        {
            Source.Particles = InitialParticles;

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


            //Vector3 bang = Source.transform.up * (float)Source.EnergyDensity * (float)Source.ParticleMass * (float)r.NextDouble();

            Vector3 initialForce = new Vector3(fx, fy, fz);
            Vector3 initialPosition = new Vector3(x, y, z);
            Vector3 initialEnergy = new Vector3(ex, ey, ez);

            //Debug.Log(initialEnergy);

            GameObject particle = Instantiate(Particle, initialPosition, Quaternion.identity);
            particle.transform.parent = transform;
            particle.transform.localPosition = initialPosition;
            particle.SetActive(true);

            Particle p = particle.GetComponent<Particle>();

            p.Project = Marker.Project;
            
            //ds967',[;/\'
            
            p.Position = new Vector4(initialPosition.x, initialPosition.y, initialPosition.z, 1);
            p.Energy = new Vector4(initialEnergy.x, initialEnergy.y, initialEnergy.z, 1);

            p.ContactDepth = (float)Source.ParticleMass * (float)Zone.ContactRatio;
            
            p.AddForce(initialForce, Vector3.zero, Time.deltaTime);

            Particles.Add(p);   
        }

        void EnscriptionDisc()
        {
            foreach (Particle particle in Particles)
            {
                double energy = r.NextDouble() * Meaning.Energy() / UniversalMachine.Particle.EnergeticResistance;
                Vector3 offset = Meaning.Offset();

                particle.AddForce(Meaning.transform.up * (float)energy, offset, Time.deltaTime);
            }
        }

        void DarkRadiance()
        {
            foreach (Particle particle in Particles)
            {
                float distance = Vector3.Distance(Meaning.transform.position, particle.PointPosition(Time.deltaTime));
                double energy = Radiance.GetIntensity(distance);
                Vector3 offset = Meaning.transform.right * (float)Radiance.GetReactivity(distance) * (float)energy;
                particle.AddForce(offset, Vector3.zero, Time.deltaTime);
            }
        }

        void ContactorOutput()
        {
            foreach (Particle particle in Particles)
            {
                float distance = Vector3.Distance(Source.transform.position, particle.PointPosition(Time.deltaTime));
                double energy = Source.EnergyDensity * Source.ParticleMass / distance;
                Vector3 force = Source.transform.up * (float)energy;
                particle.AddForce(force, Vector3.zero, Time.deltaTime);
            }
        }

        void FixedUpdate()
        {
            EnscriptionDisc();
            
            //DarkRadiance();
            
            ContactorOutput();
            
            //Contacts.Exchange(Particles);
            
            //Zone.Height = Meaning.Height;
            //Zone.Friction(Particles);
            
            //Binding.Bind(Particles);
        }
    }
}