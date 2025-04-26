using System;
using System.Diagnostics;
using System.CodeDom;
using System.Media;

namespace DungeonExplorer
{
    /// <summary>
    /// New class to allow for player to enter their own name
    /// </summary>
    public class NewPlayer
    {
        private static Player player;
        /// <summary>
        /// True while loop to ensure the code does throw an exception 
        // if a player enters a name. "player" is initialised
        /// </summary>
        public static void InputName()
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
           }
           
           player = new Player(playerName, 100); // Setting and initialising Player name and health
        }
        /// <summary>
        /// Function allows for Game class to access player name to display it,
        /// one method of circumventing a previous bug I ran into.
        /// </summary>
        /// <returns>player</returns>
        public static Player GetPlayer() 
        {
            return player;
        }
    }

    internal class Game
    {   
        /// <summary>
        /// Attribute is set to the first room to later be changed
        /// in the code if the player moves rooms
        /// </summary>
        private GameMap.Room currentRoom; // Class attribute (within the class Game)
        
        /// <summary>
        /// For "startMessage" to be used within Start()
        /// </summary>
        public string startMessage; // attribute for Game()
        private Player player; //Added as an attribute
        public Game(Player player) // Constructor- these are the initial values, takes the argument of player object
        {
            // Initialize the game with one room and one player
            startMessage = "Welcome to Dungeon Explorer! You find yourself in a strange room.";
            currentRoom = GameMap.Room.room1; // To update variable later in the code when the user turns. Instead of GetDescription(), I'm using the implementation in a different way, as at this time it's easier for me to understand
            player = NewPlayer.GetPlayer();
        }

        /// <summary>
        /// Displays player's name, health, and their inventory for when they type "status."
        /// See function within Start()
        /// </summary>
        private void StatusCheck()
        {
            Console.WriteLine($"Name: {player.Name}, Health: {player.Health}"); // Player name + health displayed
            Console.WriteLine($"Inventory: {player.PlayerInventory.ViewInventory()}");
        }
        
        /// <summary>
        /// Contains all the functions and features for running the game currently.
        /// </summary>
        public void Start()
        {
            bool playing = true; // Changed to true (away from example). Return to false to end program (condition; if false, end game)
            while (playing) // assumes playing is true for this to execute.
            {
                Game currentPlay = new Game(player); // currentPlay = object of Game, representative of a current playthrough during the running program
                Console.WriteLine(currentPlay.startMessage); // Welcomes user (Required to work here)
                Console.WriteLine(currentRoom.description); // Displays room1's description
                Console.WriteLine("You can check your status (player name, health, and inventory) by typing 'status.'");
                
                string userInput = Console.ReadLine();
                if (string.Equals(userInput, "status", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                {
                    StatusCheck();
                }
                
                currentRoom.InitializeRoomItems(); // The items get added to "availableItems" list for the user to pick up
                Debug.Assert(currentRoom.SelectItem() != null, "This should not return null!"); // Debugging to check if the item is null or not
                GameMap.Room.Items item = currentRoom.SelectItem(); // Calls function via the object (instance of the class Items) items where an item randomly gets selected. Function changed to SelectItem() to be a failsafe if "items" returns null.

                /// <summary>
                /// Another while loop to gurantee only accepted inputs.
                /// </summary.
                string askPickUp;
                while (true)
                {
                    Console.WriteLine($"You look further to see {item}. Do you want to pick it up? yes/no.");
                    askPickUp = Console.ReadLine();
                    
                    if (string.Equals(askPickUp, "yes", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        player.PlayerInventory.PickUpItem(item);
                        playing = false;
                        break;
                    }
                    else if (string.Equals(askPickUp, "no", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        Console.WriteLine($"You did not pick up {item}. Your inventory remains empty.");
                        playing = false;
                        break;
                    }
                    else
                    {
                        Console.Write("You must enter either yes or no.");
                    }
                }
                
                //Ends game after the player picks up the object or not
                

                // Continuation for additional development
                // if (string.Equals(userInput, "left", StringComparison.OrdinalIgnoreCase))
                // {
                //     currentRoom = GameMap.Room.room2; // Allow for an option for the user to go back to the starting room.
                // }

            }
        }
    }
}