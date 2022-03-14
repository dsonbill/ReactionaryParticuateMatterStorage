using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using MoonSharp.Interpreter;
using Computers.Hardware;

namespace Computers.Devices
{
    public class ComputerButtons : MonoBehaviour
    {
        public Computers.UserComputer computer;
        public List<Button> buttons = new List<Button>();
        public Dictionary<int, string> scripts = new Dictionary<int, string>();
        bool setGlobals;
        bool updated = true;

        void Update()
        {
            if (updated)
            {
                updated = false;
                for (int i = 0; i < buttons.Count; i++)
                {
                    int x = i;
                    buttons[x].onClick.AddListener(() => { computer.mb.AddKernelOperation(scripts[x]); });
                }
            }

            if (!setGlobals && computer.mb.processor != null)
            {
                setGlobals = true;
                computer.mb.processor.SetKernelGlobal<Action<int, string>>("SetButtonScript", (int index, string script) => { scripts[index] = script; updated = true; });
                computer.mb.processor.SetKernelGlobal<Func<int>>("NumberOfButtons", () => { return buttons.Count; });
            }
        }
    }
}