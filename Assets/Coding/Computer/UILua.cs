using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Computers
{
    public class UILua : MonoBehaviour
    {
        public GameObject text;
        public GameObject console;
        public TerminalConnector terminalConnector;

        Hardware.GraphicsCard activeCard;
        Text consoleText;
        ScrollLock scrollLock;
        InputField ifield;
        StringBuilder sb;

        bool buildingString;
        bool wasBuildingString;

        public ProcessorOperationMode mode = ProcessorOperationMode.Kernel;

        string currentComputerGuid = "";
        public string computerGuid = "";

        public ScrollRect scrollrect;
        public ExpandToTextLines expander;

        StringBuilder blankConsole;

        void Start()
        {
            ifield = GetComponent<InputField>();
            consoleText = console.GetComponent<Text>();
            scrollLock = console.GetComponent<ScrollLock>();

            blankConsole = new StringBuilder();
            for (int line = 0; line < 100; line++)
            {
                blankConsole.AppendLine();
            }

            blankConsole.AppendLine("[Console Disconnected]");
        }

        void Update()
        {
            if (terminalConnector != null) computerGuid = terminalConnector.TerminalCable.guid.ToString();

            if (currentComputerGuid != computerGuid)
            {
                if (Guid.Parse(computerGuid) == Guid.Empty)
                {
                    if (activeCard != null)
                    {
                        activeCard.onAddLine -= OnAddLine;
                    }

                    currentComputerGuid = Guid.Empty.ToString();
                    activeCard = null;

                    return;
                }

                Guid guid = Guid.Parse(computerGuid);
                if (UserComputerRegistry.computers.ContainsKey(guid) && UserComputerRegistry.computers[guid].mb.graphicsCards.Count > 0)
                {
                    activeCard = UserComputerRegistry.computers[guid].mb.graphicsCards[UserComputerRegistry.computers[guid].mb.activeGPU];
                    activeCard.onAddLine += OnAddLine;

                    currentComputerGuid = computerGuid;
                }
            }

            if (activeCard == null)
            {
                if (consoleText.text != blankConsole.ToString())
                {
                    consoleText.text = blankConsole.ToString();

                    OnAddLine();
                }
                
                return;
            }

            if (consoleText.text != activeCard.terminal)
            {
                consoleText.text = activeCard.terminal;

                //OnAddLine();
            }
        }

        void OnAddLine()
        {
            Threads.UnityFunctionQueue.Instance.QueueOperation(new Threads.UnityOperationWrapper((object[] x) =>
            {
                expander.Expand();
                SetScrollPosition();
            }, new object[0]));
        }

        void SetScrollPosition()
        {
            Threads.UnityFunctionQueue.Instance.QueueOperation(new Threads.UnityOperationWrapper((object[] x) =>
            {
                scrollrect.verticalNormalizedPosition = 0;
                scrollLock.SetPosition();
            }, new object[0]));
        }

        void ConsoleLog(string text)
        {
            if (activeCard != null) activeCard.AddLineToTerminal("<color=#ffffffff>" + text + "</color>");
        }

        public void OnInputChange()
        {
            if (ifield.text == "\n")
            {
                if (buildingString)
                {
                    ifield.text = " \n";
                }
                else
                {
                    if (activeCard != null) activeCard.AddLineToTerminal("");
                }
                ifield.text = "";
                return;
            }
            //Check for enter button
            if (ifield.text.Length > 0 && ifield.text[ifield.text.Length - 1].ToString() == "\n")
            {
                if (ifield.text[0].ToString() == "#")
                {
                    ProcessConsoleCommand();
                    ifield.text = "";
                    return;
                }

                ProcessLua();
                ifield.text = "";
            }
        }

        void ProcessLua()
        {
            //Don't process if GPU null
            if (activeCard == null) return;

            //Do multi-line building
            if (ifield.text[ifield.text.Length - 2].ToString() == "\\" && !buildingString)
            {
                sb = new StringBuilder();

                buildingString = true;

                sb.AppendLine(ifield.text.Substring(0, ifield.text.Length - 2));
            }
            else if (ifield.text[ifield.text.Length - 2].ToString() == "\\" && buildingString)
            {
                sb.AppendLine(ifield.text.Substring(0, ifield.text.Length - 2));
            }
            else if (buildingString)
            {
                sb.AppendLine(ifield.text.Substring(0, ifield.text.Length - 1));

                activeCard.AddLineToTerminal("<color=#00ffffff>" + ifield.text.Substring(0, ifield.text.Length - 1) + "</color>");
                activeCard.AddLineToTerminal(sb.ToString());

                buildingString = false;
                wasBuildingString = true;
            }

            //Print input and add operations
            if (buildingString)
            {
                activeCard.AddLineToTerminal("<color=#00ffffff>" + ifield.text.Substring(0, ifield.text.Length - 2) + "</color>");
            }
            else if (wasBuildingString)
            {
                //computer.mb.AddProcessorOperation(mode, new Hardware.ProcessorOperation(sb.ToString(), 0));
                activeCard.AddScriptOperation(mode, sb.ToString());

                wasBuildingString = false;
            }
            else
            {
                ConsoleLog(ifield.text.Substring(0, ifield.text.Length - 1));
                activeCard.AddScriptOperation(mode, ifield.text.Substring(0, ifield.text.Length - 1));
            }
        }

        void ProcessConsoleCommand()
        {
            string input = ifield.text.Substring(1, ifield.text.Length - 2);
            string command = input;
            string args = "";
            if (input.Contains(" "))
            {
                command = input.Substring(0, input.IndexOf(" "));
                args = input.Substring(input.IndexOf(" ") + 1, input.Length - input.IndexOf(" ") - 1);
            }
            switch (command.ToLower())
            {
                case "help":
                    PrintConsoleHelp();
                    break;
                case "mode":
                    ChangeMode();
                    ConsoleLog("Mode changed to: " + mode.ToString());
                    break;
                case "run":
                    if (args == "") break;
                    ConsoleLog("Running script: " + args);
                    RunScript(args);
                    break;
            }
        }

        void PrintConsoleHelp()
        {
            activeCard.AddLineToTerminal(FormatHelp("#mode", "Change between Kernel- and User- mode"));
            activeCard.AddLineToTerminal(FormatHelp("#run [script path]", "Run a script at specified path in the current mode"));
            activeCard.AddLineToTerminal(FormatHelp("\\ at end of line", "Write multi-line input"));
            activeCard.AddLineToTerminal(FormatHelp("ClearScreen()", "Clears the terminal"));
        }

        string FormatHelp(string subject, string helpText)
        {
            return string.Format("<color=#008000ff>{0}</color>\n<color=#ffa500ff>{1}</color>", subject, helpText);
        }

        public void ChangeControllerLockState(bool state)
        {
            CharacterController.Instance.ChangeLockState(state);
            CarryMode.locked = state;
        }

        void ChangeMode()
        {
            if (mode == ProcessorOperationMode.Kernel) mode = ProcessorOperationMode.User;
            else mode = ProcessorOperationMode.Kernel;
        }

        void RunScript(string name)
        {
            activeCard.AddScriptOperation(mode, Hardware.ProcessorFileLoader.GetFile(name));
        }
    }
}