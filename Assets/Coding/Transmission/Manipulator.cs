using System;
using UnityEngine;

namespace Transmission.Components
{
    public class Manipulator : MonoBehaviour
    {


        public ManipulatorRing A;
        public ManipulatorRing B;
        public ManipulatorRing C;
        public ManipulatorRing D;
        public ManipulatorRing Mode;

        public Transmitter Transmitter;

        double Value;

        void Start()
        {
            Transmitter.manipulator = Manipulate;
        }

        double Manipulate(double data)
        {
            switch(Mode.Value)
            {
                case (int)ManipulatorRing.Mode.Add:
                    return data + Value;
                case (int)ManipulatorRing.Mode.Subtract:
                    return data - Value;
                case (int)ManipulatorRing.Mode.Multiply:
                    return data * Value;
                case (int)ManipulatorRing.Mode.Divide:
                    return data / Value;
                case (int)ManipulatorRing.Mode.Exponent:
                    return Math.Pow(data, Value);
            }
            return 0;
        }

        public void Update()
        {
            Value = 0;
            Value += A.Value * 100;
            Value += B.Value * 10;
            Value += C.Value;
            Value *= D.Value;
        }
    }
}