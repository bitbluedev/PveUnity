namespace Pve.GameEntity.Equipment
{
    internal class Armor : Item
    {
        public int BaseDefense { get; set; }
        public int ExtraDefense { get; set; }
        public int Health { get; set; }

        public override string ToString()
        {
            string healthOptional = Health > 0 ? ("\n * HEALTH: " + Health) : "";
            string description = Name + "\n<size=15> * TYPE: Armor\n * DEFENSE: " + (BaseDefense + ExtraDefense) + healthOptional + "\n</size>";
            return description;
        }
    }
}