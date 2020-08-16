using Pve.GameEntity.Equipment;
using Pve.Util;
using UnityEngine;

namespace Pve.Handlers
{
    internal class InventoryHandler : StateHandlerBase
    {
        private enum InventoryHandlerPhase
        {
            ENTRY_POINT_INVENTORY_MAIN_MENU, 
            READ_INPUT_IN_INVENTORY_MAIN_MENU, 
            EQUIP_ITEM_ENTRY_POINT, 
            UNEQUIP_ITEM_ENTRY_POINT, 
            READ_ANY_KEY, 
            EQUIP_ITEM_SUB_MENU, 
            UNEQUIP_ITEM_SUB_MENU, 
            EQUIP_ITEM_SUB_MENU_INPUT, 
            UNEQUIP_ITEM_SUB_MENU_INPUT
        }

        private InventoryHandlerPhase phase;

        public InventoryHandler()
        {
            Description = "Manage Inventory.";
            phase = InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU;
        }

        private bool debug = false;

        public override void Execute()
        {
            if (debug)
            {
                debug = false;
                World.Player.Inventory.Add(LootGenerator.GenerateItem());
                World.Player.Inventory.Add(LootGenerator.GenerateItem());
                World.Player.Inventory.Add(LootGenerator.GenerateItem());
            }
            string worldText = "";
            switch (phase)
            {
                case InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU:
                    {
                        worldText += PrintEquippedItems();

                        worldText += "What would you like to do?\n";
                        string noUnequippedNotification = (World.Player.Inventory.Count == 0 ? " (There are no items to equip) " : "");
                        worldText += "1. Equip Item." + noUnequippedNotification + "\n";
                        string noEquippedNotification = (World.Player.Weapon == null && World.Player.Armor == null ? " (There are no items to unequip) " : "");
                        worldText += "2. Unequip Item." + noEquippedNotification + "\n";
                        worldText += "3. Continue Adventure." + "\n";
                        worldText += "4. " + World.ExitHandlerInstance.Description + "\n";
                        worldText += ": ";
                        World.Text = worldText;

                        phase = InventoryHandlerPhase.READ_INPUT_IN_INVENTORY_MAIN_MENU;
                    }
                    break;
                case InventoryHandlerPhase.READ_INPUT_IN_INVENTORY_MAIN_MENU:
                    {
                        if (Input.inputString == "\r")
                        {
                            switch (World.UserInput)
                            {
                                case "1":
                                    {
                                        phase = InventoryHandlerPhase.EQUIP_ITEM_ENTRY_POINT;
                                    }
                                    break;
                                case "2":
                                    {
                                        phase = InventoryHandlerPhase.UNEQUIP_ITEM_ENTRY_POINT;
                                    }
                                    break;
                                case "3":
                                    {
                                        World.CurrentState = World.MainHandlerInstance;
                                        phase = InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU;
                                    }
                                    break;
                                case "4":
                                    {
                                        World.CurrentState = World.ExitHandlerInstance;
                                    }
                                    break;
                                default:
                                    {
                                        phase = InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU;
                                    }
                                    break;
                            }
                            World.UserInput = "";
                        }
                        else if (Input.inputString == "\b")
                        {
                            if (World.UserInput.Length > 0)
                            {
                                World.UserInput = World.UserInput.Substring(0, World.UserInput.Length - 1);
                            }
                        }
                        else
                        {
                            if (Input.inputString.Length > 0)
                            {
                                World.UserInput += Input.inputString;
                            }
                        }
                    }
                    break;
                case InventoryHandlerPhase.EQUIP_ITEM_ENTRY_POINT:
                    {
                        if (World.Player.Inventory.Count == 0)
                        {
                            worldText += "There are no items to equip.\n";
                            worldText += "Press any key to continue...\n";

                            World.Text = worldText;

                            phase = InventoryHandlerPhase.READ_ANY_KEY;
                        }
                        else
                        {
                            phase = InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU;
                        }
                    }
                    break;
                case InventoryHandlerPhase.UNEQUIP_ITEM_ENTRY_POINT:
                    {
                        if (World.Player.Weapon == null && World.Player.Armor == null)
                        {
                            worldText += "There are no items to unequip.\n";
                            worldText += "Press any key to continue...\n";
                            World.Text = worldText;

                            phase = InventoryHandlerPhase.READ_ANY_KEY;
                        }
                        else
                        {
                            phase = InventoryHandlerPhase.UNEQUIP_ITEM_SUB_MENU;
                        }
                    }
                    break;
                case InventoryHandlerPhase.READ_ANY_KEY:
                    {
                        if (Input.anyKeyDown)
                        {
                            phase = 0;
                        }
                    }
                    break;
                case InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU: // EquipItem
                    {
                        worldText = "";
                        worldText += PrintEquippedItems();
                        worldText += "What would you like to do?\n";
                        for (int i = 0; i < World.Player.Inventory.Count; i++)
                        {
                            worldText += (i + 1) + ". Equip " + World.Player.Inventory[i] + "\n";
                        }
                        worldText += (World.Player.Inventory.Count + 1) + ". Back to " + Description + "\n";
                        worldText += ": ";

                        World.Text = worldText;

                        phase = InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU_INPUT;
                    }
                    break;
                case InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU_INPUT:
                    {
                        if (Input.inputString == "\r")
                        {
                            if (int.TryParse(World.UserInput, out int choice))
                            {
                                phase = InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU;
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
                                    phase = InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU_INPUT;
                                }
                            }
                            else
                            {
                                phase = InventoryHandlerPhase.EQUIP_ITEM_SUB_MENU_INPUT;
                            }
                            World.UserInput = "";
                        }
                        else if (Input.inputString == "\b")
                        {
                            if (World.UserInput.Length > 0)
                            {
                                World.UserInput = World.UserInput.Substring(0, World.UserInput.Length - 1);
                            }
                        }
                        else
                        {
                            if (Input.inputString.Length > 0)
                            {
                                World.UserInput += Input.inputString;
                            }
                        }
                    }
                    break;
                case InventoryHandlerPhase.UNEQUIP_ITEM_SUB_MENU:
                    {
                        worldText = "";
                        worldText += PrintEquippedItems();
                        worldText += "What would you like to do?\n";
                        int index = 1;
                        if (World.Player.Weapon != null)
                        {
                            worldText += index + ". Unequip Weapon: " + World.Player.Weapon + "\n";
                            index++;
                        }
                        if (World.Player.Armor != null)
                        {
                            worldText += index + ". Unequip Armor: " + World.Player.Armor + "\n";
                            index++;
                        }
                        worldText += index + ". Back to " + Description + "\n";
                        worldText += ": ";

                        World.Text = worldText;

                        phase = InventoryHandlerPhase.UNEQUIP_ITEM_SUB_MENU_INPUT;
                    }
                    break;
                case InventoryHandlerPhase.UNEQUIP_ITEM_SUB_MENU_INPUT:
                    {
                        if (Input.inputString == "\r")
                        {
                            phase = InventoryHandlerPhase.ENTRY_POINT_INVENTORY_MAIN_MENU;
                            string choice = World.UserInput;
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
                                    phase = InventoryHandlerPhase.UNEQUIP_ITEM_SUB_MENU_INPUT;
                                }
                            }
                            World.UserInput = "";
                        }
                        else if (Input.inputString == "\b")
                        {
                            if (World.UserInput.Length > 0)
                            {
                                World.UserInput = World.UserInput.Substring(0, World.UserInput.Length - 1);
                            }
                        }
                        else
                        {
                            if (Input.inputString.Length > 0)
                            {
                                World.UserInput += Input.inputString;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private string PrintEquippedItems()
        {
            string result = "";
            result += "Equipment: \n";
            if (World.Player.Weapon == null)
            {
                result += "Weapon: NONE\n";
            }
            else
            {
                result += "Weapon: " + World.Player.Weapon + "\n";
            }

            if (World.Player.Armor == null)
            {
                result += "Armor: NONE\n";
            }
            else
            {
                result += "Armor: " + World.Player.Armor + "\n";
            }

            return result;
        }
    }
}