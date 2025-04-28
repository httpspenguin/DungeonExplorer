using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public string Name { get; private set; } // Property
        public int Health { get; private set; } // Property
        public Inventory PlayerInventory { get; private set; } // Added PlayerInventory property for the Inventory class (assignment requirement), so the Game class can access the functions within

        /// <summary>
        /// Allows for the an instance for a player to be initialised, alongside the class NewPlayer
        /// (which has the function of allowing for a name input)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>u
        public Player(string name, int health) //Constructor w/ fields
        {
            Name = name;
            Health = health;
            PlayerInventory = new Inventory(); //Intialisation of inventory within the constructor
        }
        /// <summary>
        /// Static method handling user input + creating a new player
        /// </summary>
        public static Player NewPlayer() // Class Player as a return type
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
            return new Player(playerName, 100); // Initialising player with a name and health when function is called
        }

        public class Inventory //Existing relevant inventory functions moved to this class
        {
            // List is private (encapsulation), can only be accessed + modified within the Player class

            /// <summary>
            /// Initialising inventory as a List for the player
            /// to be able to view with a later function; "StatusCheck()" in Game.cs
            /// </summary>

            private List<GameMap.Room.Items> items = new List<GameMap.Room.Items>(); // Changed list name to "items" to fit reasonable naming conventions

            // TO DO: Change this summary once you finish how items work
            /// <summary>
            /// "item" picked from availableItems from the "ItemSelector()"
            /// function gets added to player's inventory to check (and potentially use)
            /// </summary>
            /// <param name="item"></param>

            public void PickUpItem(GameMap.Room.Items item) // Recieves item. Function for putting it into the inventory
            {
                items.Add(item);
                Console.WriteLine($"{item.Name} was added to your inventory.");
            }
            /// <summary>
            /// Selection if as error handling in case nothing 
            /// is currently in the player's inventory
            /// </summary>
            /// <returns>list of "item"s in the inventory</returns>
            public string ViewInventory() // Allows player to see inventory
            {
                if (items.Count == 0)
                {
                    return "Your inventory is empty!";
                }
                else
                {
                    return string.Join(", ", items);
                }
            }
        }
    }  
}