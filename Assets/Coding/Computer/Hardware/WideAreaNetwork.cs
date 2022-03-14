using UnityEngine;
using System.Collections.Generic;

namespace Computers
{
    public class WANCollective
    {
        public static List<WideAreaNetwork> wanCards = new List<WideAreaNetwork>();

        public static void SendToWan(Packet packet)
        {
            foreach (WideAreaNetwork net in wanCards)
            {
                if (net.SendToRouter(packet))
                {
                    return;
                }
            }
            Debug.Log("Could not find address on network: " + packet.destination);
        }
    }

    public class WideAreaNetwork : MonoBehaviour {

        public List<NetworkCard> routers = new List<NetworkCard>();

        void Start()
        {
            WANCollective.wanCards.Add(this);
        }

        public bool SendToRouter(Packet packet)
        {
            bool foundRouter = false;
            foreach (NetworkCard card in routers)
            {
                if (card.connectedNetworkCard.router.ContainsAddress(packet.destination))
                {
                    card.SendPacket(packet);
                    foundRouter = true;
                    break;
                }
            }
            return foundRouter;
        }

        // Update is called once per frame
        void Update()
        {
            foreach (NetworkCard card in routers)
            {
                if (card.numberOfPacketsRecieved > 0)
                {
                    while (card.numberOfPacketsRecieved > 0)
                    {
                        Packet packet = card.GetNextPacket();
                        bool foundRouter = SendToRouter(packet);
                        
                        if (!foundRouter)
                        {
                            WANCollective.SendToWan(packet);
                        }
                    }
                }
            }
        }
    }
}