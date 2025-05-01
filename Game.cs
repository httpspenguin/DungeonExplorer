using System;

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
        /// if a player enters a name. "player" is initialised
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
            startMessage = "You find yourself in a strange place. There seems to be 3 different doors.";
            currentRoom = GameMap.Room.room1; // To update variable later in the code when the user turns.
            this.player = player; //Use player object passed into constructor
            
            // Moved to constructor in hopes items don't return null
            // Remove if added to Room.cs
            currentRoom.InitializeRoomItems(); // The items get added to "availableItems" list for the user to pick up (called from Room.cs)
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
        public void Start() //REMINDER; Game ends once bool variable *playing* becomes False
        {
            bool playing = true; // Changed to true (away from example). Return to false to end program (condition; if false, end game)
            while (playing) // assumes playing is true for this to execute.
            {
                Game currentPlay = new Game(player); // currentPlay = object of Game, representative of a current playthrough during the running program
                Console.WriteLine(currentPlay.startMessage); // Welcomes user (Required to work here)
                Console.WriteLine(currentRoom.description); // Displays room1's description
                Console.WriteLine("You can check your status (player name, health, and inventory) by typing 'status.'");
                Console.WriteLine("Press enter to skip to the next prompt.");
                
                string userInput = Console.ReadLine();
                if (string.Equals(userInput, "status", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                {
                    StatusCheck();
                }

                // Having all three Item types (basic items, healing and weapons being called over from Room.cs to be used Game.cs) w/ null-conditional error handelling 
                GameMap.Room.Items itemObject = currentRoom.SelectItem();
                string itemName = itemObject?.Name; //Null-conditional in case SelectItem returns null. Changed "item" to itemObject (to reflect Items no longer being a string, but an instance)
                


                string askPickUp;

                bool weaponPickUp = true;

                /// <summary>
                /// Another while loop to gurantee only accepted inputs.
                /// </summary.
                while (true)
                {
                    if (weaponPickUp == false) // Added so that if the player declines to pick up the weapon, the code below will skip over to the next while loop.
                    {
                        break;
                    }

                    Console.WriteLine($"You look further to see {itemName} sparkling in the distance. Do you want to pick it up? yes/no.");
                    askPickUp = Console.ReadLine();

                    if (string.Equals(askPickUp, "yes", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        Console.WriteLine($"You picked up {itemName}.");
                        player.PlayerInventory.PickUpItem(itemObject);
                        if (itemName == "Key")
                        {
                            Console.WriteLine("Lucky you! Seems you can leave this place already.");
                            Console.WriteLine("You unlock the locked door inf front of you and leave. To be continued...");
                            Console.WriteLine("Game Over. You win! (If you would like to see more of the game, try running the game again?)");
                            playing = false;
                            break;
                        }
                        
                        if (playing == true) // Only displays message if "playing" variable is true.
                        {
                            // Gives player chance to also pick up a weapon in the starting room.
                            Console.WriteLine("There looks to be a sharp object here too, and something else... Do you also pick these two objects up?");
                        }
                        
                        while (true && playing == true) // If key is picked up from the start, all the other while loops will be ignored.
                        {
                            askPickUp = Console.ReadLine(); // Moved to the while loop
                            if (string.Equals(askPickUp, "yes", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                            {
                                // Moved to select only when player picks it up
                                GameMap.Room.Items.Weapons weaponObject = currentRoom.SelectWeapon();
                                string weaponName = weaponObject?.Name;
                                player.PlayerInventory.PickUpItem(weaponObject);
                                
                                GameMap.Room.Items.HealingItems healingObject = currentRoom.SelectHeal();
                                string healName = healingObject?.Name;
                                Console.WriteLine($"You also picked up three {healName}s.");

                                // Gives player three healing items to start with (ensurance for surviving in battles.)
                                for (int i = 0; i < 3; i++)
                                {
                                    player.PlayerInventory.PickUpItem(healingObject);
                                }

                                Console.WriteLine("Want to check your inventory by typing 'status?' If you no, and you wish to continue, press enter.");
                                // Added here to allow player to enter "status"
                                string statusInput = Console.ReadLine();
                                if (string.Equals(statusInput, "status", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                                {
                                    StatusCheck();
                                }
                                break;
                            }
                            else if (string.Equals(askPickUp, "no", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                            {
                                // Null-conditional added to prevent error if weaponObject is null
                                Console.WriteLine("You did not pick up the sharp object. Your inventory remains empty.");
                                weaponPickUp = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You must enter either yes or no.");
                                // Inner loop continues until answer is given
                            }
                        }
                        break; //Added to break out of the while loop once the player has picked up an item or not.
                    }
                    else if (string.Equals(askPickUp, "no", StringComparison.OrdinalIgnoreCase)) // Allows for input to be case-insensitive.
                    {
                        Console.WriteLine($"You did not pick up {itemName}. Your inventory remains empty.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You must enter either yes or no.");
                        // Loop continues until user enters a valid answer. TO DO; FIX HERE PLZ
                    }
                }
                if (playing == true)
                { 
                    Console.WriteLine("You can go left or right. Which way do you want to go?"); // Prompt user for input
                    // Another while true loop to check for user input/navigation
                }
                
                while (true && playing == true)
                {
                    userInput = Console.ReadLine();
                    if (string.Equals(userInput, "left", StringComparison.OrdinalIgnoreCase))
                    {
                        // Update room player is in and display description
                        currentRoom = GameMap.Room.room2;
                        Console.WriteLine(currentRoom.description);
                        
                        // Allow for an option for the user to go back to the starting roo
                        Console.WriteLine("You can go back to the starting room by typing 'back.'");
                        Console.WriteLine("Otherwise, press enter to continue.");
                        
                        userInput = Console.ReadLine();
                        if(string.Equals(userInput, "back", StringComparison.OrdinalIgnoreCase))
                        {
                            currentRoom = GameMap.Room.room1;
                            Console.WriteLine("After visiting the room on the left, you return back to where you started. You wonder what the deal with that strange lamp was...");
                            break; // Come out of while loop and return to room1
                        }
                        Console.WriteLine("Strangely, the lamp is shaking. You feel if you go up to it, you will engage in a fight.");
                        Console.WriteLine("Enter 'yes' if you want to fight. Otherwise, enter no to return to the starting room.");
                        
                        // Inner while loop for lamp fight
                        while (true)
                        {
                            string askFight = Console.ReadLine();
                            if (string.Equals(askFight, "yes", StringComparison.OrdinalIgnoreCase))
                            {
                                Monster.Lamp lamp = new Monster.Lamp(); // Create a new instance of the Lamp class for player to fight
                                Monster.Battle(player, lamp); // Call the Battle method to start the fight
                                if (player.Health <= 0)
                                {
                                    playing = false; // End the game if player health is 0 (or less, if it goes in the negatives)
                                }
                                itemObject = currentRoom.SelectItem(); // Call the SelectItem method for the lamp dropping an item
                                itemName = itemObject?.Name; //Null-conditional in case SelectItem returns null
                                
                                GameMap.Room.Items.HealingItems healingObject = currentRoom.SelectHeal();
                                string healName = healingObject?.Name;

                                healingObject = currentRoom.SelectHeal(); // Call the SelectHeal method for the lamp dropping a healing item
                                healName = healingObject?.Name; //Null-conditional in case SelectHeal returns null
                                Console.WriteLine($"The lamp left behind something. You pick up the {itemName} and {healName}.");
                                player.PlayerInventory.PickUpItem(itemObject); // Add the item to the player's inventory
                                player.PlayerInventory.PickUpItem(healingObject); // Add the healing item to the player's inventory

                                //LINES HERE; Whether or not you fight the lamp, the code jumps to you heading to the Right room. Find a workaround. (and check if the same applies for the "right" move)

                                Console.WriteLine("You then head to the room on the right.");
                                break; // Break out of inner loop
                            }
                            else if (string.Equals(askFight, "no", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("You did not engage in a fight with the lamp. You leave the room.");
                                break; // Break out of inner loop
                            }
                            else
                            {
                                Console.WriteLine("You must enter either yes or no.");
                                askFight = Console.ReadLine(); // Ask again for the user input
                                // Inner loop continues until answer is given
                            }
                        }
                    }
                    // This happens to run regardless 
                    if (string.Equals(userInput, "right", StringComparison.OrdinalIgnoreCase)); // Another if instead of "else if", so that the player can navigate to the right room too.
                    {
                        // Update room player is in and display description
                        currentRoom = GameMap.Room.room3;
                        Console.WriteLine(currentRoom.description);

                        // Allow for an option for the user to go back to the starting room
                        Console.WriteLine("You can go back to the starting room by typing 'back.'");
                        Console.WriteLine("Enter 'yes' if you want to fight. Otherwise, enter no to return to the starting room.");

                        userInput = Console.ReadLine();
                        if (string.Equals(userInput, "back", StringComparison.OrdinalIgnoreCase))
                        {
                            currentRoom = GameMap.Room.room1;
                            Console.WriteLine("After visiting the room on the right, you return back to where you started. You wonder what the deal with that strange chair was...");
                            break; // Come out of while loop and return to room1
                        }
                        Console.WriteLine("The chair looks like it has been used recently. You hear strange creaking from it. If you go up to it, you will engage in a fight.");
                        string askFight = Console.ReadLine();

                        // Inner while loop for lamp fight
                        while (true)
                        {
                            if (string.Equals(askFight, "yes", StringComparison.OrdinalIgnoreCase))
                            {
                                Monster.Chair chair = new Monster.Chair(); // Create a new instance of the Chair class for player to fight
                                Monster.Battle(player, chair); // Call the Battle method to start the fight
                                if (player.Health <= 0)
                                {
                                    playing = false; // End the game if player health is 0 (or less, if it goes in the negatives)
                                }
                                itemObject = currentRoom.SelectItem(); // Call the SelectItem method for the lamp dropping an item
                                itemName = itemObject?.Name; //Null-conditional in case SelectItem returns null

                                GameMap.Room.Items.HealingItems healingObject = currentRoom.SelectHeal();
                                string healName = healingObject?.Name;

                                healingObject = currentRoom.SelectHeal(); // Call the SelectHeal method for the lamp dropping a healing item
                                healName = healingObject?.Name; //Null-conditional in case SelectHeal returns null
                                Console.WriteLine($"The chair left behind something. You pick up the {itemName} and {healingObject}.");
                                player.PlayerInventory.PickUpItem(itemObject); // Add the item to the player's inventory
                                player.PlayerInventory.PickUpItem(healingObject); // Add the healing item to the player's inventory
                                break; // Break out of inner loop
                            }
                            else if (string.Equals(askFight, "no", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("You did not engage in a fight with the chair. You leave the room.");
                                break; // Break out of inner loop
                            }
                            else
                            {
                                Console.WriteLine("You must enter either yes or no.");
                                askFight = Console.ReadLine(); // Ask again for the user input
                                // Inner loop continues until answer is given
                            }
                        }
                    }
                }
                
                // If the game hasn't ended beforehand, variable "playing" set to false will skip these messages. 
                if (playing == true)
                {
                    currentRoom = GameMap.Room.room1;
                    Console.WriteLine("You're back to where you started... Looks like the door is open!");
                    Console.WriteLine("Curious to progress, you walk through the door and find yourself in a new room.");
                    Console.WriteLine("To be continued!");
                    playing = false; // End game
                }
            }
        }
    }
}