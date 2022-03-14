using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Transmission.Components
{
    public class Condenser : MonoBehaviour
    {
        public Transmitter Transmitter;
        public Transmitter Receiver;

        public Transmitter Ratio;
        public Transmitter Reset;

        public Text Text;

        public double ResetLevel = 1;
        public double Divisor = 1;

        public double Store;
        double PreviousData;

        void Start()
        {
            Transmitter.manipulator = Manipulate;
        }

        void LateUpdate()
        {
            if (Receiver.rx != PreviousData)
            {
                Store += Receiver.rx * Ratio.rx;
                PreviousData = Receiver.rx * Ratio.rx;
            }

            if (Reset.rx >= ResetLevel)
            {
                Store = 0;
            }

            Text.text = Store.ToString();
        }

        double Manipulate(double data)
        {
            return Store;
        }
    }
}