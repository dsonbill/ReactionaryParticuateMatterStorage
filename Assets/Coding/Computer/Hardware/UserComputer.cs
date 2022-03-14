using UnityEngine;
using Computers.Hardware;
using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Computers
{
    public static class UserComputerRegistry
    {
        public static Dictionary<Guid, UserComputer> computers = new Dictionary<Guid, UserComputer>();
    }

    [AddComponentMenu("Computers/User Computer")]
    public class UserComputer : MonoBehaviour
    {
        public class UserComputerFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<UserComputer> computer;

            [MoonSharpHidden]
            public UserComputerFunctions(Misc.VarRef<UserComputer> computer)
            {
                this.computer = computer;
            }

            public string GetGuid()
            {
                return computer.Value.guid;
            }
        }

        public Motherboard mb;
        public TransmissionInterface transmissionInterface;
        public TerminalConnector terminalConnector;
        public UnityEngine.UI.Button powerButton;
        public UnityEngine.UI.Image powerButtonImage;
        public Color powerOnColor = Color.green;
        public Color powerOffColor = Color.red;
        public float cutoffVoltage = 90;
        public float criticalVoltage = 300;
        public bool autoConfigure = true;
        public bool motherboardInstalled = false;
        public bool processorInstalled = false;
        public int numberOfGPUs = 1;
        public string guid;
        

        public bool runBootFile;
        public string bootFile = "Scripts/boot.lua";
        public bool runDebugFile;
        public string debugFile = "Scripts/debug.lua";

        bool built;

        void Start()
        {
            if (!autoConfigure)
            {
                powerButton.onClick.AddListener(() => { PowerKeyToggle(); });
            }
            else
            {
                motherboardInstalled = true;
                processorInstalled = true;
                InstallMotherboard(new Motherboard());
                InstallProcessor(new Processor());
                InstallMemstick(0, new Memory());
            }

            if (guid == string.Empty) guid = Guid.NewGuid().ToString();

            UserComputerRegistry.computers.Add(Guid.Parse(guid), this);

            if (terminalConnector != null)
            {
                terminalConnector.TerminalCable.broadcast = TerminalBroadcast;
            }

            if (autoConfigure) PowerKeyToggle();
        }

        public void PowerKeyToggle()
        {
            if (!built) Build();

            mb.ToggleMainPower();

            if (mb.mainPower)
            {
                mb.SetProcessorGlobal(ProcessorOperationMode.Kernel, "Computer", new UserComputerFunctions(new Misc.VarRef<UserComputer>(() => this, (UserComputer comp) => { })));
                powerButtonImage.color = powerOnColor;
            }
            else
            {
                powerButtonImage.color = powerOffColor;
            }

            if (runBootFile) mb.AddKernelOperation(ProcessorFileLoader.GetFile(bootFile));
            if (runDebugFile) mb.AddKernelOperation(ProcessorFileLoader.GetFile(debugFile));
        }

        public void Build()
        {
            if (!motherboardInstalled || !processorInstalled) return;
            //if (!autoConfigure)
            //{
                //if ((powerSocket.wire.implementation as Wiring.IBasicWireImplementation).lastVoltage < cutoffVoltage ||
                //    (powerSocket.wire.implementation as Wiring.IBasicWireImplementation).lastVoltage > criticalVoltage) return;
            //}

            for (int i = 0; i < numberOfGPUs; i++)
            {
                GraphicsCard gpu = new GraphicsCard();
                
                gpu.onAddOperation += (ProcessorOperationMode mode, string script) =>
                {
                    switch(mode)
                    {
                        case ProcessorOperationMode.Kernel:
                            mb.AddKernelOperation(script);
                            break;
                        case ProcessorOperationMode.User:
                            mb.AddUserOperation(script);
                            break;
                    }
                };
                mb.processor.onScriptException += gpu.AddLineToTerminal;

                mb.InstallGraphicsCard(gpu);
            }

            foreach (NetworkCard card in GetComponents<NetworkCard>())
            {
                card.SetGuid(guid);
                mb.InstallNetworkCard(card);
            }

            if (GetComponent<NetworkRouter>() != null)
            {
                NetworkRouter router = GetComponent<NetworkRouter>();
                router.guid = guid;
                foreach (NetworkCard card in GetComponents<NetworkCard>())
                {
                    router.AddNetworkCard(card);
                }
                mb.InstallRouter(router);
            }

            built = true;
        }

        public void InstallMotherboard(Motherboard motherboard)
        {
            mb = motherboard;
            motherboardInstalled = true;
        }

        public void UninstallMotherboard()
        {
            mb = null;
            motherboardInstalled = false;
        }

        public void InstallProcessor(Processor processor)
        {
            mb.InstallProcessor(processor);
            processorInstalled = true;
        }

        public void UninstallProcessor()
        {
            mb.UninstallProcessor();
            processorInstalled = false;
        }

        public void InstallMemstick(int slot, Memory memory)
        {
            mb.InstallMemoryStick(slot, memory);
        }

        public void UninstallMemstick(int slot)
        {
            mb.UninstallMemoryStick(slot);
        }

        public void InstallHarddrive(HardDrive drive)
        {
            drive.Initialize();
            mb.InstallHarddrive(drive);
        }

        public void UninstallHarddrive(int driveNumber)
        {
            mb.UninstallHarddrive(driveNumber);
        }

        public void InstallTransmissionInterface(TransmissionInterface transmissionInterface)
        {
            mb.InstallTransmittionOutput(transmissionInterface);
        }

        public void UninstallTransmissionInterface()
        {
            mb.UninstallTransmittionOutput();
        }

        public Guid TerminalBroadcast()
        {
            if (mb != null && mb.mainPower)
                return Guid.Parse(guid);
            else
                return Guid.Empty;
        }

        void OnDestroy()
        {
            if (motherboardInstalled && mb.mainPower) mb.ToggleMainPower();
        }
    }
}