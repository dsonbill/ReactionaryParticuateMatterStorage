using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class Contactor : MonoBehaviour
    {
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
            EnscriptionDiscSetup();
            ContactorSetup();
            DispensarySetup();
            ExistenceSetup();
        }

        

        void EnscriptionDiscSetup()
        {
            Meaning.Quanta = () => { return UnitQuanta; };
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
            Well.Approach = () => { return (float)Source.AssertationScale; };
            Well.SafetyZone = () => { return Source.Diameter; };

            Well.IndexParticle = (p) => { UnitQuanta.Add(p); };
            Well.DeindexParticle = (p) => { UnitQuanta.Remove(p); };

            Well.ProjectionReceivance = () => { return Marker.Project; };

            Well.ExistentCapacity = () => { return (float)Source.Reach; };
            Well.ContactDepth = () => { return (float)Source.UnitAscriptiveDensity; };

            Well.SpawnAction = (p) =>
            {
                Source.Birth(p);
            };
        }

        void ExistenceSetup()
        {
            Zone.Quanta = () => { return UnitQuanta; };
        }

        void FixedUpdate()
        {
            
            
            //DarkRadiance();
            
            //Contacts.Exchange(Particles);
            
            //Zone.Height = Meaning.Height;
            //Zone.Friction(Particles);
            
            //Binding.Bind(Particles);
        }
    }
}