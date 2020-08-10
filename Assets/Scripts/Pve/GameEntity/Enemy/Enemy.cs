namespace Pve.GameEntity.Enemy
{
    internal abstract class Enemy
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public Enemy(string name, int level, int attack, int defense, int health)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            Health = health;
            Level = level;
        }

        public override string ToString()
        {
            return Name + " [ATTACK: " + Attack + " DEFENSE: " + Defense + " HEALTH: " + Health + "]";
        }
    }
}