using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace Computers.Hardware
{
    public static class SetScriptOptions
    {
        private static bool set = false;
        public static void SetOptions()
        {
            if (set) return;
            set = true;

            Script.DefaultOptions.UseLuaErrorLocations = true;
            Script.DefaultOptions.ScriptLoader = new RequireSystem()
            {
                ModulePaths = new string[] { "?.lua", "?/?.lua", "?/init.lua", "?" }
            };
        }
    }

    public class Processor
    {
        public class InternalFunctions
        {
            [MoonSharpHidden]
            private Misc.VarRef<Processor> processor;

            [MoonSharpHidden]
            public InternalFunctions(Misc.VarRef<Processor> processor)
            {
                this.processor = processor;
            }

            public void SysLog(string message)
            {
                Threads.UnityFunctionQueue.Instance.QueueOperation(new Threads.UnityOperationWrapper((operation) => { Debug.Log((string)operation[0]); }, message));
            }

            public void ServiceStart(ScriptFunctionDelegate func)
            {
                processor.Value.ServiceStart(func);
            }

            public void ServiceStop(ScriptFunctionDelegate func)
            {
                processor.Value.ServiceStop(func);
            }

            public void SetUserGlobalFunction(string name, ScriptFunctionDelegate func)
            {
                processor.Value.SetUserGlobalFunction(name, func);
            }

            public string GetFile(string path)
            {
                return ProcessorFileLoader.GetFile(path);
            }
        }

        public List<ScriptFunctionDelegate> runningServices;
        public Table userGlobals;
        public Table userKernelGlobals;
        public object kernelLock = new object();
        public object userLock = new object();
        public delegate void OnScriptException(string excep);
        public event OnScriptException onScriptException;

        public Script kernel;

        public List<Action> globalSets = new List<Action>();

        public bool isPowered { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opsPerOp"></param>
        /// <param name="frequency"></param>
        public Processor()
        {
            SetScriptOptions.SetOptions();
            runningServices = new List<ScriptFunctionDelegate>();
            userGlobals = new Table(kernel);
            userKernelGlobals = new Table(kernel);
            kernel = new Script(CoreModules.Preset_SoftSandbox | CoreModules.LoadMethods);

            SetKernelGlobal("Internal", new InternalFunctions(new Misc.VarRef<Processor>(() => this, (Processor value) => { })));
            SetKernelGlobal("RequireVars", "0,system");
        }

        public bool TogglePower()
        {
            if (isPowered) PowerOff();
            else PowerOn();
            return isPowered;
        }

        void PowerOn()
        {
            runningServices = new List<ScriptFunctionDelegate>();
            userGlobals = new Table(kernel);
            kernel = new Script(CoreModules.Preset_SoftSandbox | CoreModules.LoadMethods);

            foreach (Action entry in globalSets)
            {
                entry();
            }

            isPowered = true;
        }

        void PowerOff()
        {
            isPowered = false;
        }

        void ServiceStart(ScriptFunctionDelegate action)
        {
            lock (kernelLock)
            {
                runningServices.Add(action);
            }
        }

        void ServiceStop(ScriptFunctionDelegate action)
        {
            lock (kernelLock)
            {
                runningServices.Remove(action);
            }
        }

        public void SetKernelGlobal<T>(string name, T value)
        {
            globalSets.Add(() =>
            {
                try
                {
                    lock (kernelLock)
                    {
                        kernel.Globals[name] = value;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Exception while setting global on processor!");
                }
            });
        }

        public void SetUserGlobal<T>(string name, T value)
        {
            globalSets.Add(() =>
            {
                try
                {
                    lock (userLock)
                    {
                        userGlobals[name] = value;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Exception while setting global on processor!");
                }
            });
        }

        public void SetUserGlobalFunction(string name, ScriptFunctionDelegate function)
        {
            userKernelGlobals[name] = function;
        }

        public void SetUserGlobalTable(string name, Table table)
        {
            userKernelGlobals[name] = table;
        }

        public void ScriptException(string excep)
        {
            if (onScriptException != null) onScriptException(excep);
        }
    }

    public class RequireSystem : ScriptLoaderBase
    {
        public string drivePath = Path.Combine(Environment.CurrentDirectory, "Drives");
        public override object LoadFile(string file, Table globalContext)
        {
            //return string.Format("SysLog ([[A request to load '{0}' has been made]])", file);
            //UnityEngine.Debug.Log("File Path is: " + Path.Combine(Path.Combine(Environment.CurrentDirectory, "Lua"), file));
            try
            {
                string[] requireVars = (globalContext["RequireVars"] as string).Split(",".ToCharArray()[0]);
                Func<int, HardDrive> GetHarddrive = globalContext["GetHarddrive"] as Func<int, HardDrive>;
                return GetHarddrive(0).ReadFile(String.Format("{0}:/{1}/{2}", requireVars[0], requireVars[1], file));
            }
            catch
            {
                throw new Exception("Really bad stuff or something");
            }
            //return File.ReadAllText(Path.Combine(Path.Combine(Environment.CurrentDirectory, "Lua"), file));
        }

        public override bool ScriptFileExists(string name)
        {
            //UnityEngine.Debug.Log("(Exist)File Path is: " + Path.Combine(Path.Combine(Environment.CurrentDirectory, "Lua"), name));
            return File.Exists(Path.Combine(Path.Combine(Environment.CurrentDirectory, "Lua"), name));
        }
    }

    public static class ProcessorFileLoader
    {
        private static string directory = Path.Combine(Environment.CurrentDirectory, "Lua");
        private static FileSystemWatcher watcher = new FileSystemWatcher();
        private static Dictionary<string, string> filesLoaded = new Dictionary<string, string>();

        static ProcessorFileLoader()
        {
            watcher.Path = directory;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (filesLoaded.ContainsKey(e.FullPath)) filesLoaded.Remove(e.FullPath);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            if (filesLoaded.ContainsKey(e.OldFullPath)) filesLoaded.Remove(e.OldFullPath);
        }

        public static string GetFile(string path)
        {
            if (path.Contains(":") || path.StartsWith("/")) return null;
            string safeishPath = path.Replace("..", ".");
            string fullPath = Path.Combine(directory, safeishPath);
            //UnityEngine.Debug.Log("Request to load file: " + fullPath);
            if (!filesLoaded.ContainsKey(fullPath))
            {
                if (File.Exists(fullPath))
                {
                    //UnityEngine.Debug.Log("Loading file from disk");
                    filesLoaded[fullPath] = File.ReadAllText(fullPath);
                    return filesLoaded[fullPath];
                }
                return null;
            }
            else
            {
                //UnityEngine.Debug.Log("Loading file from cache");
                return filesLoaded[fullPath];
            }
        }
    }
}