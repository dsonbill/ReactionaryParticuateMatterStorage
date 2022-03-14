using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace Transmission
{
    [AddComponentMenu("Transmitter Connector")]
    public class TransmitterConnector : Connector
    {
        public Transmitter Transmitter;
        public double tx;

        public Component Component;
        public int Pin;

        public void Awake()
        {
            connectCondition = (Connector socket) =>
            {
                if ((socket as TransmitterConnector) != null) return true;
                return false;
            };

            if (Transmitter == null)
            {
                Transmitter = GetComponent<Transmitter>();
            }
            if (Transmitter == null)
            {
                Transmitter = transform.parent.GetComponent<Transmitter>();
            }

            Transmitter.connectors.Add(this);
        }

        public override void Connect(Connector socket)
        {
            base.Connect(socket);

            TransmitterConnector tocon = (socket as TransmitterConnector);
            //Debug.Log("Connecting " + Wire.Guid.ToString() + " at pin " + Pin + " to " + tocon.Wire.Guid.ToString() + " at pin " + tocon.Pin);
            Component.Connect(Pin, tocon.Pin, tocon.Component);
        }

        public override void Disconnect()
        {
            //Disconnect Here
            if (connectedConnector != null)
            {
                TransmitterConnector concon = (connectedConnector as TransmitterConnector);
                Component.Disconnect(Pin, concon.Component);
            }

            base.Disconnect();
        }
    }
}