using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Transmission.Components
{
    public class TSource : MonoBehaviour
    {
        public Transmitter Transmitter;
        public double Energy;

        public TSourceInterface Interface;

        void Start()
        {
            Interface.onValueChanged += (double EnergyLevel) =>
            {
                Energy = EnergyLevel;
            };

            Transmitter.manipulator = Manipulate;
        }

        double Manipulate(double input)
        {
            return Energy;
        }
    }
}