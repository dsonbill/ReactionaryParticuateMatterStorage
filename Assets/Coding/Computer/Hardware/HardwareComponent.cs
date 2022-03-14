using System.Collections.Generic;
using UnityEngine;

namespace Computers
{
    [AddComponentMenu("Computers/Hardware Component")]
    public class HardwareComponent : Connector
    {
        public enum HardwareType
        {
            Motherboard,
            Processor,
            Memory,
            Harddrive,
            ExpansionCard,
            Transmission
        }

        public HardwareType hardwareType;
        public UserComputer computer;
        public List<HardwareComponent> componentConnectors = new List<HardwareComponent>();
        public int memorySlot;
        public int driveNumber;
        public string driveGuid;
        public Hardware.TransmissionInterface transmissionInterface;

        public void Awake()
        {
            connectCondition = (Connector socket) =>
            {
                //Debug.Log(this.hardwareType + " + " + (socket as HardwareComponent).hardwareType);
                if ((socket as HardwareComponent) == null) return false;
                if (hardwareType != (socket as HardwareComponent).hardwareType) return false;

                if (hardwareType != HardwareType.Motherboard)
                {
                    if ((socket as HardwareComponent).computer == null) return false;
                    if ((socket as HardwareComponent).computer.mb.mainPower) return false;
                }
                else if (socket.GetComponent<UserComputer>().motherboardInstalled) return false;

                return true;
            };
        }

        public override void Connect(Connector socket)
        {
            base.Connect(socket);
            switch(hardwareType)
            {
                case HardwareType.Motherboard:
                    computer = (socket as HardwareComponent).computer;
                    computer.InstallMotherboard(new Hardware.Motherboard());
                    foreach (HardwareComponent connector in componentConnectors)
                    {
                        connector.computer = computer;
                    }
                    break;

                case HardwareType.Processor:
                    (socket as HardwareComponent).computer.InstallProcessor(new Hardware.Processor());
                    break;

                case HardwareType.Memory:
                    (socket as HardwareComponent).computer.InstallMemstick((socket as HardwareComponent).memorySlot, new Hardware.Memory());
                    break;

                case HardwareType.Harddrive:
                    Hardware.HardDrive drive = new Hardware.HardDrive((socket as HardwareComponent).driveNumber, driveGuid);
                    (socket as HardwareComponent).computer.InstallHarddrive(drive);
                    break;

                case HardwareType.Transmission:
                    (socket as HardwareComponent).computer.InstallTransmissionInterface(transmissionInterface);
                    break;

            }
        }

        public override void Disconnect()
        {
            if (hardwareType == HardwareType.Motherboard)
            {
                if (computer.mb.mainPower) return;
                if (computer.mb.processor != null) return;
                if (computer.mb.memsticks.Count > 0) return;
                if (computer.mb.harddrives.Count > 0) return;
                if (computer.mb.transmissionOutput != null) return;
            }
            else if ((connectedConnector as HardwareComponent).computer.mb.mainPower) return;

            switch (hardwareType)
            {
                case HardwareType.Motherboard:
                    computer.UninstallMotherboard();
                    computer = null;
                    foreach (HardwareComponent connector in componentConnectors) connector.computer = null;
                    break;

                case HardwareType.Processor:
                    (connectedConnector as HardwareComponent).computer.UninstallProcessor();
                    break;

                case HardwareType.Memory:
                    (connectedConnector as HardwareComponent).computer.UninstallMemstick((connectedConnector as HardwareComponent).memorySlot);
                    break;

                case HardwareType.Harddrive:
                    (connectedConnector as HardwareComponent).computer.UninstallHarddrive((connectedConnector as HardwareComponent).driveNumber);
                    break;

                case HardwareType.Transmission:
                    (connectedConnector as HardwareComponent).computer.UninstallTransmissionInterface();
                    break;
            }

            base.Disconnect();
        }
    }
}