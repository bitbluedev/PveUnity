using System;

namespace Pve.Util
{
    static class Dice
    {
        private static readonly Random random = new Random();

        public static int Roll()
        {
            return 1 + random.Next(6);
        }

        public static int RollMultipleDice(int numberOfRolls)
        {
            int sum = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                sum += Roll();
            }
            return sum;
        }

        public static int RollCrit(int chancePercentage, int value)
        {
            if (random.Next(101) >= (100 - chancePercentage))
            {
                return value;
            }
            return 0;
        }
    }
}
