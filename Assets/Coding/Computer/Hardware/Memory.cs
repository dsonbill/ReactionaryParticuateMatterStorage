using UnityEngine;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Computers.Hardware
{
    public class Memory
    {
        private Dictionary<string, object> values;
        private object chipLock = new object();

        public Memory()
        {
            values = new Dictionary<string, object>();
        }

        public object GetValue(string key)
        {
            lock (chipLock)
            {
                if (!values.ContainsKey(key))
                {
                    //GameSystems.LogSystem.Script("Error - Tried to access memory out of range.");
                    //return "Excep";
                    return null;
                }

                return values[key];
            }
        }

        public void SetValue(string key, object value)
        {
            lock (chipLock)
            {
                values[key] = value;
            }
        }
    }
}