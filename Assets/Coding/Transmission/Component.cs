using System;
using System.Collections.Generic;
using UnityEngine;

namespace Transmission
{
    public class Component : MonoBehaviour
    {
        public static Dictionary<string, Component> Registry = new Dictionary<string, Component>();
        public Guid Guid;
        public List<string> Pins = new List<string>();
        public Transmission.ComponentBase Base;
        public Guid Circuit = Guid.Empty;
        public DarkGraph.Component DarkComponent;

        public virtual void Awake()
        {
            Guid = Guid.NewGuid();
            Registry.Add(Guid.ToString(), this);
        }

        public virtual void Connect(int pin, int toPin, Component toComponent)
        {
            DarkComponent.Connect(pin, toComponent.DarkComponent, toPin);
            Component component = toComponent;
            //CircuitThread.Instance.QueueAction(new Action(() =>
            //{
            //    MasterCircuit.Instance.ConnectComponents(this, component);
            //}));
            MasterCircuit.Instance.ConnectComponents(this, component);
        }

        public virtual void Disconnect(int pin, Component fromComponent)
        {
            DarkComponent.Disconnect(pin);
            Component component = fromComponent;
            //CircuitThread.Instance.QueueAction(new Action(() =>
            //{
            //    MasterCircuit.Instance.DisconnectComponents(this, component);
            //}));
            MasterCircuit.Instance.DisconnectComponents(this, component);
        }

        public virtual void BeforeExecute(object sender, EventArgs eventArgs)
        {

        }
    }
}