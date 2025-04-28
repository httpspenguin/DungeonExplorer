///<summary>
///This file is for the main program loop- most likely the "main" menu of starting/stopping the game (links to the "Game.cs" file)
///</summary>

using System;

// TO DO: PART OF ASSIGNEMNT: Error checking/handling 

/// <summary>
/// Main program, instansiates a new game and runs the function within Game.cs
/// </summary>
namespace DungeonExplorer
{
    internal class Program // Calls all relevant 
    {
        static void Main(string[] args) // Belongs to the class, and is not an object (static)
        {
            Player player = Player.NewPlayer(); // Creates a new player using the method in the Player class
            Game game = new Game(player); // game is an object of the class Game + "player" object is passed as an argument (within this file specifically- game is also initialised in Game.cs for program to work as intended)
            game.Start(); // Function from game class being called w/ the object/instance
            
            // For end of program
            Console.WriteLine("Thanks for playing! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
