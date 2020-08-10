namespace Pve.GameEntity.Equipment
{
    internal class Armor : Item
    {
        public int BaseDefense { get; set; }
        public int ExtraDefense { get; set; }
        public int Health { get; set; }

        public override string ToString()
        {
            string healthOptional = Health > 0 ? (" HEALTH: " + Health) : "";
            string description = Name + " [TYPE: Armor DEFENSE: " + (BaseDefense + ExtraDefense) + healthOptional + "]";
            return description;
        }
    }
}