using System;
using System.Diagnostics;
using System.CodeDom;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {   
        /// <summary>
        /// Attribute is set to the first room to later be changed
        /// in the code if the player moves rooms
        /// </summary>
        private Room currentRoom = Room.room1; // Class attribute (within the class Game)
        
        /// <summary>
        /// For "startMessage" to be used within Start()
        /// </summary>
        public string startMessage; // attribute for Game()

        // Commented this out as it is unused, may add back in later
        //private Room.Items items; // Adding Items class as an instance here. "items" is unused here. Remove?

        private Player player; //Added as an attribute
        public Game(Player player) // Constructor- these are the initial values, takes the argument of player obkect
        {
            // Initialize the game with one room and one player
            startMessage = "Welcome to Dungeon Explorer! You find yourself in a strange room.";
            currentRoom = Room.room1; // To update variable later in the code when the user turns. Instead of GetDescription(), I'm using the implementation in a different way, as at this time it's easier for me to understand
            this.player = player;
        }

        /// <summary>
        /// Displays player's name, health, and their inventory for when they type "status."
        /// See function within Start()
        /// </summary>
        private void StatusCheck()
        {
            Console.WriteLine($"Name: {player.Name}, Health: {player.Health}"); // Player name + health displayed
            Console.WriteLine($"Inventory: {player.InventoryContents()}");
        }
        
        /// <summary>
        /// Contains all the functions and features for running the game.
        /// </summary>
        public void Start()
        {
            bool playing = true; // Changed to true (away from example). Return to false to end program (condition; if false, end game)
            while (playing) // assumes playing is true for this to execute.
            {
                Game currentPlay = new Game(player); // currentPlay = object of Game, representative of a current playthrough during the running program
                Console.WriteLine(currentPlay.startMessage); // Welcomes user
                Console.WriteLine(currentRoom.description); // Displays room1's description
                Console.WriteLine("You can check your status (player name, health, and inventory) by typing 'status.'");
                Console.WriteLine("Feel free to try the above, or simply press enter to continue.");

                string userInput = Console.ReadLine();
                if (string.Equals(userInput, "status", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                {
                    StatusCheck();
                }
                
                currentRoom.InitializeRoomItems(); // The items get added to "availableItems" list for the user to pick up
                Debug.Assert(currentRoom.SelectItem() != null, "This should not return null!"); // Debugging to check if the item is null or not
                string item = currentRoom.SelectItem(); // Calls function via the object (instance of the class Items) items where an item randomly gets selected. Function changed to SelectItem() to be a failsafe if "items" returns null.

                /// <summary>
                /// Another while loop to gurantee only accepted inputs.
                /// </summary.
                string askPickUp;
                // OPTIONAL; replace: With a switch case
                while (true)
                {
                    Console.WriteLine($"You look further to see {item}. Do you want to pick it up? yes/no.");
                    askPickUp = Console.ReadLine();
                    
                    if (string.Equals(askPickUp, "yes", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        player.PickUpItem(item);
                        playing = false; // TO DO: Remove once you start game modifications
                        // ! Add another line here for them to add the item to their inventory (make the class first)
                        break;
                    }
                    else if (string.Equals(askPickUp, "no", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        Console.WriteLine($"You did not pick up the {item}. Your inventory remains empty.");
                        playing = false; // TO DO: Remove once you start game modifications
                        break; 
                    }
                    else
                    {
                        Console.Write("You must enter either yes or no.");
                    }
                }
                
                //Ends game after the player picks up the object or not
                // Check player; make inventory its own class.
                

                // Continuation for additional development; allowing user to move into different rooms
                // if (string.Equals(userInput, "left", StringComparison.OrdinalIgnoreCase))
                // {
                //     currentRoom = Room.room2; // Allow for an option for the user to go back to the starting room.
                // }

            }
        }
    }
}