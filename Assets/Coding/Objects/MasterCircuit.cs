using System.Threading;
using System.Collections.Generic;
using System;
using UnityEngine;
using Threads;

namespace Transmission
{

    public class MasterCircuit : MonoBehaviour
    {
        public static MasterCircuit Instance { get; private set; }

        public Dictionary<Guid, Circuit> Circuits;

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            Circuits = new Dictionary<Guid, Circuit>();
        }

        void OnApplicationQuit()
        {
            foreach (Circuit circuit in Circuits.Values)
            {
                circuit.Destroy();
            }
        }

        public void ConnectComponents(Component a, Component b)
        {
            if (a.Circuit == Guid.Empty && b.Circuit == Guid.Empty)
            {
                //Need a new circuit
                AddComponentsToNewCircuit(a, b);
            }
            else if (a.Circuit == Guid.Empty && b.Circuit != Guid.Empty)
            {
                //Add to b
                AddComponentToCircuit(b.Circuit, a);
            }
            else if (a.Circuit != Guid.Empty && b.Circuit == Guid.Empty)
            {
                //Add to a
                AddComponentToCircuit(a.Circuit, b);
            }
            else if (a.Circuit == b.Circuit)
            {
                //Same Circuit
            }
            else
            {
                //Two separate circuits
                MergeCircuits(a, b);
            }
            UpdateConnections(a);
        }

        public void DisconnectComponents(Component a, Component b)
        {

            if (!a.DarkComponent.SameCircuit(b.DarkComponent))
            {
                Dictionary<string, DarkGraph.Component> circuitList = a.DarkComponent.GetCircuitList();
                List<string> keyList = new List<string>();
                foreach (string guid in circuitList.Keys)
                {
                    //Circuits[a.Circuit].RemoveComponent(Component.Registry[guid].Component);
                    RemoveComponentFromCircuit(Component.Registry[guid]);
                    keyList.Add(guid);
                }

                for (int i = 1; i < circuitList.Count; i++)
                {
                    if (i == 1)
                    {
                        AddComponentsToNewCircuit(Component.Registry[keyList[0]], Component.Registry[keyList[1]]);
                    }
                    else
                    {
                        AddComponentToCircuit(Component.Registry[keyList[0]].Circuit, Component.Registry[keyList[i]]);
                    }
                }
                UpdateConnections(a);
            }
            UpdateConnections(b);
        }

        private void UpdateConnections(Component component)
        {
            if (component.Circuit == Guid.Empty)
            {
                //component.Component.Connect(component.Pins.ToArray());
                //GameSystems.UnityOperator.instance.QueueOperation(new GameSystems.UnityOperationWrapper((obj) => {component.DarkComponent.PrintCircuit();}));
                return;
            }

            List<DarkGraph.ConnectionListEntry> connections = component.DarkComponent.GetConnectionList();
            foreach (DarkGraph.ConnectionListEntry entry in connections)
            {
                string[] connectionPins = new string[entry.ConnectionName.Length];
                for (int i = 0; i < entry.ConnectionName.Length; i++)
                {
                    if (entry.Connected[i])
                    {
                        connectionPins[i] = entry.ConnectionName[i];
                    }
                    else
                    {
                        connectionPins[i] = Component.Registry[entry.ComponentName].Pins[i];
                    }
                }
            }
            //GameSystems.UnityOperator.instance.QueueOperation(new GameSystems.UnityOperationWrapper((obj) => {component.DarkComponent.PrintCircuit();}));
        }

        private void MergeCircuits(Component a, Component b)
        {
            if (Circuits[a.Circuit].Components.Count > Circuits[b.Circuit].Components.Count)
            {
                Guid circuitID = a.Circuit;
                foreach (string guid in b.DarkComponent.GetCircuitList().Keys)
                {
                    RemoveComponentFromCircuit(Component.Registry[guid]);
                    AddComponentToCircuit(circuitID, Component.Registry[guid]);
                }
            }
            else
            {
                Guid circuitID = b.Circuit;
                foreach (string guid in a.DarkComponent.GetCircuitList().Keys)
                {
                    RemoveComponentFromCircuit(Component.Registry[guid]);
                    AddComponentToCircuit(circuitID, Component.Registry[guid]);
                }
            }
        }

        private void AddComponentToCircuit(Guid guid, Component component)
        {
            Circuits[guid].QueueAction(() => { Circuits[guid].AddComponent(component); });
            component.Circuit = guid;
        }

        private void RemoveComponentFromCircuit(Component component)
        {
            if (component.Circuit == Guid.Empty)
            {
                return;
            }
            Circuits[component.Circuit].QueueAction(() => { Circuits[component.Circuit].RemoveComponent(component); });
            if (Circuits[component.Circuit].Components.Count <= 1)
            {
                Guid id = component.Circuit;
                foreach (Component componentY in Circuits[id].Components.Values)
                {
                    Circuits[id].QueueAction(() => { Circuits[id].RemoveComponent(Component.Registry[componentY.Guid.ToString()]); });
                    Component.Registry[componentY.Guid.ToString()].Circuit = Guid.Empty;
                }
                Circuits[id].Destroy();
                Circuits.Remove(id);
            }
            component.Circuit = Guid.Empty;
        }

        private void AddComponentsToNewCircuit(Component a, Component b)
        {
            Circuit circuit = new Circuit();
            Circuits.Add(circuit.Guid, circuit);
            AddComponentToCircuit(circuit.Guid, a);
            AddComponentToCircuit(circuit.Guid, b);
            CircuitThread.Instance.AddCircuit(circuit);
        }
    }
}