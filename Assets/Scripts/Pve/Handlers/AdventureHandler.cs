using Pve.GameEntity.Enemy;
using Pve.Util;
using UnityEngine;

namespace Pve.Handlers
{
    internal class AdventureHandler : StateHandlerBase
    {
        private bool waitingForInput;

        public AdventureHandler()
        {
            Description = "Go on Adventure.";
            waitingForInput = false;
        }

        public override void Execute()
        {
            if (!waitingForInput)
            {
                bool combat = Dice.Roll() > 2;
                if (combat)
                {
                    World.Enemy = CreateRandomEnemy();
                    PrintOptions();
                    waitingForInput = true;
                }
                else
                {
                    World.CurrentState = World.WorldEventHandlerInstance;
                    return;
                }
            }

            if (Input.anyKeyDown)
            {
                World.CurrentState = World.CombatHandlerInstance;
                waitingForInput = false;
            }
        }

        private void PrintOptions()
        {
            string worldText = "";
            worldText += "You have encountered a hostile " + World.Enemy.Name + ".\n";
            worldText += "Press any key to begin combat...\n";
            World.Text = worldText;
        }

        private Enemy CreateRandomEnemy()
        {
            int roll = Dice.RollMultipleDice(6);
            if (roll >= 28)
            {
                return new EnemyGiant();
            }
            else if (roll >= 23)
            {
                return new EnemyBear();
            }
            else
            {
                return new EnemyDog();
            }
        }
    }
}