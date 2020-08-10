using Pve.Util;
using System;

namespace Pve.Handlers
{
    internal class ExitHandler : StateHandlerBase
    {
        public ExitHandler()
        {
            Description = "Exit Game.";
        }

        public override void Execute()
        {
            World.Exit = true;
            Console.Clear();
            Console.WriteLine("Farewell.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}