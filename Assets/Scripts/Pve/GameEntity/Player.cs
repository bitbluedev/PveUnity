using Pve.GameEntity.Equipment;
using System.Collections.Generic;

namespace Pve.GameEntity
{
    internal class Player
    {
        private Weapon weapon;
        private Armor armor;
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseCriticalHitChancePercent { get; set; }
        public int Health { get; set; }
        public int BaseMaxHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int CriticalHitChancePercent { get; set; }

        public Weapon Weapon
        {
            get
            {
                return weapon;
            }

            set
            {
                Attack = BaseAttack;
                CriticalHitChancePercent = BaseCriticalHitChancePercent;
                if (value != null)
                {
                    Attack += value.BaseDamage + value.ExtraDamage;
                    CriticalHitChancePercent += value.CriticalHitChance;
                }
                weapon = value;
            }
        }
        public Armor Armor {
            get
            {
                return armor;
            }

            set
            {
                Defense = BaseDefense;
                MaxHealth = BaseMaxHealth;
                if (value != null)
                {
                    Defense += value.BaseDefense + value.ExtraDefense;
                    MaxHealth += value.Health;
                    if (Health > MaxHealth)
                    {
                        Health = MaxHealth;
                    }
                }
                armor = value;
            }
        }
        public List<Item> Inventory { get; set; }

        public Player()
        {
            Attack = BaseAttack = 10;
            Defense = BaseDefense = 10;
            Health = MaxHealth = BaseMaxHealth = 30;
            CriticalHitChancePercent = BaseCriticalHitChancePercent = 5;
            Level = 1;
            Inventory = new List<Item>();
        }

        public override string ToString()
        {
            return "Player [" +
                "ATTACK: " + Attack + 
                " DEFENSE: " + Defense + 
                " HEALTH: " + Health + "/" + MaxHealth + 
                " CRITICAL HIT CHANCE: " + CriticalHitChancePercent + "%" + 
                "]";
        }
    }
}