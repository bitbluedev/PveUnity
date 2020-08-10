using Pve.GameEntity;
using Pve.Util;
using System;
using UnityEngine;

namespace Pve.Handlers
{
    internal class NewGameHandler : StateHandlerBase
    {
        private bool waitingForInput;
        public NewGameHandler()
        {
            Description = "Start New Game.";
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (waitingForInput == false)
            {
                waitingForInput = true;
                World.Exit = false;
                World.Player = new Player();

                PrintOptions();
            }

            if (waitingForInput)
            {
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
                    return;
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
        }

        private void PrintOptions()
        {
            string worldText = "";
            worldText += "What would you like to do?\n";
            worldText += "1. " + World.NewGameHandlerInstance.Description + "\n";
            worldText += "2. " + World.ExitHandlerInstance.Description + "\n";
            worldText += ": ";
            World.Text = worldText;
        }

        private bool ProcessUserInput()
        {
            bool accepted = true;
            if (World.UserInput == "1")
            {
                //World.CurrentState = World.MainHandlerInstance;
                World.CurrentState = World.BlankHandlerInstance;
            }
            else if (World.UserInput == "2")
            {
                //World.CurrentState = World.ExitHandlerInstance;
                World.CurrentState = World.BlankHandlerInstance;
            }
            else
            {
                accepted = false;
            }

            World.UserInput = "";
            return accepted;
        }
    }
}