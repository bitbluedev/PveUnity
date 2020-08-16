using Pve.Util;
using UnityEngine;

namespace Pve.Handlers
{
    internal class RestHandler : StateHandlerBase
    {
        private bool waitingForInput;

        public RestHandler()
        {
            Description = "Rest.";
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                waitingForInput = true;

                PrintOptions();
            }
            if (Input.anyKeyDown)
            {
                World.CurrentState = World.MainHandlerInstance;
                waitingForInput = false;
            }
            
        }

        private void PrintOptions()
        {
            string playerDescription = World.Player.ToString();
            bool shouldHeal = World.Player.Health < World.Player.MaxHealth;
            if (shouldHeal)
            {
                World.Player.Health = World.Player.MaxHealth;
                playerDescription += " -> " + World.Player;
            }
            string worldText = "";
            worldText += playerDescription + "\n";
            worldText += "You have rested.\n";
            if (shouldHeal)
            {
                worldText += "You are at full strength again.\n";
            }
            worldText += "Press any key to continue...\n";
            World.Text = worldText;
        }
    }
}