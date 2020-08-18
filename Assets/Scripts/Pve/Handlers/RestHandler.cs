using Pve.Util;
using System.Linq;
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
                // Not too nice hack to display health replenishment instead of writing the whole player in states before and after.
                string originalLine = (from str in playerDescription.Split('\n')
                                       where str.StartsWith(" * HEALTH:")
                                       select str).First();
                string newLine = originalLine + " ---> " + World.Player.MaxHealth + " / " + World.Player.MaxHealth;
                playerDescription = playerDescription.Replace(originalLine, newLine);

                World.Player.Health = World.Player.MaxHealth;
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