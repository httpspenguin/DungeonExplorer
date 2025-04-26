using System; 
using System.Collections.Generic; // Added for Random to be used
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Messaging;

namespace DungeonExplorer
{
    public class GameMap // Added as part of the assignment requirement as the Room class instance is already managed in the Game class
    {
        public class Room
        {   ///<remarks>
            /// Changed to public so Start() could access and print the description
            /// </remarks>
            public string description; // Test for if the descriptions would change appropiately
            private Items items; // Instance of Items class
            
            //(Remove if necessary)!!! If there is a major error with inheritance from Items, change how the items work. Rather than a string, they could all be objects
            public Room(string description) // Constructor, blueprint for creating room descriptions
            {
                items = new Items(); ///<remarks> Variable for the Item class so the method + function can be used in Game.cs </remarks>
                this.description = description; // "this." refers to current instance of a class
            }

            /// <summary>
            /// Method in the Room class directly to call a method in the Items class
            /// </summary>
            public void InitializeRoomItems()
            {
                if (items == null) // Error checking; Check to make sure "items" is initialized and doesn't return null
                {
                    items = new Items();
                }
                items.InitializeItem(); // Calls the method in this file, as it cannot be called in Games.cs
            }

            /// <summary>
            /// Error handling w/ new function- in case item initialisation doesn't work as it's supposed to (previous tests had it return Null)
            /// </summary>
            /// <returns></returns>
            public string SelectItem()
            {
                return items?.ItemSelector() ?? "No items avaliable";
            }

            /// <summary>
            /// Made all public + static, so that Game.cs can access it
            /// Rooms will all have an object the user is able to pick up (see "ItemSelector()" method)
            /// </summary>

            // TO DO: Monsters in each room?

            // Instances of rooms to be used in the Game class- only as a single description
            public static Room room1 = new Room("Currently, you find yourself in a white room. The sparkle of an item catches your eye."); // Begin in front of a locked door. Tell user they can move left, right, or south.
            public static Room room2 = new Room("Description for room 2"); // Items will be found in each new room
            public static Room room3 = new Room("Description for room 3");
            public static Room room4 = new Room("Description for room 4");

            /// <remarks>
            /// "item" is a variable within the Player class (for them to pick up and view their inventory)
            /// </remarks>

            public class Items
            {
                string oneItem; // An index of a random object the player picks up (see the function "ItemSelector" below)
                List<string> avaliableItems = new List<string>();
                List<HealingItems> healingItems = new List<HealingItems>(); // List of healing items
                List<Weapons> weapons = new List<Weapons>(); // List of weapons

                // To be used for multiple "selectors" (for the general items, weapons, healing items)
                private static Random r = new Random();
                
                /// <summary>
                /// Method to add items for the player to possibly get in Game 
                /// </summary>

                // Currently DOing: Add healing items (as a class within this Item class) + their behaviours (ONE example below in the object room initialisation)

                class HealingItems : Items // Behaves seperately to the string list of items, as it has a healing amount and description. Made to be a subclass of Items for assignment
                {
                    // Make healing item list, pick out random index (don't remove it from list of avaliable objects) and add to initialised items
                    public string name;
                    public int healingAmount; // The number itself
                    public string description; // Telling players of the heal amount
                }

                class Weapons : Items // Weapons class for assignemnt
                {
                    public string name;
                    // public int damage; TO DO: Will there be a damage amount for the weapon HERE or within the Battle behaviour (check specific object for a Random range of attack damage?)
                    public string description;

                    public Weapons WeaponSelect()
                    {
                        if (weapons.Count == 0)
                        {
                            return null; // Returns nothing is list is empty so code doesn't go into an error. 
                        }
                        int intSelectItem = r.Next(weapons.Count);
                        oneItem = weapons[intSelectItem].name; // Selects the name of the weapon

                    }   
                }
                public void InitializeItem() // Public for it to be called in Game.cs, so the items are avaliable to be picked up
                {
                    // CURRENT TO DO: Similar to healing items, add weapons with a seperate list
                    avaliableItems.Add("Box");
                    avaliableItems.Add("Lighter");
                    avaliableItems.Add("Key");
                    avaliableItems.Add("Torch");

                    // Seperate list for healing items, as they're not a single string.
                    healingItems.Add(new HealingItems { name = "Potion",  healingAmount = 20, description = "TO DO: DESCRIPTIONS"});

                    //Seperate list for weapon objects
                    weapons.Add(new Weapons { name = "Knife", description = "A basic knife, handy for attacking." });
                    weapons.Add(new Weapons { name = "Sword", description = "A sword, sharp and deadly. Best weapon to find here." });
                }

                /// <para>
                /// This function is to be called each time the player enters a different room; 
                /// they can pick up a different item, as the random function is used
                /// to choose a random item, and then it gets removed from the list
                /// so it doesn't get picked up again.
                /// </para>

                /// <remarks>
                /// If statement returns nothing is list is empty so code doesn't run into an error.
                /// </remarks>
                /// <returns>"oneItem," index of availableItems</returns>
                
                // TO DO: Modify function for healings and weapons;
                // Weapons could be added here as they can be removed from the potential list of collectibles BUT NOT healing items, there should be an abundance

                public string ItemSelector()
                {
                    if (avaliableItems.Count == 0)
                    {
                        return null; // Returns nothing is list is empty so code doesn't go into an error. 
                    }
                    int intSelectItem = r.Next(avaliableItems.Count);
                    oneItem = avaliableItems[intSelectItem];
                    avaliableItems.RemoveAt(intSelectItem); // Removes item from list once it is picked up by the player, for other items to be picked randomly in other rooms when the function is called again

                    return oneItem;
                }
            }
        }
    }
}