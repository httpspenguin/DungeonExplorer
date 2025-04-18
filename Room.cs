using System; 
using System.Collections.Generic; // Added for Random to be used
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Messaging;

namespace DungeonExplorer
{
    public class Room
    {   ///<remarks>
        /// Changed to public so Start() could access and print the description
        /// </remarks>
        public string description; // Test for if the descriptions would change appropiately
        private Items items; // Instance of Items class
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
            if (items == null) // Error checking; Check to make sure items is initialized and doesn't return null
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
        // TO DO: Put this in a class called GameMap (requirement for brief)

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
            
            /// <summary>
            /// Method to add items for the player to possibly get in Game 
            /// </summary>
            
            // TO DO: To add healing items (as a class within this Item class) + their behaviours

            class HealingItems
            {
                public string healingItem;
                public int healingAmount; // The number itself
                public string description; // Telling players of the heal amount
            }
            public void InitializeItem() // Public for it to be called in Game.cs, so the items are avaliable to be picked up

                // Example below of additional items to potentially add
            //{
            //    avaliableItems.Add("Torch");
            //    avaliableItems.Add("Key");
            //    avaliableItems.Add("Map");
            //    avaliableItems.Add("Potion");
            //}
            {
                avaliableItems.Add("Box");
                avaliableItems.Add("Knife");
                avaliableItems.Add("Lighter");
                avaliableItems.Add("Key");
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
            public string ItemSelector()
            {
                if(avaliableItems.Count == 0)
                {
                    return null; // Returns nothing is list is empty so code doesn't go into an error. Potential debug here.
                }
                
                Random r = new Random();
                int randomInt = r.Next(avaliableItems.Count);
                oneItem = avaliableItems[randomInt];
                avaliableItems.RemoveAt(randomInt); // Removes item from list once it is picked up by the player, for other items to be picked randomly in other rooms when the function is called again
                
                return oneItem;
            }
            
        }
    }
}