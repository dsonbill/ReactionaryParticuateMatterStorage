using JsonFx.Bson;
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

        public ContactZoneController ZoneController;

        public List<Particle> UnitQuanta = new List<Particle>();

        System.Random r = new System.Random();

        void Awake()
        {
            EnscriptionDiscSetup();
            ContactorSetup();
            DispensarySetup();
            ExistenceSetup();
            ZoneControllerSetup();
        }
        

        void EnscriptionDiscSetup()
        {
            Meaning.Quanta = () => { return UnitQuanta; };
        }

        //void DarkRadiance()
        //{
        //    foreach (Particle particle in UnitQuanta)
        //    {
        //        float distance = Vector3.Distance(Meaning.transform.position, particle.PointPosition(Time.deltaTime));
        //        double energy = Radiance.GetIntensity(distance);
        //        Vector3 offset = Meaning.transform.right * (float)Radiance.GetReactivity(distance) * (float)energy;
        //        particle.AddForce(offset, Vector3.zero, Time.deltaTime);
        //    }
        //}

        void ContactorSetup()
        {
            Source.Ascriptions = () => { return UnitQuanta.Count; }; //Particle Counter Function
            Source.ContactRatio = () => { return (float)(1 / Zone.Diameter * Source.Diameter);  }; //Unit/Area Contact Ratio Amount
        }

        void DispensarySetup()
        {
            Well.Quanta = () => { return UnitQuanta.Count; };
            Well.Range = () => { return (int)Source.AssertationScale; };

            Well.OnDestroy = (p) => { UnitQuanta.Remove(p); };

            Well.Approach = () => { return (float)r.NextDouble(); };
            Well.SafetyZone = () => { return Source.Diameter; };

            Well.IndexParticle = (p) => { UnitQuanta.Add(p); };

            Well.ProjectionReceivance = () => { return Marker.Project; };

            Well.ExistentCapacity = () => { return (float)Source.UnitAscriptiveDensity; };
            Well.ContactDepth = () => { return (float)Source.ContactRatio() * (float)Source.UnitAscriptiveDensity; };

            Well.SpawnAction = (p) =>
            {
                Source.Birth(p);
            };
        }

        void ExistenceSetup()
        {
            Zone.Quanta = () => { return UnitQuanta; };
            Zone.WellDistance = () =>
            {
                return new Vector2(
                    Vector3.Distance(Well.transform.position, Zone.Primary.transform.position),
                    Vector3.Distance(Well.transform.position, Zone.Secondary.transform.position)
                    );
            };

            Zone.WellDirection = (exPos) =>
            {
                return (Well.transform.position - exPos).normalized;
            };
        }

        void ZoneControllerSetup()
        {
            ZoneController.ZoneExit = (c) =>
            {
                if (c.gameObject.GetComponent<Particle>() != null)
                {
                    Well.OnDestroy(c.gameObject.GetComponent<Particle>());
                    Destroy(c.gameObject);
                }
            };

            ZoneController.CapEnter = (c) =>
            {
                if (c.gameObject.GetComponent<Particle>() != null)
                {
                    Well.OnDestroy(c.gameObject.GetComponent<Particle>());
                    Destroy(c.gameObject);
                }
            };
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