using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace Transmission
{
    public class Circuit
    {
        public Guid Guid = Guid.NewGuid();

        public Dictionary<Guid, Component> Components;

        public Queue<Action> actionQueue;

        public bool destroy = false;

        DateTime lastUpdate = DateTime.Now;
        DateTime currentUpdate = DateTime.Now;

        double currentElapsedTime;

        public Circuit()
        {
            Components = new Dictionary<Guid, Component>();

            actionQueue = new Queue<Action>();
        }

        public void AddComponent(Component component)
        {
            if (!Components.ContainsKey(component.Guid))
            {
                Components.Add(component.Guid, component);
            }
        }

        public void RemoveComponent(Component component)
        {

            if (Components.ContainsKey(component.Guid))
            {
                Components.Remove(component.Guid);
            }
        }

        public void QueueAction(Action func)
        {
            actionQueue.Enqueue(func);
        }

        public void SimulateCircuit()
        {
            while (actionQueue.Count > 0)
            {
                actionQueue.Dequeue()();
            }

            if (Components.Count <= 1)
            {
                return;
            }

            try
            {
                foreach (Component component in Components.Values)
                {
                    component.Base.ComponentUpdate();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Destroy()
        {
            destroy = true;
        }

        public bool IsDestroyed()
        {
            return destroy;
        }
    }
}