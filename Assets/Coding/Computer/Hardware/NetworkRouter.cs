using UnityEngine;
using System.Collections.Generic;

namespace Computers
{
    public class NetworkRouter : MonoBehaviour {
        private object packetLock = new object();
        public WideAreaNetwork wan;
        public NetworkCard wanCard;
        public string guid;
        public Dictionary<string, NetworkCard> mappedCards = new Dictionary<string, NetworkCard>();
        private Queue<Packet> receivedPackets = new Queue<Packet>();
        private List<NetworkCard> nonReadyCards = new List<NetworkCard>();

        public void RegisterDomain(string domain, string guid)
        {

        }

        public void AddNetworkCard(NetworkCard card)
        {
            if (card.connectedNetworkCard.guid == string.Empty)
            {
                nonReadyCards.Add(card);
                return;
            }
            Debug.Log("Router Connected To: " + card.connectedNetworkCard.guid);
            mappedCards[card.connectedNetworkCard.guid] = card;
            card.SetRouter(this);
        }

        public bool ContainsAddress(string guid)
        {
            return guid == this.guid ? true : mappedCards.ContainsKey(guid);
        }

        public Packet GetNextPacket()
        {
            if (receivedPackets.Count > 0) return receivedPackets.Dequeue();
            return null;
        }

        void Update() {
            if (wanCard == null && wan != null)
            {
                wanCard = gameObject.AddComponent<NetworkCard>();
                wanCard.router = this;
                wanCard.guid = guid;
                NetworkCard wanCardConnection = wan.gameObject.AddComponent<NetworkCard>();
                wanCardConnection.ConnectTo(wanCard);
                wan.routers.Add(wanCardConnection);
            }

            if (nonReadyCards.Count > 0)
            {
                for (int i = 0; i < nonReadyCards.Count; i ++)
                {
                    if (nonReadyCards[i].connectedNetworkCard != null && nonReadyCards[i].connectedNetworkCard.guid != string.Empty)
                    {
                        AddNetworkCard(nonReadyCards[i]);
                        nonReadyCards.RemoveAt(i);
                    }
                }
            }
            else
            {
                if (wanCard != null)
                {
                    if (wanCard.numberOfPacketsRecieved > 0)
                    {
                        Packet packet = wanCard.GetNextPacket();

                        if (packet.destination == guid)
                        {
                            lock (packetLock)
                            {
                                receivedPackets.Enqueue(packet);
                            }
                        }
                        else
                        {
                            if (!mappedCards.ContainsKey(packet.destination))
                            {
                                if (wanCard != null) wanCard.SendPacket(packet);
                            }
                            else
                            {
                                mappedCards[packet.destination].SendPacket(packet);
                            }
                        }
                    }
                }

                foreach (KeyValuePair<string, NetworkCard> card in mappedCards)
                {
                    if (card.Value.numberOfPacketsRecieved > 0)
                    {
                        Packet packet = card.Value.GetNextPacket();

                        if (packet.destination == guid)
                        {
                            lock (packetLock)
                            {
                                receivedPackets.Enqueue(packet);
                            }
                        }
                        else
                        {
                            if (!mappedCards.ContainsKey(packet.destination))
                            {
                                if (wanCard != null) wanCard.SendPacket(packet);
                            }
                            else
                            {
                                mappedCards[packet.destination].SendPacket(packet);
                            }
                        }
                    }
                }
            }
        }
    }
}