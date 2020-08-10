using Pve.GameEntity;
using Pve.Util;
using System;

namespace Pve.Handlers
{
    internal class NewGameHandler : StateHandlerBase
    {
        public NewGameHandler()
        {
            Description = "Start New Game.";
        }

        public override void Execute()
        {
            World.Exit = false;
            World.Player = new Player();

            bool done;
            do
            {
                done = true;

                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. " + World.NewGameHandlerInstance.Description);
                Console.WriteLine("2. " + World.ExitHandlerInstance.Description);
                Console.Write(": ");
                string result = Console.ReadLine();
                if (result == "1")
                {
                    World.CurrentState = World.MainHandlerInstance;
                }
                else if (result == "2")
                {
                    World.CurrentState = World.ExitHandlerInstance;
                }
                else
                {
                    done = false;
                }
            } while (!done);
        }
    }
}