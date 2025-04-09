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
        /// Static method handeling user input + creating a new player
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
            return new Player(playerName, 100); // Initialising player with a name and health
        }

        // Create an inventory class to move here

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

        public void PickUpItem(string item) // Recieves item. Function for putting it into the inventory
        {
            inventory.Add(item);
            Console.WriteLine($"{item} was added to your inventory.");
        }
        /// <summary>
        /// Selection if as error handling in case nothing 
        /// is currently in the player's inventory
        /// </summary>
        /// <returns>list of "item"s in the inventory</returns>
        public string InventoryContents() // Allows player to see inventory
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