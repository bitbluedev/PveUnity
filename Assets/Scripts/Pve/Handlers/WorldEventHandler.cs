using Pve.GameEntity.Equipment;
using Pve.Util;
using System;
using UnityEngine;

namespace Pve.Handlers
{
    internal class WorldEventHandler : StateHandlerBase
    {
        private bool waitingForInput;

        private static readonly string[] messages =
            {
                "You wandered in green meadows of foreign lands.",
                "You were on the road and the day went by without any remarkable moments.",
                "It started as a cloudy day, then the sun broke through. Feeling the tender warmth lifted your spirit.",
                "You noticed something strange near a landmark. You have found a long abandoned stash."
            };

        public WorldEventHandler()
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

            if (Input.anyKeyDown)
            {
                waitingForInput = false;
                World.CurrentState = World.MainHandlerInstance;
            }
        }

        private void PrintOptions()
        {
            string worldText = "";
            if (Dice.RollCrit(10, 1) > 0)
            {
                Item item = LootGenerator.GenerateItem();
                worldText += messages[3] + "\n";
                worldText += "You found an item:\n";
                worldText += "* " + item + "\n";
                World.Player.Inventory.Add(item);
            }
            else
            {
                int roll = Dice.Roll();
                int index = (roll - 1) / 2;
                worldText = messages[index] + "\n";
            }
            worldText += "Press any key to begin continue...\n";
            World.Text = worldText;
        }
    }
}