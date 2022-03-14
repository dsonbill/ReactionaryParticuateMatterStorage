using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Computers
{
    public class NetworkCard : MonoBehaviour
    {
        private object netCardLock = new object();
        public string guid;
        public NetworkCard connectedNetworkCard;
        private Queue<Packet> packets = new Queue<Packet>();
        public int numberOfPackets
        {
            get
            {
                return packets.Count;
            }
        }

        private Queue<Packet> packetsRecieved = new Queue<Packet>();
        public int numberOfPacketsRecieved
        {
            get
            {
                return packetsRecieved.Count;
            }
        }

        public NetworkRouter router;
        public bool isRouter
        {
            get
            {
                return router != null;
            }
        }

        public void SetRouter(NetworkRouter router)
        {
            this.router = router;
        }

        public void ConnectTo(NetworkCard networkCard)
        {
            connectedNetworkCard = networkCard;
        }

        public string GetConnectedGuid()
        {
            return connectedNetworkCard.guid;
        }

        public void SetGuid(string guid)
        {
            lock (netCardLock)
            {
                this.guid = guid;
            }
        }

        public void SendPacket(Packet packet)
        {
            lock (netCardLock)
            {
                packets.Enqueue(packet);
            }
        }

        public void ReceivePacket(Packet packet)
        {
            lock (netCardLock)
            {
                packetsRecieved.Enqueue(packet);
            }
        }

        public Packet GetNextPacket()
        {
            lock (netCardLock)
            {
                if (packetsRecieved.Count > 0) return packetsRecieved.Dequeue();
                return null;
            }
        }

        public void Update()
        {
            lock (netCardLock)
            {
                if (connectedNetworkCard != null && connectedNetworkCard.connectedNetworkCard == null) connectedNetworkCard.connectedNetworkCard = this;

                if (connectedNetworkCard != null && packets.Count > 0)
                {
                    connectedNetworkCard.ReceivePacket(packets.Dequeue());
                }
            }
        }

        void OnDrawGizmos()
        {
            if (connectedNetworkCard != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, connectedNetworkCard.gameObject.transform.position);
            }

            if (numberOfPacketsRecieved > 0)
            {
                Gizmos.color = new Color(Mathf.Min(0.1f * numberOfPacketsRecieved, 1f), 0.5f, 1);
                Gizmos.DrawCube(transform.position, new Vector3(1.2f,1.2f, 1.2f));
            }
        }
    }

    public class Packet
    {
        public string data { get; private set; }
        public string origin { get; private set; }
        public string destination { get; private set; }

        public Packet(string data, string origin, string destination)
        {
            this.data = data;
            this.origin = origin;
            this.destination = destination;
        }

        public override string ToString()
        {
            return "Data: " + data + " Origin: " + origin + " Dest: " + destination;
        }
    }
}