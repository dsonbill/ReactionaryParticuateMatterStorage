using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMachine
{
    public class LightSource : MonoBehaviour
    {
        public double Intensity;
        public double Potential;

        public Light Light;

        // ABCDEFG 12345 |\|\|\  [786]  ~~
        //1234567890.2601346790

        //12/30/2021 - Thought of date and was pulled off course

        public double GetIntensity(double distance)
        {
            return (Intensity / Math.Pow(distance, 2) * 2);
        }

        public double GetReactivity(double distance)
        {
            return Potential * GetIntensity(distance);
        }

        // Update is called once per frame
        void Update()
        {
            Light.intensity = (float)Intensity;
            Light.range = (float)Potential;
        }
    }
}