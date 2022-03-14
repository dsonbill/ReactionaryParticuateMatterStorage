using System;
using System.Collections.Generic;
using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine.UI;
using UnityEngine;

namespace Computers.Hardware
{
    public class Motherboard
    {
        public class ProcessorFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<Processor> processor;

            [MoonSharpHidden]
            public ProcessorFunctions(Misc.VarRef<Processor> processor)
            {
                this.processor = processor;
            }

            public void Add(ProcessorOperation operation)
            {
                ProcessorOperator.Instance.QueueOperation(operation);
            }

            public ProcessorOperation Kernel(string script)
            {
                return new ProcessorOperation(script, ProcessorOperationMode.Kernel, processor.Value);
            }

            public ProcessorOperation User(string script)
            {
                return new ProcessorOperation(script, ProcessorOperationMode.User, processor.Value);
            }
        }

        public class MemoryFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<Dictionary<int, Memory>> memsticks;

            [MoonSharpHidden]
            public MemoryFunctions(Misc.VarRef<Dictionary<int, Memory>> memsticks)
            {
                this.memsticks = memsticks;
            }

            public void Write(int memstick, string key, object value)
            {
                memsticks.Value[memstick].SetValue(key, value);
            }

            public object Read(int memstick, string key)
            {
                return memsticks.Value[memstick].GetValue(key);
            }
        }

        public class HardDriveFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<Dictionary<int, HardDrive>> hardDrives;

            [MoonSharpHidden]
            public HardDriveFunctions(Misc.VarRef<Dictionary<int, HardDrive>> hardDrives)
            {
                this.hardDrives = hardDrives;
            }

            public HardDrive Get(int drive)
            {
                if (hardDrives.Value.ContainsKey(drive)) return hardDrives.Value[drive];
                return null;
            }

            public int Count()
            {
                return hardDrives.Value.Count;
            }
        }

        public class NetworkFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<List<NetworkCard>> networkCards;

            [MoonSharpHidden]
            Misc.VarRef<NetworkRouter> router;

            [MoonSharpHidden]
            public NetworkFunctions(Misc.VarRef<List<NetworkCard>> networkCards, Misc.VarRef<NetworkRouter> router)
            {
                this.networkCards = networkCards;
                this.router = router;
            }

            public Packet Packet(string data, string origin, string destination)
            {
                return new Packet(data, origin, destination);
            }

            public int Count()
            {
                return networkCards.Value.Count;
            }

            public Packet Read(int card)
            {
                return networkCards.Value[card].GetNextPacket();
            }

            public void Send(int card, Packet packet)
            {
                networkCards.Value[card].SendPacket(packet);
            }

            public bool RouterInstalled()
            {
                return router != null;
            }

            public Packet ReadRouterPacket()
            {
                if (router == null) return null;
                return router.Value.GetNextPacket();
            }
        }

        public class GraphicsFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<List<GraphicsCard>> cards;

            [MoonSharpHidden]
            Misc.VarRef<int> activeGpu; 

            [MoonSharpHidden]
            public GraphicsFunctions(Misc.VarRef<List<GraphicsCard>> cards, Misc.VarRef<int> activeGpu)
            {
                this.cards = cards;
                this.activeGpu = activeGpu;
            }

            public int Active()
            {
                return activeGpu.Value;
            }

            public void Set(int gpu)
            {
                activeGpu.Value = gpu;
            }

            public void Clear()
            {
                cards.Value[activeGpu.Value].ClearTerminal();
            }

            public void Console(string input)
            {
                cards.Value[activeGpu.Value].AddLineToTerminal(input);
            }
        }

        public class TransmissionFunctions
        {
            [MoonSharpHidden]
            Misc.VarRef<TransmissionInterface> tInterface;

            [MoonSharpHidden]
            public TransmissionFunctions(Misc.VarRef<TransmissionInterface> iface)
            {
                this.tInterface = iface;
            }

            public double Receive(int transmitter)
            {
                return tInterface.Value.Transmitters[transmitter].rx;
            }

            public void Transmit(int connector, double value)
            {
                tInterface.Value.Output[connector] = value;
            }
        }

        
        public Processor processor;
        public Dictionary<int, Memory> memsticks = new Dictionary<int, Memory>();
        public Dictionary<int, HardDrive> harddrives = new Dictionary<int, HardDrive>();
        public List<GraphicsCard> graphicsCards = new List<GraphicsCard>();
        public List<NetworkCard> networkCards = new List<NetworkCard>();
        public NetworkRouter router;
        public TransmissionInterface transmissionOutput;

        public bool mainPower { get; private set; }

        private int _activeGPU = 0;
        public int activeGPU
        {
            get
            {
                return _activeGPU;
            }
            set
            {
                if (value < graphicsCards.Count && value > 0)
                {
                    _activeGPU = value;
                    return;
                }
                _activeGPU = 0;
            }
        }

        public bool ToggleMainPower()
        {
            if (processor == null)
            {
                return false;
            }

            mainPower = ToggleProcessorPower();
            return mainPower;
        }

        private bool ToggleProcessorPower()
        {
            return processor.TogglePower();
        }

        public void AddKernelOperation(string script)
        {
            ProcessorOperator.Instance.QueueOperation(new ProcessorOperation(script, ProcessorOperationMode.Kernel, processor));
        }

        public void AddUserOperation(string script)
        {
            ProcessorOperator.Instance.QueueOperation(new ProcessorOperation(script, ProcessorOperationMode.User, processor));
        }

        

        public void InstallProcessor(Processor processor)
        {
            this.processor = processor;
            //Register Kernel Functions
            this.processor.SetKernelGlobal("CPU", new ProcessorFunctions(new Misc.VarRef<Processor>(() => this.processor, (Processor p) => { })));
            this.processor.SetKernelGlobal("MEM", new MemoryFunctions(new Misc.VarRef<Dictionary<int, Memory>>(() => memsticks, (Dictionary<int, Memory> m) => { })));
            this.processor.SetKernelGlobal("HDD", new HardDriveFunctions(new Misc.VarRef<Dictionary<int, HardDrive>>(() => harddrives, (Dictionary<int, HardDrive> drives) => { })));
            this.processor.SetKernelGlobal("NET", new NetworkFunctions(new Misc.VarRef<List<NetworkCard>>(() => networkCards, (List<NetworkCard> cards) => { }), new Misc.VarRef<NetworkRouter>(() => router, (NetworkRouter rowter) => { })));
            this.processor.SetKernelGlobal("GPU", new GraphicsFunctions(new Misc.VarRef<List<GraphicsCard>>(() => graphicsCards, (List<GraphicsCard> cards) => { }), new Misc.VarRef<int> (() => { return activeGPU; }, (gpu) => { activeGPU = gpu; })));
            this.processor.SetKernelGlobal("IO", new TransmissionFunctions(new Misc.VarRef<TransmissionInterface>(() => transmissionOutput, (TransmissionInterface tInterface) => { })));
        }

        public bool UninstallProcessor()
        {
            processor = null;
            return true;
        }

        public void InstallMemoryStick(int slot, Memory memory)
        {
            memsticks[slot] = memory;
        }

        public void UninstallMemoryStick(int slot)
        {
            memsticks.Remove(slot);
        }

        public void InstallHarddrive(HardDrive drive)
        {
            harddrives[drive.driveNumber] = drive;
        }

        public void UninstallHarddrive(int driveNumber)
        {
            harddrives.Remove(driveNumber);
        }

        public void InstallNetworkCard(NetworkCard card)
        {
            networkCards.Add(card);
        }

        public void UninstallNetworkCard(NetworkCard card)
        {
            networkCards.Remove(card);
        }

        public void InstallGraphicsCard(GraphicsCard card)
        {
            graphicsCards.Add(card);
        }

        public void UninstallGraphicsCard(GraphicsCard card)
        {
            graphicsCards.Remove(card);
        }

        public void InstallRouter(NetworkRouter router)
        {
            this.router = router;
        }

        public void UninstallRouter(NetworkRouter router)
        {
            this.router = null;
        }

        public void InstallTransmittionOutput(TransmissionInterface transmissionOutput)
        {
            this.transmissionOutput = transmissionOutput;
        }

        public void UninstallTransmittionOutput()
        {
            this.transmissionOutput = null;
        }

        public void SetProcessorGlobal<T>(ProcessorOperationMode mode, string name, T value)
        {
            try
            {
                if (mode == ProcessorOperationMode.Kernel) processor.SetKernelGlobal<T>(name, value);
                else if (mode == ProcessorOperationMode.User) processor.SetUserGlobal<T>(name, value);
            }
            catch (Exception e)
            {
                Debug.Log("Exception while setting Processor global");
            }
        }
    }
}