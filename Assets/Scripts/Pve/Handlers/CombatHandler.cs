using Pve.GameEntity.Equipment;
using Pve.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pve.Handlers
{
    internal class CombatHandler : StateHandlerBase
    {
        private bool waitingForInput;
        private bool finished = false;
        string[] fullCombatTextLines;
        int index = 0;

        public CombatHandler()
        {
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                fullCombatTextLines = PrintOptions().Split('\n');
                waitingForInput = true;
                index = 0;

                PrintPart();
            }


            if (Input.anyKeyDown)
            {
                PrintPart();

                if (index == fullCombatTextLines.Length)
                {
                    if (!finished)
                    {
                        finished = true;
                        return;
                    }
                    else
                    {
                        index = 0;
                        waitingForInput = false;
                        if (World.Player.Health > 0)
                        {
                            World.CurrentState = World.MainHandlerInstance;
                        }
                        else
                        {
                            World.CurrentState = World.NewGameHandlerInstance;
                        }
                        finished = false;
                    }
                }
            }
        }

        private void PrintPart()
        {
            if (finished)
            {
                return;
            }
            string worldText = "";
            if (fullCombatTextLines[index].StartsWith("Turn #"))
            {
                worldText += fullCombatTextLines[index] + "\n";
                index++;
            }
            while (index < fullCombatTextLines.Length && !fullCombatTextLines[index].StartsWith("Turn #"))
            {
                worldText += fullCombatTextLines[index] + "\n";
                index++;
            }
            worldText += "Press any key to continue...\n";
            World.Text = worldText;
        }

        private string PrintOptions()
        {
            int turn = 1;

            string worldText = "";

            while (World.Player.Health >= 0 && World.Enemy.Health >= 0)
            {
                worldText += "Turn #" + turn + "\n";
                worldText += World.Player.ToString() + "\n";
                worldText += "Enemy: " + World.Enemy + "\n";
                worldText += DoCombatTurn();
                turn++;
            }

            bool victory = World.Player.Health > 0;

            if (victory)
            {
                worldText += "You have won." + "\n";
                List<Item> loot = LootGenerator.GenerateLootItems(World.Enemy.Level);
                World.Player.Inventory.AddRange(loot);
                if (loot.Count > 0)
                {
                    string s = loot.Count > 1 ? "s" : "";
                    worldText += "You have found " + loot.Count + " item" + s + " after the battle." + "\n";
                    for (int i = 0; i < loot.Count; i++)
                    {
                        worldText += "* " + loot[i] + "\n";
                    }
                }
            }
            else
            {
                worldText += "You have lost. Your adventure ends here." + "\n";
            }

            return worldText;
        }

        private string DoCombatTurn()
        {
            string combatText = "";

            int playerAttackRoll = Dice.RollMultipleDice(2) + Dice.RollCrit(World.Player.CriticalHitChancePercent, 100);
            int enemyAttackRoll = Dice.RollMultipleDice(2) + Dice.RollCrit(World.Enemy.Level, 100);
            int playerAttack = World.Player.Attack + playerAttackRoll;
            int enemyAttack = World.Enemy.Attack + enemyAttackRoll;

            combatText += "Player Attack Power: " + World.Player.Attack + "+" + playerAttackRoll + " -> " + playerAttack + "\n";
            combatText += "Enemy Attack Power:  " + World.Enemy.Attack + "+" + enemyAttackRoll + " -> " + enemyAttack + "\n";
            if (playerAttack > enemyAttack)
            {
                int damage = Math.Max(0, World.Player.Attack - World.Enemy.Defense);
                int crit = Dice.RollCrit(20, 5);
                World.Enemy.Health -= (damage + crit);
                string critMessage = crit > 0 ? "+" + crit : "";
                combatText += "Player attacks " + World.Enemy.Name + ". " + World.Enemy.Name + " takes " + damage + critMessage + " damage." + "\n";
            }
            else
            {
                int damage = Math.Max(0, World.Enemy.Attack - World.Player.Defense);
                int crit = Dice.RollCrit(20, 5);
                World.Player.Health -= (damage + crit);
                string critMessage = crit > 0 ? "+" + crit : "";
                combatText += World.Enemy.Name + " attacks Player. Player takes " + damage + critMessage + " damage." + "\n";
            }

            return combatText;
        }
    }
}