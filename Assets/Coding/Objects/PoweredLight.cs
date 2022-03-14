using UnityEngine;
using System.Collections.Generic;
using System;

namespace Transmission
{
    [AddComponentMenu("Wiring/LED")]
    public class PoweredLight : MonoBehaviour
    {
        public GameObject destructionObject;
        public Transmitter transmitter;
        public Light Light;
        public Color color = Color.white;
        public Color onColor = Color.yellow;
        public double offEmission = 0.0;
        public double cutoffLevel = 5.0;
        public double criticalLevel = 300.0;
        public double onIntensity = 0.75;
        public Renderer Mesh;

        float intensity;
        bool state;

        void Start()
        {
            //light.color = color;
            if (Mesh == null) Mesh = GetComponent<Renderer>();
        }

        void FixedUpdate()
        {
            if (Math.Abs(transmitter.rx) > cutoffLevel && Math.Abs(transmitter.rx) < criticalLevel)
            {
                state = true;
            }
            else state = false;

            if (state)
            {
                intensity = (float)((1 / criticalLevel) * Math.Abs(transmitter.rx));
                Mesh.material.SetColor("_EmissionColor", onColor * Mathf.LinearToGammaSpace(intensity));
                Light.intensity = intensity * (float)onIntensity;
            }

            else
            {
                Mesh.material.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace((float)offEmission));
                Light.intensity = 0;
            }
        }
    }
}