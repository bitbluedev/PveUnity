using Pve.Util;
using System;
using UnityEngine;

namespace Pve.Handlers
{
    internal class ExitHandler : StateHandlerBase
    {
        private bool waitingForInput;

        public ExitHandler()
        {
            Description = "Exit Game.";
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                World.Text = "Farewell.\nPress any key to exit...\n";
                waitingForInput = true;
            }

            if (Input.anyKeyDown)
            {
                World.Exit = true;
            }
        }
    }
}