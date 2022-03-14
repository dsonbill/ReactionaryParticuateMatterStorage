using System;
using System.Collections.Generic;

namespace DarkGraph
{
    public class Connection
    {
        //Nice to have some pretty names for the circuit nets
        private static int freeID = 0;
        public string name;
        public List<Terminal> terminals;

        public Connection()
        {
            terminals = new List<Terminal>();
            UpdateName();
        }

        public void UpdateName()
        {
            bool setName = false;
            foreach (Terminal t in terminals)
            {
                if (t.name != null)
                {
                    setName = true;
                    name = t.name;
                }
            }
            if (!setName)
            {
                name = "connection " + freeID;
                freeID++;
            }
        }
    }

    public class Terminal
    {
        public Terminal(Component parent, int terminalId)
        {
            component = parent;
            this.terminalId = terminalId;
        }
        public Component component;

        public Connection connection;
        public string name;
        public int terminalId;

        public override string ToString()
        {
            return component.name + " [" + terminalId + "]";
        }

        public void Connect(Terminal toTerminal)
        {
            //Console.WriteLine("Connecting " + this + " to " + toTerminal);
            bool added = false;
            //Both null
            if (!added && connection == null && toTerminal.connection == null)
            {
                added = true;
                connection = new Connection();
                toTerminal.connection = connection;
                connection.terminals.Add(this);
                connection.terminals.Add(toTerminal);
            }
            //We have a terminal, they don't
            if (!added && connection != null && toTerminal.connection == null)
            {
                added = true;
                toTerminal.connection = connection;
                connection.terminals.Add(toTerminal);
            }
            //They have a terminal, we don't
            if (!added && connection == null && toTerminal.connection != null)
            {
                added = true;
                connection = toTerminal.connection;
                connection.terminals.Add(this);
            }
            //Both terminals have a connection, merge them if different
            if (!added && connection != null && toTerminal.connection != null && connection != toTerminal.connection)
            {
                added = true;
                foreach (Terminal t in toTerminal.connection.terminals)
                {
                    connection.terminals.Add(t);
                    t.connection = connection;
                }
            }
            connection.UpdateName();
        }

        public void Disconnect()
        {
            connection.terminals.Remove(this);
            //We disconnected from a connection with only one other component, delete it's connection too.
            if (connection.terminals.Count == 1)
            {
                connection.terminals[0].connection = null;
            }
            connection = null;
        }

        public void Split(List<Terminal> keepTerminals)
        {
            Connection newConnection = null;
            for (int i = connection.terminals.Count - 1; i >= 0; i--)
            {
                Terminal t = connection.terminals[i];
                //Don't move our own terminal!
                if (t == this)
                {
                    continue;
                }
                //Split into new connection
                if (!keepTerminals.Contains(t))
                {
                    if (newConnection == null)
                    {
                        newConnection = new Connection();
                    }
                    t.connection = newConnection;
                    newConnection.terminals.Add(t);
                    connection.terminals.RemoveAt(i);
                }
            }
            //New split only has one terminal, delete it
            if (newConnection != null)
            {
                if (newConnection.terminals.Count == 1)
                {
                    newConnection.terminals[0].connection = null;
                }
                else
                {
                    newConnection.UpdateName();
                }
            }
            connection.UpdateName();
        }
    }

    public class Component
    {
        public Component(string name, int terminals)
        {
            this.name = name;
            this.terminals = new Terminal[terminals];
            for (int i = 0; i < terminals; i++)
            {
                this.terminals[i] = new Terminal(this, i);
            }
        }
        public string name;
        Terminal[] terminals;

        public override string ToString()
        {
            return name;
        }

        public Terminal this[int i]
        {
            get
            {
                return terminals[i];
            }
        }

        public void SetTerminalName(int id, string name)
        {
            terminals[id].name = name;
            Connection c = terminals[id].connection;
            if (c != null)
            {
                if (name != null)
                {
                    c.name = name;
                }
                else
                {
                    c.UpdateName();
                }
            }
        }

        public void Connect(int fromTerminal, Component toComponent, int toTerminal)
        {
            terminals[fromTerminal].Connect(toComponent.terminals[toTerminal]);
        }
        public void Disconnect(int terminal)
        {
            terminals[terminal].Disconnect();
        }

        public void Split(int terminal, List<Component> keepComponents)
        {
            List<Terminal> keepTerminals = new List<Terminal>();
            Terminal t = terminals[terminal];
            if (t.connection == null)
            {
                return;
            }
            foreach (Terminal t2 in t.connection.terminals)
            {
                //Internal call stops us splitting our own terminal, so we can skip it
                if (t2 == t)
                {
                    continue;
                }
                if (keepComponents.Contains(t2.component))
                {
                    keepTerminals.Add(t2);
                }
            }
            Split(terminal, keepTerminals);
        }

        public void Split(int terminal, List<Terminal> keepTerminals)
        {
            terminals[terminal].Split(keepTerminals);
        }

        public bool SameCircuit(Component component)
        {
            Dictionary<string, Component> components = GetCircuitList();
            foreach (string name in component.GetCircuitList().Keys)
            {
                if (components.ContainsKey(name))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        public Dictionary<string, Component> GetCircuitList()
        {
            Dictionary<string, Component> circuitList = new Dictionary<string, Component>();
            GetCircuitList(ref circuitList);
            return circuitList;
        }

        private void GetCircuitList(ref Dictionary<string, Component> circuitList)
        {
            if (circuitList.ContainsKey(this.name))
            {
                return;
            }
            circuitList.Add(this.name, this);
            for (int i = 0; i < terminals.Length; i++)
            {
                Terminal t = terminals[i];
                Connection c = t.connection;
                if (c != null)
                {
                    foreach (Terminal t2 in t.connection.terminals)
                    {
                        Component connectedComponent = t2.component;
                        if (connectedComponent != this)
                        {
                            connectedComponent.GetCircuitList(ref circuitList);
                        }
                    }
                }
            }
        }

        public List<ConnectionListEntry> GetConnectionList()
        {
            Dictionary<string, Component> components = new Dictionary<string, Component>();
            GetCircuitList(ref components);

            List<ConnectionListEntry> connectionList = new List<ConnectionListEntry>();
            foreach (Component component in components.Values)
            {
                ConnectionListEntry cle = new ConnectionListEntry()
                {
                    ComponentName = component.name,
                    ConnectionName = new string[component.terminals.Length],
                    Connected = new bool[component.terminals.Length]
                };

                for (int i = 0; i < component.terminals.Length; i++)
                {
                    if (component.terminals[i].connection != null)
                    {
                        cle.Connected[i] = true;
                        cle.ConnectionName[i] = component.terminals[i].connection.name;
                        continue;
                    }
                    cle.Connected[i] = false;
                }
                connectionList.Add(cle);
            }

            return connectionList;
        }

        public void PrintCircuit()
        {
            string lineEntry = "";
            foreach (ConnectionListEntry entry in GetConnectionList())
            {
                for (int i = 0; i < entry.ConnectionName.Length; i++)
                {
                    if (entry.Connected[i])
                    {
                        //lineEntry += (GameComponent.Registry[entry.ComponentName].gameObject.name + " [" + i + "] is connected to " + entry.ConnectionName[i]) + "\n";
                        continue;
                    }
                    //lineEntry += (GameComponent.Registry[entry.ComponentName].gameObject.name + " [" + i + "] is disconnected") + "\n";
                }
            }
            // lineEntry.TrimEnd('\n'); Who Cares
            UnityEngine.Debug.Log(lineEntry);
        }
    }

    public class ConnectionListEntry
    {
        public string ComponentName;
        public string[] ConnectionName;
        public bool[] Connected;
    }
}