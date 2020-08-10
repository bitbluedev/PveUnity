using Pve.GameEntity;
using Pve.GameEntity.Equipment;
using Pve.Util;
using System;
using System.ComponentModel;

namespace Pve.Handlers
{
    internal class InventoryHandler : StateHandlerBase
    {
        public InventoryHandler()
        {
            Description = "Manage Inventory.";
        }
        public override void Execute()
        {
            bool done;
            do
            {
                done = true;

                Console.Clear();
                
                PrintEquippedItems();

                Console.WriteLine("What would you like to do?");
                string noUnequippedNotification = (World.Player.Inventory.Count == 0 ? " (There are no items to equip) " : "");
                Console.WriteLine("1. Equip Item." + noUnequippedNotification);
                string noEquippedNotification = (World.Player.Weapon == null && World.Player.Armor == null ? " (There are no items to unequip) " : "");
                Console.WriteLine("2. Unequip Item." + noEquippedNotification);
                Console.WriteLine("3. Continue Adventure.");
                Console.WriteLine("4. " + World.ExitHandlerInstance.Description);
                Console.Write(": ");
                string result = Console.ReadLine();
                if (result == "1")
                {
                    if (World.Player.Inventory.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("There are no items to equip.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        EquipItem();
                    }
                }
                else if (result == "2")
                {
                    if (World.Player.Weapon == null && World.Player.Armor == null)
                    {
                        Console.Clear();
                        Console.WriteLine("There are no items to unequip.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        UnequipItem();
                    }
                }
                else if (result == "3")
                {
                    World.CurrentState = World.MainHandlerInstance;
                }
                else if (result == "4")
                {
                    World.CurrentState = World.ExitHandlerInstance;
                }
                else
                {
                    done = false;
                }
            } while (!done);
        }

        private void UnequipItem()
        {
            bool done;
            do
            {
                done = true;

                Console.Clear();

                PrintEquippedItems();
                Console.WriteLine("What would you like to do?");

                int index = 1;
                if (World.Player.Weapon != null)
                {
                    Console.WriteLine(index + ". Unequip Weapon: " + World.Player.Weapon);
                    index++;
                }
                if (World.Player.Armor != null)
                {
                    Console.WriteLine(index + ". Unequip Armor: " + World.Player.Armor);
                    index++;
                }
                Console.WriteLine(index + ". Back to " + Description);
                Console.Write(": ");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    if (World.Player.Weapon != null)
                    {
                        World.Player.Inventory.Add(World.Player.Weapon);
                        World.Player.Weapon = null;
                    }
                    else
                    {
                        World.Player.Inventory.Add(World.Player.Armor);
                        World.Player.Armor = null;
                    }
                }
                else if (choice == "2")
                {
                    if (World.Player.Weapon != null && World.Player.Armor != null)
                    {
                        World.Player.Inventory.Add(World.Player.Armor);
                        World.Player.Armor = null;
                    }
                }
                else
                {
                    if (World.Player.Weapon == null || World.Player.Armor == null || choice != "3")
                    {
                        done = false;
                    }
                }
            } while (!done);
        }

        private void EquipItem()
        {
            bool done;
            do
            {
                done = true;

                Console.Clear();

                PrintEquippedItems();
                Console.WriteLine("What would you like to do?");
                for (int i = 0; i < World.Player.Inventory.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". Equip " + World.Player.Inventory[i]);
                }
                Console.WriteLine((World.Player.Inventory.Count + 1) + ". Back to " + Description);
                Console.Write(": ");

                string result = Console.ReadLine();
                if (int.TryParse(result, out int choice))
                {
                    if (choice > 0 && choice <= World.Player.Inventory.Count)
                    {
                        int index = choice - 1;
                        Item item = World.Player.Inventory[index];
                        World.Player.Inventory.RemoveAt(index);
                        if (item is Weapon)
                        {
                            if (World.Player.Weapon != null)
                            {
                                World.Player.Inventory.Add(World.Player.Weapon);
                            }
                            World.Player.Weapon = item as Weapon;
                        }
                        else
                        {
                            if (World.Player.Armor != null)
                            {
                                World.Player.Inventory.Add(World.Player.Armor);
                            }
                            World.Player.Armor = item as Armor;
                        }
                    }
                    else if (choice != World.Player.Inventory.Count + 1)
                    {
                        done = false;
                    }
                }
                else
                {
                    done = false;
                }
            } while (!done);
        }

        private void PrintEquippedItems()
        {
            Console.WriteLine("Equipment: ");
            if (World.Player.Weapon == null)
            {
                Console.WriteLine("Weapon: NONE");
            }
            else
            {
                Console.WriteLine("Weapon: " + World.Player.Weapon);
            }

            if (World.Player.Armor == null)
            {
                Console.WriteLine("Armor: NONE");
            }
            else
            {
                Console.WriteLine("Armor: " + World.Player.Armor);
            }
            Console.WriteLine();

        }
    }
}