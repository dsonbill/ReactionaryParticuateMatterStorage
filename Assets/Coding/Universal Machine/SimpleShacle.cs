using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class SimpleShacle : MonoBehaviour
    {
        public GameObject Cylinder;
        public float Diameter;

        public float Binding;

        System.Random r = new System.Random();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Bind(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                Vector2 localPosition = new Vector2(particle.Position.x, particle.Position.z) - new Vector2(Cylinder.transform.position.x, Cylinder.transform.position.z);
                Vector2 direction = localPosition.normalized;
                float distance = localPosition.magnitude;

                float reduction = 1 / (Diameter * 2) * distance * (float)(Binding * r.NextDouble());

                Vector3 inwardForce = (-particle.PointForce(Time.deltaTime)) * reduction;

                particle.AddForce(inwardForce, Vector3.zero, Time.deltaTime);
            }
        }
    }
}