using Pve.GameEntity.Equipment;
using Pve.Util;
using System;

namespace Pve.Handlers
{
    internal class WorldEventHandler : StateHandlerBase
    {
        private static readonly string[] messages =
            {
                "You wandered in green meadows of foreign lands.",
                "You were on the road and the day went by without any remarkable moments.",
                "It started as a cloudy day, then the sun broke through. Feeling the tender warmth lifted your spirit.",
                "You noticed something strange near a landmark. You have found a long abandoned stash."
            };

        public override void Execute()
        {
            Console.Clear();
            if (Dice.RollCrit(10, 1) > 0)
            {
                Item item = LootGenerator.GenerateItem();
                Console.WriteLine(messages[3]);
                Console.WriteLine("You found an item:");
                Console.WriteLine("* " + item);
                World.Player.Inventory.Add(item);
            }
            else
            {
                int roll = Dice.Roll();
                int index = (roll - 1) / 2;
                Console.WriteLine(messages[index]);
            }
            Console.WriteLine("Press any key to begin continue...");
            Console.ReadKey();
            World.CurrentState = World.MainHandlerInstance;
        }
    }
}