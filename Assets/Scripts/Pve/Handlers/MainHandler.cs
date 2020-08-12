using Pve.Util;
using UnityEngine;

namespace Pve.Handlers
{
    internal class MainHandler : StateHandlerBase
    {
        private bool waitingForInput;

        public MainHandler()
        {
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                waitingForInput = true;
                PrintOptions();
            }

            if (Input.inputString == "\r")
            {
                bool accepted = ProcessUserInput();
                if (!accepted)
                {
                    PrintOptions();
                }
                else
                {
                    waitingForInput = false;
                }
            }
            else if (Input.inputString == "\b")
            {
                if (World.UserInput.Length > 0)
                {
                    World.UserInput = World.UserInput.Substring(0, World.UserInput.Length - 1);
                }
            }
            else
            {
                if (Input.inputString.Length > 0)
                {
                    World.UserInput += Input.inputString;
                }
            }
        }

        private bool ProcessUserInput()
        {
            bool accepted = true;
            if (World.UserInput == "1")
            {
                World.CurrentState = World.RestHandlerInstance;
            }
            else if (World.UserInput == "2")
            {
                World.CurrentState = World.AdventureHandlerInstance;
            }
            else if (World.UserInput == "3")
            {
                //World.CurrentState = World.InventoryHandlerInstance;
                World.CurrentState = World.BlankHandlerInstance;
            }
            else if (World.UserInput == "4")
            {
                World.CurrentState = World.ExitHandlerInstance;
            }
            else
            {
                accepted = false;
            }

            World.UserInput = "";
            return accepted;
        }

        private void PrintOptions()
        {
            string worldText = "";
            worldText += "What would you like to do?\n";
            worldText += World.Player + "\n\n";
            worldText += "1. " + World.RestHandlerInstance.Description + "\n";
            worldText += "2. " + World.AdventureHandlerInstance.Description + "\n";
            worldText += "3. " + World.InventoryHandlerInstance.Description + "\n";
            worldText += "4. " + World.ExitHandlerInstance.Description + "\n";
            worldText += ": ";
            World.Text = worldText;
        }
    }
}