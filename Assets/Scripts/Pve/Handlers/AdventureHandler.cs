using Pve.GameEntity.Enemy;
using Pve.Util;
using System;

namespace Pve.Handlers
{
    internal class AdventureHandler : StateHandlerBase
    {
        public AdventureHandler()
        {
            Description = "Go on Adventure.";
        }

        public override void Execute()
        {
            bool combat = Dice.Roll() > 2;
            if (combat)
            {
                World.Enemy = CreateRandomEnemy();
                Console.Clear();
                Console.WriteLine("You have encountered a hostile " + World.Enemy.Name);
                Console.WriteLine("Press any key to begin combat...");
                Console.ReadKey();
                World.CurrentState = World.CombatHandlerInstance;
            }
            else
            {
                World.CurrentState = World.WorldEventHandlerInstance;
            }
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