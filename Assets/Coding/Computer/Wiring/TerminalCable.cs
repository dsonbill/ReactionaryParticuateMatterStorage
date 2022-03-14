using System;
using UnityEngine;
using System.Collections.Generic;

namespace Computers
{
    [AddComponentMenu("TerminalCable")]
    [DefaultGameImplementation("DefaultTerminalCableImplementation")]
    public class TerminalCable : ObjectiveGameImplementation
    {
        public List<TerminalConnector> connectors;

        public Guid guid;

        public Func<Guid> broadcast;
        Func<Guid> bctrack;

        public override void Start()
        {
            if (implementation == null) InstantiateImplementation(this);
        }

        public override void Update()
        {
            base.Update();

            guid = (implementation as ITerminalCableImplementation).rxGuid;

            if (broadcast != bctrack)
            {
                (implementation as ITerminalCableImplementation).broadcast = broadcast;
                bctrack = broadcast;
            }
        }
    }


    public interface ITerminalCableImplementation
    {
        //Base
        TerminalCable wrapper { get; set; }

        //Data
        public Guid rxGuid { get; set; }

        //Broadcast
        public Func<Guid> broadcast { get; set; }
    }

    [GameImplementation("DefaultTerminalCableImplementation")]
    public class DefaultTerminalCableImplementation : IGameImplementation, ITerminalCableImplementation
    {
        //Base
        public TerminalCable wrapper { get; set; }

        //Data
        public Guid rxGuid { get; set; }

        //Broadcast
        public Func<Guid> broadcast { get; set; }

        //Tracking
        public Dictionary<TerminalConnector, double> backflowPrevention;

        //Implementation
        public Action UpdateFunction { get; set; }
        public Action DeathFunction { get; set; }

        public DefaultTerminalCableImplementation(TerminalCable wrapper)
        {
            this.wrapper = wrapper;
            //id = Guid.NewGuid();
            UpdateFunction = () =>
            {
                Receive();
                Transmit();
            };
        }

        void Transmit()
        {
            foreach (TerminalConnector connector in wrapper.connectors)
            {
                if (connector.connected && broadcast != null)
                    connector.txGuid = broadcast();
                else if (connector.connected)
                    connector.txGuid = rxGuid;
                else
                    connector.txGuid = Guid.Empty;
            }

        }

        void Receive()
        {
            rxGuid = Guid.Empty;

            foreach (TerminalConnector connector in wrapper.connectors)
            {
                if (connector.connected)
                {
                    Guid received = (connector.connectedConnector as TerminalConnector).txGuid;
                    if (received != Guid.Empty)
                        rxGuid = received;
                }
            }
        }
    }
}
