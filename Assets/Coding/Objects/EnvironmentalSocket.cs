using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace Gameplay
{
    [AddComponentMenu("EnvironmentalSocket")]
    public class EnvironmentalSocket : Connector
    {
        public void Awake()
        {
            connectCondition = (Connector socket) =>
            {
                return (socket as EnvironmentalSocket) != null;
            };
        }

        public override void Disconnect()
        {
            //Environment connectors do not come apart
            //return; // Or at least they won't in the future <- Probably a lie
            base.Disconnect();
        }
    }
}