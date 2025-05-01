using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public string Name { get; set; } // Property
        public int Health { get; set; } // Property
        public Inventory PlayerInventory { get; private set; } // Added PlayerInventory property for the Inventory class (assignment requirement), so the Game class can access the functions within
        public GameMap.Room.Items.Weapons EquippedWeapon { get; set; } // Property for equipped weapon (for player to use in battle)

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
            //Intialisation of inventory within the constructor.
            PlayerInventory = new Inventory(this);  // "this" refers to the current instance of the Player class
            EquippedWeapon = null; // No weapon is equipped initially as the player would have to pick it up first
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
            /// <summary>
            /// Initialising inventory as a List for the player
            /// to be able to view with a later function; "StatusCheck()" in Game.cs
            /// </summary>
            /// 
            // List is private (encapsulation), can only be accessed + modified within the Player class
            private List<GameMap.Room.Items> items = new List<GameMap.Room.Items>();
            private Player owner; // Holds reference to specific Player instance

            public Inventory(Player player)
            {  owner = player; } // Receieve Player instance in constructor

            // TO DO: Change this summary once you finish how items work
            /// <summary>
            /// "item" picked from availableItems, healingItems and weapons from the "SelectItem(), SelectHeal(), and SelectWeapon() resspectively"
            /// function gets added to player's inventory to check (and potentially use)
            /// </summary>
            /// <param name="item"></param>

            public void PickUpItem(GameMap.Room.Items item) // Recieves item. Function for putting it into the inventory
            {
                // If statement to check if object is from the Weapon class to equip
                if (item is GameMap.Room.Items.Weapons weapon)
                {
                    items.Add(weapon); // Add the weapon to the inventory FIRST so the error checking in EquipWeapon() doesn't incorrectly display the message under the "else" statement                 
                    EquipWeapon(weapon);
                }
                items.Add(item);
                Console.WriteLine($"{item.Name} was added to your inventory.");
            }

            // Function to equip auto-equip weapon to the player
            public void EquipWeapon(GameMap.Room.Items.Weapons weapon) 
            {
                if (items.Contains(weapon))
                {
                    owner.EquippedWeapon = weapon; // Set the equipped weapon to the one picked up
                    Console.WriteLine($"You tightly hold the {weapon.Name}.");
                }
                // Error checking; if function is called when player doesn't have a weapon/or any other extreme case
                else
                {
                    Console.WriteLine($"For a moment, you think you're holding something to defend yourself. But you're not.");
                }
            }

            //Using LINQ to check if the player has any healing items in their inventory
            public bool HasHealingItems()
            { return items.Any(item => item is GameMap.Room.Items.HealingItems); }

            //LINQ to filter inventory objects to healing items only- useful in battle
            public void ListHealingItems()
            {
                Console.WriteLine("Healing items in your inventory:");
                int index = 1;
                foreach (var item in items.OfType<GameMap.Room.Items.HealingItems>())
                {
                    Console.WriteLine($"{item.Name} - Healing Amount: {item.HealingAmount}, Description: {item.Description}");
                    index++;
                }
                if (!HasHealingItems())
                {
                    Console.WriteLine("You have no healing items in your inventory.");
                }
            }
            public void UseHealingItem(string itemName)
            {
                var healingItem = items.OfType<GameMap.Room.Items.HealingItems>()
                    .FirstOrDefault(item => item.Name.ToLower() == item.Name.ToLower());
                
                // Healing item avaliable, player restores health
                if (healingItem != null)
                {
                    owner.Health += healingItem.HealingAmount;
                    Console.WriteLine($"You used {healingItem.Name} and restored {healingItem.HealingAmount} health.");
                    items.Remove(healingItem); // Remove the item from the inventory after use

                    if (owner.Health > 120)
                    {
                        owner.Health = 120; // Max health cap
                        Console.WriteLine("You feel revitalised! HP fully restored.");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have any items in your inventory that can heal you.");
                }
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
                    return "Seems that your pockets are empty.";
                }
                else
                {
                    // Changed to only display the names of the items in the inventory- using LINQ Select (as they are no longer strings)
                    return string.Join(", ", items.Select(i => i.Name));
                }
            }
        }
    }  
}