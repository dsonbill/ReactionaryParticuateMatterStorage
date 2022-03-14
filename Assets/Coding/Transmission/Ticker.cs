using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Transmission.Components
{
    public class Ticker : MonoBehaviour
    {
        public Transmitter Transmitter;
        public Transmitter Receiver;

        public Transmitter Period;

        public Text Text;

        double tock;

        // Start is called before the first frame update
        void Start()
        {
            Transmitter.manipulator = Manipulate;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            tock += Time.deltaTime;

            Text.text = tock.ToString() + " / " + Period.rx.ToString(); ;
        }

        double Manipulate(double data)
        {
            if (tock >= Period.rx)
            {
                tock = 0;
                return Receiver.rx;
            }

            return 0;
        }
    }
}