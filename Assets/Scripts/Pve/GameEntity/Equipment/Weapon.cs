namespace Pve.GameEntity.Equipment
{
    internal class Weapon : Item
    {
        public int BaseDamage { get; set; }
        public int ExtraDamage { get; set; }
        public int CriticalHitChance { get; set; }

        public override string ToString()
        {
            string critOptional = CriticalHitChance > 0 ? (" CRITICAL HIT CHANCE: " + CriticalHitChance) : "";
            string description = Name + " [TYPE: Weapon DAMAGE: " + (BaseDamage + ExtraDamage) + critOptional + "]";
            return description;
        }
    }
}