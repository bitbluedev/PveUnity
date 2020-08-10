using Pve.GameEntity.Equipment;
using Pve.Util;
using System;
using System.Collections.Generic;

namespace Pve.Handlers
{
    internal class CombatHandler : StateHandlerBase
    {
        public override void Execute()
        {
            Console.Clear();
            int turn = 1;
            while (World.Player.Health >= 0 && World.Enemy.Health >= 0)
            {
                Console.WriteLine("Turn #" + turn++);
                Console.WriteLine(World.Player.ToString());
                Console.WriteLine("Enemy: " + World.Enemy.ToString());
                DoCombatTurn();
            }

            bool victory = World.Player.Health > 0;

            if (victory)
            {
                Console.WriteLine("You have won.");
                List<Item> loot = LootGenerator.GenerateLootItems(World.Enemy.Level);
                World.Player.Inventory.AddRange(loot);
                if (loot.Count > 0)
                {
                    string s = loot.Count > 1 ? "s" : "";
                    Console.WriteLine("You have found " + loot.Count + " item" + s + " after the battle.");
                    for (int i = 0; i < loot.Count; i++)
                    {
                        Console.WriteLine("* " + loot[i]);
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                World.CurrentState = World.MainHandlerInstance;
            }
            else
            {
                Console.WriteLine("You have lost. Your adventure ends here.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                World.CurrentState = World.NewGameHandlerInstance;
            }
        }

        private void DoCombatTurn()
        {
            int playerAttackRoll = Dice.RollMultipleDice(2) + Dice.RollCrit(World.Player.CriticalHitChancePercent, 100);
            int enemyAttackRoll = Dice.RollMultipleDice(2) + Dice.RollCrit(World.Enemy.Level, 100);
            int playerAttack = World.Player.Attack + playerAttackRoll;
            int enemyAttack = World.Enemy.Attack + enemyAttackRoll;

            Console.WriteLine("Player Attack Power: " + World.Player.Attack + "+" + playerAttackRoll + " -> " + playerAttack);
            Console.WriteLine("Enemy Attack Power:  " + World.Enemy.Attack + "+" + enemyAttackRoll + " -> " + enemyAttack);
            if (playerAttack > enemyAttack)
            {
                int damage = Math.Max(0, World.Player.Attack - World.Enemy.Defense);
                int crit = Dice.RollCrit(20, 5);
                World.Enemy.Health -= (damage + crit);
                string critMessage = crit > 0 ? "+" + crit : "";
                Console.WriteLine("Player attacks " + World.Enemy.Name + ". " + World.Enemy.Name + " takes " + damage + critMessage + " damage.");
            }
            else
            {
                int damage = Math.Max(0, World.Enemy.Attack - World.Player.Defense);
                int crit = Dice.RollCrit(20, 5);
                World.Player.Health -= (damage + crit);
                string critMessage = crit > 0 ? "+" + crit : "";
                Console.WriteLine(World.Enemy.Name + " attacks Player. Player takes " + damage + critMessage + " damage.");
            }
        }
    }
}