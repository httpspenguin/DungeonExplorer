using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; } // Property
        public int Health { get; private set; } // Property
        
        /// <summary>
        /// Allows for the an instance for a player to be initialised
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        public Player(string name, int health) //Constructor w/ fields
        {
            Name = name;
            Health = health;
        }
        /// <summary>
        /// Static method handling user input + creating a new player
        /// </summary>
        public static Player NewPlayer()
        {
            string playerName;
            while (true)
            {
                Console.WriteLine("Please enter the player's name: "); // Repeats line if given and empty input until a player enters a name
                playerName = Console.ReadLine();
                if (!string.IsNullOrEmpty(playerName))
                {
                    break;
                }
                Console.WriteLine("Name cannot be empty. Please try again: ");
            }
            return new Player(playerName, 100); // Initialising player with a name and health once called in main program
        }

        // TO DO (General): Behaviours specifically used for combat + add behaviour for the items used/selected.
        // Could use a selector (switch case) for the items to potentially be used in combat (or healing)
        public class Inventory
        {
            // REMOVE IF UNUSED: Fields up here if needed for constructor
            public Inventory()
            {
                // Constructor logic if needed
            }
            /// <summary>
            /// Initialising iventory as a List for the player
            /// to be able to view with a later function; "StatusCheck()" in Game.cs
            /// </summary>
            private List<string> inventory = new List<string>(); // Encapsulation; preventing accidental modifications. Only avaliable to the Player class. intialising list of items the player picks up 

            /// <summary>
            /// "item" picked from availableItems from the "ItemSelector()"
            /// function gets added to player's inventory to check (and potentially use)
            /// </summary>
            /// <param name="item"></param>

            // TO DO: PART OF ASSIGNEMNT: COLLECTIBLE needs to be an INTERFACE. How would I apply it as an interface? (Do it for healing items?)
            public void PickUpItem(string item) // Recieves item. Function for putting it into the inventory. Could add weapon hiearchy here if another weapon is added (aka if a knife is picked up, but player has a better weapon, they are given the choice to ignore it. Interger based to determine strength? knife = 1, sword = 2 and if current weapon held == 2, knife will be ignored!)
                                                // TO DO: PART OF ASSIGNEMNT: USING LINQ TO FILTER STRONGEST WEAPON AND IMPLEMENTING IT TO HOW WEAPON HIEARCHY'S WOULD WORK (similar to output of above plan, but different method?)
            {
                inventory.Add(item);
                Console.WriteLine($"{item} was added to your inventory.");

                // TO DO: Could add more logic here for if the player has a weapon already, and if they want to equip it or not
                //TO DO: Add a unique message for the knife/any other weapon being picked up (could do a string match switch statement? aka if item == knife OR sword OR etc, give an equip message)
                // Below: sample example of the above two
                if (item == "Knife")
                {
                    Console.WriteLine($"The {item} is now equipped!");
                }
            }
            /// <summary>
            /// Selection if as error handling in case nothing 
            /// is currently in the player's inventory
            /// </summary>
            /// <returns>list of "item"s in the inventory</returns>

            // TO DO/Suggest = Could call this behaviour within Monster.cs Battle behaviour so the player can select what item they want to use (via typing it in)
            // TO DO: PART OF ASSIGNEMNT: Using LINQ to filter inventory items here
            public string ViewInventory() // Function/Behaviour; Allows player to see inventory
            {
                if (inventory.Count == 0) 
                {
                    return "Your inventory is empty!";
                }
                else
                {
                    return string.Join(", ", inventory);
                }
            }
        }

    }
        
}