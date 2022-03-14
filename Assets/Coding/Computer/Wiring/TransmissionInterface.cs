using Transmission;
using System.Collections.Generic;
using UnityEngine;

namespace Computers.Hardware
{
    public class TransmissionInterface : MonoBehaviour
    {
        public List<Transmitter> Transmitters;

        public double[] Output;

        void Start()
        {
            Output = new double[Transmitters.Count];

            for (int i = 0; i < Transmitters.Count; i++)
            {
                int x = i;
                Transmitters[i].manipulator = (double data) => { return Output[x]; };
            }
        }
    }
}