using UnityEngine;
using System.Text;

namespace Computers.Hardware
{
    public class GraphicsCard
    {
        //General
        private object gpuLock = new object();
        public delegate void AddOperation(ProcessorOperationMode mode, string script);
        public event AddOperation onAddOperation;
        public delegate void OnAddLine();
        public event OnAddLine onAddLine;

        //Terminal
        private StringBuilder terminal_builder = new StringBuilder();
        public string terminal { get { return terminal_builder.ToString(); } }

        public GraphicsCard()
        {
            for (int line = 0; line < 100; line++)
            {
                terminal_builder.AppendLine();
            }
        }

        public void AddLineToTerminal(string input)
        {
            lock (gpuLock)
            {
                terminal_builder.AppendLine("<color=#ffafd8>" + input + "</color>");

                while (terminal_builder.ToString().Split("\n".ToCharArray()[0]).Length > 100)
                {
                    terminal_builder.Remove(0, terminal_builder.ToString().IndexOf("\n") + 1);
                }

                if (onAddLine != null) onAddLine();
            }
        }

        public void ClearTerminal()
        {
            lock (gpuLock) terminal_builder = new StringBuilder();
        }

        public void AddScriptOperation(ProcessorOperationMode mode, string script)
        {
            lock (gpuLock) if (onAddOperation != null) onAddOperation(mode, script);
        }
    }
}