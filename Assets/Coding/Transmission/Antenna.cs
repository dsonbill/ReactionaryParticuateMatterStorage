using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Transmission.Components
{
    public class Antenna : MonoBehaviour
    {
        static public List<Antenna> Antennas = new List<Antenna>();

        public Rigidbody AntennaRB;

        public Transmitter Transmitter;
        public Transmitter Receiver;

        public Transmitter txPower;

        public Text ChannelNumber;
        public int Channel;

        public double rx;

        // Start is called before the first frame update
        void Start()
        {
            Antennas.Add(this);
            Receiver.manipulator = Manipulate;
        }

        void OnDestroy()
        {
            Antennas.Remove(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Receive()
        {
            rx = 0;
            foreach (Antenna antenna in Antennas)
            {
                if (antenna == this || antenna.Channel != Channel)
                {
                    continue;
                }

                if (Vector3.Distance(AntennaRB.position, antenna.AntennaRB.position) < antenna.txPower.rx)
                {
                    rx += antenna.Transmitter.rx;
                }
            }
        }

        double Manipulate(double data)
        {
            Receive();
            return rx;
        }

        public void ChannelUp()
        {
            Channel++;

            if (Channel > 99)
            {
                Channel = 0;
            }

            ChannelNumber.text = Channel.ToString();
        }

        public void ChannelDown()
        {
            Channel--;

            if (Channel < 0)
            {
                Channel = 99;
            }

            ChannelNumber.text = Channel.ToString();
        }
    }
}