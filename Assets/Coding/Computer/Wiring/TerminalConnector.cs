using UnityEngine;
using System;

namespace Computers
{
    [AddComponentMenu("Computers/Terminal Connector")]
    public class TerminalConnector : Connector
    {
        public TerminalCable TerminalCable;

        public Guid txGuid;

        public void Awake()
        {
            connectCondition = (Connector socket) =>
            {
                if ((socket as TerminalConnector) != null) return true;
                return false;
            };

            if (TerminalCable == null)
            {
                TerminalCable = GetComponent<TerminalCable>();
            }
            if (TerminalCable == null)
            {
                TerminalCable = transform.parent.GetComponent<TerminalCable>();
            }

            TerminalCable.connectors.Add(this);
        }
    }
}