using MoonSharp.Interpreter;
using System;
using System.Text;
using System.Threading;
using Threads;
using UnityEngine;
using MoonSharp.Interpreter.Serialization;
using System.IO;

namespace Computers
{

    public class ProcessorOperation
    {
        public string script;

        [MoonSharpHidden]
        public ProcessorOperationMode mode;
        [MoonSharpHidden]
        public Hardware.Processor processor;

        [MoonSharpHidden]
        public ProcessorOperation(string script, ProcessorOperationMode mode, Hardware.Processor processor)
        {
            this.script = script;
            this.mode = mode;
            this.processor = processor;
        }
    }

    public enum ProcessorOperationMode
    {
        Kernel,
        User
    }

    [AddComponentMenu("Computers/Processor Operator")]
    public class ProcessorOperator : ThreadedFunctionQueue
    {
        static class RegisterUserData
        {
            static bool done = false;
            public static void Register()
            {
                if (done) return;
                done = true;
                UserData.RegisterType<ProcessorOperation>();

                UserData.RegisterType<Packet>();
                UserData.RegisterType<Hardware.HardDrive>();
                
                UserData.RegisterType<Hardware.Processor.InternalFunctions>();
                UserData.RegisterType<Hardware.Motherboard.ProcessorFunctions>();
                UserData.RegisterType<Hardware.Motherboard.MemoryFunctions>();
                UserData.RegisterType<Hardware.Motherboard.HardDriveFunctions>();
                UserData.RegisterType<Hardware.Motherboard.NetworkFunctions>();
                UserData.RegisterType<Hardware.Motherboard.GraphicsFunctions>();
                UserData.RegisterType<Hardware.Motherboard.TransmissionFunctions>();
                UserData.RegisterType<UserComputer.UserComputerFunctions>();
                
                //Table dump = UserData.GetDescriptionOfRegisteredTypes(true);
                //File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "hardwire_dump.lua"), dump.Serialize());
                //ComputerHardwiring.Initialize();
            }
        }

        public static ThreadedFunctionQueue Instance { get; set; }

        //public override void Update()
        //{
            //if (!LoadingStage.ResourcesLoaded) return;
            //base.Update();
        //}

        public override void Initialize()
        {
            if (Instance != null) return;
            Instance = this;

            RegisterUserData.Register();
            //threadCount = Environment.ProcessorCount;
            threadCount = 1;

            base.Initialize();
            //LoadingStage.ProcessorOperatorLoaded = true;
        }

        public override void LoopAction(object operation)
        {
            ProcessorOperation currentOperation = operation as ProcessorOperation;

            if (!currentOperation.processor.isPowered) return;

            switch (currentOperation.mode)
            {
                case ProcessorOperationMode.Kernel:
                    try
                    {
                        currentOperation.processor.kernel.DoString(currentOperation.script);
                    }
                    catch (Exception e)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Exception Found:\nType: {0}", e.GetType().FullName);
                        sb.AppendFormat("\nMessage: {0}", e.Message);
                        //sb.AppendFormat("\nStacktrace: {0}", e.StackTrace);
                        currentOperation.processor.ScriptException("Kernel script exception!\n[Script]\n" + currentOperation.script + "\n[Exception]\n" + sb.ToString());

                        //Debug.Log("Exception while running script! Script was: " + currentOperation.script);
                        //Debug.LogError(e.Message);
                    }
                    break;

                case ProcessorOperationMode.User:
                    try
                    {
                        Script userSpace = new Script(CoreModules.Preset_SoftSandbox | CoreModules.LoadMethods);
                        foreach (TablePair tpair in currentOperation.processor.userGlobals.Pairs)
                        {
                            userSpace.Globals[tpair.Key] = tpair.Value;
                        }
                        userSpace.Globals["Kernel"] = currentOperation.processor.userKernelGlobals;
                        userSpace.Globals["RequireVars"] = "0,system";
                        userSpace.DoString(currentOperation.script);
                    }
                    catch (Exception e)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Exception Found:\nType: {0}", e.GetType().FullName);
                        sb.AppendFormat("\nMessage: {0}", e.Message);
                        //sb.AppendFormat("\nStacktrace: {0}", e.StackTrace);
                        currentOperation.processor.ScriptException("User script exception!\n[Script]\n" + currentOperation.script + "\n[Exception]\n" + sb.ToString());

                        //Debug.Log("Exception while running script! Script was: " + currentOperation.script);
                        //Debug.LogError(e.Message);
                    }
                    break;
            }
        }
    }
}
