///<summary>
///This file is for the main program loop- most likely the "main" menu of starting/stopping the game (links to the "Game.cs" file)
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//!! Remove below lines before merging!
// Need to include: explanation on encapuslation (within video; making things private to a specific class- get and set methods)
// Item ideas; torch, exit key, knife (max four items? key should always be availble in one room)

/// <summary>
/// Main program, instansiates a new game and runs the function within Game.cs
/// </summary>
namespace DungeonExplorer
{
    internal class Program // Calls all relevant functions here within other classes
    {
        static void Main(string[] args) // Belongs to the class, and is not an object (static)
        {
            Player player = Player.NewPlayer(); // Creates a new player using the method in the Player class
            Game game = new Game(player); // game is an object of the class Game + "player" object is passed as an argument
            game.Start(); // Function from game class being called w/ the object/instance

            // Exiting the game
            Console.WriteLine("Thanks for playing! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
