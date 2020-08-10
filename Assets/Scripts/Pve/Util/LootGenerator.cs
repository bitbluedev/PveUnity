using Pve.GameEntity;
using Pve.GameEntity.Equipment;
using System;
using System.Collections.Generic;

namespace Pve.Util
{
    class LootGenerator
    {
        public static List<Item> GenerateLootItems(int enemyLevel)
        {
            int numberOfItems = Dice.Roll() > 3 ? 1 : 0;
            for (int i = 2; i <= enemyLevel; i++)
            {
                numberOfItems += Dice.Roll() > 3 ? 1 : 0;
            }
            List<Item> loot = new List<Item>(numberOfItems);

            for (int i = 0; i < numberOfItems; i++)
            {
                Item item = GenerateItem();
                loot.Add(item);
            }
            return loot;
        }

        public static Item GenerateItem()
        {
            if (Dice.Roll() > 3)
            {
                return GenerateWeapon();
            }
            else
            {
                return GenerateArmor();
            }
        }

        private static Armor GenerateArmor()
        {
            Armor armor;
            int roll = Dice.RollMultipleDice(6);
            if (roll >= 24)
            {
                armor = new ChainMailArmor();
            }
            else
            {
                armor = new LeatherArmor();
            }

            Tuple<string, int> extraDefense = RollExtraDefense();
            armor.ExtraDefense = extraDefense.Item2;

            Tuple<string, int> health = RollHealth();
            armor.Health = health.Item2;

            string prefix = extraDefense.Item1;
            string postfix = health.Item1;
            armor.Name = prefix + armor.Name + postfix;

            return armor;
        }

        private static Tuple<string, int> RollHealth()
        {
            int roll = Dice.RollMultipleDice(2);
            Tuple<string, int> result;
            if (roll >= 11)
            {
                result = new Tuple<string, int>(" of Life", 3);
            }
            else
            {
                result = new Tuple<string, int>("", 0);
            }
            return result;
        }

        private static Tuple<string, int> RollExtraDefense()
        {
            int roll = Dice.RollMultipleDice(2);
            Tuple<string, int> result;
            if (roll == 12)
            {
                result = new Tuple<string, int>("Sturdy ", 2);
            }
            else if (roll >= 9)
            {
                result = new Tuple<string, int>("", 0);
            }
            else
            {
                result = new Tuple<string, int>("Worn-out ", -1);
            }
            return result;
        }

        private static Weapon GenerateWeapon()
        {
            Weapon weapon;
            int roll = Dice.RollMultipleDice(6);
            if (roll >= 28)
            {
                weapon = new WeaponTwoHandedSword();
            }
            else if (roll >= 24)
            {
                weapon = new WeaponSword();
            }
            else
            {
                weapon = new WeaponHandAxe();
            }

            Tuple<string, int> extraDamage = RollExtraDamage();
            weapon.ExtraDamage = extraDamage.Item2;

            Tuple<string, int> criticalHitChance = RollCriticalHitChance();
            weapon.CriticalHitChance = criticalHitChance.Item2;

            string prefix = extraDamage.Item1;
            string postfix = criticalHitChance.Item1;
            weapon.Name = prefix + weapon.Name + postfix;

            return weapon;
        }

        private static Tuple<string, int> RollCriticalHitChance()
        {
            int roll = Dice.RollMultipleDice(2);
            Tuple<string, int> result;
            if (roll == 12)
            {
                result = new Tuple<string, int>(" of Carnage", 5);
            }
            else if (roll == 11)
            {
                result = new Tuple<string, int>(" of Pain", 1);
            }
            else
            {
                result = new Tuple<string, int>("", 0);
            }
            return result;
        }

        private static Tuple<string, int> RollExtraDamage()
        {
            int roll = Dice.RollMultipleDice(2);
            Tuple<string, int> result;
            if (roll >= 11)
            {
                result = new Tuple<string, int>("Strong ", 2);
            }
            else
            {
                result = new Tuple<string, int>("", 0);
            }
            return result;
        }
    }
}
