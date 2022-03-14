using System;
using UnityEngine;
using System.Collections.Generic;

namespace Transmission
{
    [AddComponentMenu("Transmitter")]
    [DefaultGameImplementation("DefaultTransmitterImplementation")]
    public class Transmitter : ObjectiveGameImplementation
    {
        public List<TransmitterConnector> connectors;
        
        public double rx;

        public Func<double, double> manipulator;
        Func<double, double> maniTrack;

        public override void Start()
        {
            if (implementation == null) InstantiateImplementation(this);
        }

        public override void Update()
        {
            base.Update();

            rx = (implementation as ITransmitterImplementation).rx;

            if (maniTrack != manipulator)
            {
                (implementation as ITransmitterImplementation).manipulator = manipulator;
                maniTrack = manipulator;
            }
        }
    }

    
    public interface ITransmitterImplementation
    {
        //Base
        Transmitter wrapper { get; set; }

        //Data
        double rx { get; set; }

        //Manipulation
        Func<double, double> manipulator { get; set; }
    }

    [GameImplementation("DefaultTransmitterImplementation")]
    public class DefaultTransmitterImplementation : IGameImplementation, ITransmitterImplementation
    {
        //Base
        public Transmitter wrapper { get; set; }

        //Data
        public double rx { get; set; }

        //Manipulation
        public Func<double, double> manipulator { get; set; }

        //Tracking
        public Dictionary<TransmitterConnector, double> backflowPrevention;

        //Implementation
        public Action UpdateFunction { get; set; }
        public Action DeathFunction { get; set; }

        public DefaultTransmitterImplementation(Transmitter wrapper)
        {
            this.wrapper = wrapper;
            UpdateFunction = () =>
            {
                Receive();
                Transmit();
            };
        }

        void Transmit()
        {
            foreach (TransmitterConnector connector in wrapper.connectors)
            {
                if (connector.connected)
                    if (manipulator != null)
                        connector.tx = manipulator(rx - backflowPrevention[connector]);
                    else
                        connector.tx = rx - backflowPrevention[connector];
                else
                    connector.tx = 0;
            }
        }

        void Receive()
        {
            rx = 0;

            backflowPrevention = new Dictionary<TransmitterConnector, double>();
            foreach (TransmitterConnector connector in wrapper.connectors)
            {
                if (connector.connected)
                {
                    backflowPrevention[connector] = (connector.connectedConnector as TransmitterConnector).tx;
                    rx += backflowPrevention[connector];
                    
                }
            }
        }
    }
}
