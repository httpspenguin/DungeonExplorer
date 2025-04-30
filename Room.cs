using System; 
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class GameMap // Added as part of the assignment requirement as the Room class instance is already managed in the Game class
    {
        public class Room
        {   ///<remarks>
            /// Changed to public so Start() could access and print the description for the rooms
            /// </remarks>
            public string description; // Test for if the descriptions would change appropiately
            
            // Instances of Items class (and the child classes) within Room class to call functions within
            private Items itemManager = new Items(); 
            private Items.HealingItems healingItemManager = new Items.HealingItems();
            private Items.Weapons weaponManager = new Items.Weapons();
            
            //(Remove if necessary)!!! If there is a major error with inheritance from Items, change how the items work. Rather than a string, they could all be objects
            public Room(string description) // Constructor, blueprint for creating room descriptions
            {
                this.description = description; // "this." refers to current instance of a class
            }

            /// <summary>
            /// Method in the Room class directly to call a method in the Items class
            /// </summary>
            
            //CURRENT CHANGE: changing this and the below function
            public void InitializeRoomItems()
            {
                itemManager.InitializeItem(); // Calls method in Items class so the list of objects are possible to be picked up
            }

            // Functions to call within Game class
            public Items SelectItem()
            {
                return itemManager.ItemSelector();
            }

            //For Weapon selection here (weaponManager), the function

            public Items.Weapons SelectWeapon()
            {
                return weaponManager.WeaponSelect();
            }

            public Items.HealingItems SelectHeal()
            {
                return healingItemManager.HealingSelect();
            }
            
            // TO DO: Need to modify (or change access in Game.cs within the designated classes for weapons + healing items for their error checking)

            /// <summary>
            /// Made all public + static, so that Game.cs can access it
            /// Rooms will all have an object the user is able to pick up (see "ItemSelector()" method)
            /// </summary>

            // Instances of rooms to be used in the Game class- only as a single description
            public static Room room1 = new Room("Currently, you find yourself in a strange room."); // Begin in front of a locked door. Tell user they can move left, right, or south.
            public static Room room2 = new Room("You enter a bedroom. There's a lamp on the bedside table that gives you an eery feeling."); // Items will be found in each new room
            public static Room room3 = new Room("A mostly empty room with a few objects. You see a chair in the middle. It unnerves you.");

            /// <remarks>
            /// "item" is a variable within the Player class (for them to pick up and view their inventory)
            /// </remarks>

            public class Items
            {
                // Changed to public properties 
                public string Name { get; set; }
                public string Description { get; set; } //Optional: Player can inspect it a normal item and then get their description (Console.WriteLine([item].description))

                // Changed to be instance-specific lists, and not static
                private List<Items> avaliableItems = new List<Items>(); // TO CHANGE!!
                private List<HealingItems> healingItems = new List<HealingItems>(); // List of healing items
                private List<Weapons> weapons = new List<Weapons>(); // List of weapons

                // To be used for multiple "selectors" (for the general items, weapons, healing items)
                private static Random r = new Random();

                public class HealingItems : Items // Behaves seperately to the string list of items, as it has a healing amount and description. Made to be a subclass of Items for assignment
                {
                    // name and description are inherited from the Items class
                    public int HealingAmount { get; set; } // The number itself

                    //CURRENT TO DO: Adding HealingSelect() method to select a healing item (similar to WeaponSelect() in the Weapons class)
                    
                    public HealingItems HealingSelect()
                    {
                        if (healingItems.Count == 0)
                        {
                            return null;
                        }
                        int intSelectItem = r.Next(healingItems.Count);
                        // Unlike both weapons and general items, the healing items will be possible to repeatedly be found

                        return healingItems[intSelectItem];
                    }
                }

                public class Weapons : Items // Weapons class for assignemnt
                {
                    public Weapons WeaponSelect()
                    {
                        if (weapons.Count == 0)
                        {
                            return null; // Returns nothing is list is empty so code doesn't go into an error. 
                        }
                        int intSelectItem = r.Next(weapons.Count);

                        weapons.RemoveAt(intSelectItem); // Removes item from list once it is picked up by the player, for other items to be picked randomly in other rooms when the function is called again
                        
                        return weapons[intSelectItem]; // Returns the weapon object itself removed from potential list
                    }
                    public int damage;
                }                
                
                /// <summary>
                /// Methods to add items for the player to possibly get in Game 
                /// </summary>
                public void InitializeItem() // Public for it to be called in Game.cs, so the all potential items are avaliable to be picked up
                {
                    // Descriptions which be displayed if the user "uses" and item- or more accurately "inspects it")
                    avaliableItems.Add(new Items { Name = "Box", Description = "Small enough to fit in your pocket. You feel a strange compulsion to pick it up." }); 
                    avaliableItems.Add(new Items { Name = "Lighter", Description = "A small light source." });
                    avaliableItems.Add(new Items { Name = "Key", Description = "Looks like you can unlock the door from the room you started with." }); // This can unlock the door to end the game. Give the player a warning message for this.
                    avaliableItems.Add(new Items { Name = "Torch", Description = "A bigger light souce." });

                    // Seperate list for healing items, as they're not a single string.
                    healingItems.Add(new HealingItems { Name = "Potion",  HealingAmount = 20, Description = "A vial of liquid. You determine you'd be able to replenish your health from this."});
                    healingItems.Add(new HealingItems { Name = "Bread", HealingAmount = 40, Description = "Smells... fresh? Doesn't seem to have any mold." });
                    healingItems.Add(new HealingItems { Name = "Giant chip", HealingAmount = 60, Description = "Crispy and filling." });

                    //Seperate list for weapon objects
                    weapons.Add(new Weapons { Name = "Knife", Description = "A basic knife, handy for attacking.", damage = r.Next(30, 50) });
                    weapons.Add(new Weapons { Name = "Sword", Description = "A sword, sharp and deadly. Best weapon to find in this place.", damage = r.Next(50, 100) });
                }

                /// <para>
                /// This function is to be called each time the player enters a different room; 
                /// they can pick up a different item, as the random function is used
                /// to choose a random item, and then it gets removed from the list
                /// so it doesn't get picked up again.
                /// </para>

                // Weapons could be added here as they can be removed from the potential list of collectibles BUT NOT healing items, there should be an abundance

                public Items ItemSelector() // Changing how this operates once items is changed to instances
                {
                    if (avaliableItems.Count == 0)
                    {
                        return null; // Returns nothing is list is empty so code doesn't go into an error. 
                    }
                    int intSelectItem = r.Next(avaliableItems.Count);
                    avaliableItems.RemoveAt(intSelectItem); // Removes item from list once it is picked up by the player, for other items to be picked randomly in other rooms when the function is called again

                    return avaliableItems[intSelectItem];
                }
            }
        }
    }
}