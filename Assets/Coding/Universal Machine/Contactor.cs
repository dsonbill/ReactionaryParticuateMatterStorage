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

        public List<Particle> UnitQuanta = new List<Particle>();

        System.Random r = new System.Random();

        void Awake()
        {
            Source.Ascriptions = () => {  };

            ContactorSetup();
            DispensarySetup();
        }

        

        void EnscriptionDisc()
        {
            foreach (Particle particle in UnitQuanta)
            {
                double energy = r.NextDouble() * Meaning.Energy() / UniversalMachine.Particle.EnergeticResistance;
                Vector3 offset = Meaning.Offset();

                particle.AddForce(Meaning.transform.up * (float)energy, offset, Time.deltaTime);
            }
        }

        void DarkRadiance()
        {
            foreach (Particle particle in UnitQuanta)
            {
                float distance = Vector3.Distance(Meaning.transform.position, particle.PointPosition(Time.deltaTime));
                double energy = Radiance.GetIntensity(distance);
                Vector3 offset = Meaning.transform.right * (float)Radiance.GetReactivity(distance) * (float)energy;
                particle.AddForce(offset, Vector3.zero, Time.deltaTime);
            }
        }

        void ContactorSetup()
        {
            Source.Ascriptions = () => { return UnitQuanta.Count; }; //Particle Counter Function
            Source.ContactRatio = () => { return (float)(1 / Zone.Diameter * Source.Diameter);  }; //Unit/Area Contact Ratio Amount
        }

        void DispensarySetup()
        {
            Well.OnDestroy = () => { Well.Quanta--; };
            Well.Approach = () => { return (float)Source.AscriptiveQuanta; };
            Well.SafetyZone = () => { return Source.Diameter; };

            Well.IndexParticle = (p) => { UnitQuanta.Add(p); };
            
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