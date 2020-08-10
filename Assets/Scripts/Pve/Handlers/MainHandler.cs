using Pve.Util;
using System;

namespace Pve.Handlers
{
    internal class MainHandler : StateHandlerBase
    {
        public override void Execute()
        {
            bool done;
            do
            {
                done = true;
                Console.Clear();
                Console.WriteLine(World.Player);
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. " + World.RestHandlerInstance.Description);
                Console.WriteLine("2. " + World.AdventureHandlerInstance.Description);
                Console.WriteLine("3. " + World.InventoryHandlerInstance.Description);
                Console.WriteLine("4. " + World.ExitHandlerInstance.Description);
                Console.Write(": ");
                string result = Console.ReadLine();
                if (result == "1")
                {
                    World.CurrentState = World.RestHandlerInstance;
                }
                else if (result == "2")
                {
                    World.CurrentState = World.AdventureHandlerInstance;
                }
                else if (result == "3")
                {
                    World.CurrentState = World.InventoryHandlerInstance;
                }
                else if (result == "4")
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