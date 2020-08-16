using Pve.GameEntity.Equipment;
using Pve.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pve.Handlers
{
    internal class CombatHandler : StateHandlerBase
    {
        private bool waitingForInput;
        private List<string> turns;
        string result;
        int phase;

        public CombatHandler()
        {
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                turns = new List<string>();
                DoCombat();
                waitingForInput = true;

                PrintPart();
                phase = 0;
            }

            switch (phase)
            {
                case 0:
                    {
                        if (Input.anyKeyDown)
                        {
                            if (Input.GetKeyDown(KeyCode.Escape) && turns.Count > 0)
                            {
                                string lastTurn = "...\n" + turns.Last();
                                turns.Clear();
                                turns.Add(lastTurn);
                            }
                            if (turns.Count > 0)
                            {
                                PrintPart();
                            }
                            else
                            {
                                phase = 1;
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        waitingForInput = false;
                        phase = 0;
                        if (World.Player.Health > 0)
                        {
                            World.CurrentState = World.MainHandlerInstance;
                        }
                        else
                        {
                            World.CurrentState = World.NewGameHandlerInstance;
                        }
                    }
                    break;
            }
        }

        private void PrintPart()
        {
            string worldText;
            worldText = turns[0];
            turns.RemoveAt(0);
            if (turns.Count > 0)
            {
                worldText += "Press ESC to skip to results.\n";
            }
            else
            {
                worldText += result;
            }
            worldText += "Press any key to continue...\n";
            World.Text = worldText;
        }

        private void DoCombat()
        {
            int turn = 1;

            while (World.Player.Health >= 0 && World.Enemy.Health >= 0)
            {
                string turnText = "Turn #" + turn + "\n";
                turnText += World.Player + "\n";
                turnText += "Enemy: " + World.Enemy + "\n";
                turnText += DoCombatTurn();
                turns.Add(turnText);
                turn++;
            }

            bool victory = World.Player.Health > 0;

            result = "";
            if (victory)
            {
                result += "You have won." + "\n";
                List<Item> loot = LootGenerator.GenerateLootItems(World.Enemy.Level);
                World.Player.Inventory.AddRange(loot);
                if (loot.Count > 0)
                {
                    string s = loot.Count > 1 ? "s" : "";
                    result += "You have found " + loot.Count + " item" + s + " after the battle." + "\n";
                    for (int i = 0; i < loot.Count; i++)
                    {
                        result += "* " + loot[i] + "\n";
                    }
                }
            }
            else
            {
                result += "You have lost. Your adventure ends here." + "\n";
            }
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