namespace Pve.GameEntity.Equipment
{
    internal class Weapon : Item
    {
        public int BaseDamage { get; set; }
        public int ExtraDamage { get; set; }
        public int CriticalHitChance { get; set; }

        public override string ToString()
        {
            string critOptional = CriticalHitChance > 0 ? ("\n * CRITICAL HIT CHANCE: " + CriticalHitChance) : "";
            string description = Name + "\n<size=15> * TYPE: Weapon\n * DAMAGE: " + (BaseDamage + ExtraDamage) + critOptional + "\n</size>";
            return description;
        }
    }
}