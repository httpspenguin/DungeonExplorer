using System;

namespace DungeonExplorer
{
    //TO DO: PART OF ASSIGNMENT; NEED TO MAKE CREATURE AN ABSTRACT CLASS THAT BOTH MONSTER AND PLAYER INHERIT FROM (Polymorphism)!!

    public class Creature
    {
        public int health;
        public string description; // Will this be used in the player class too?
    }


    // TO DO: PART OF ASSIGNEMNT: Will include a (BLUEPRINT) turn based battle (a behaviour/method) within this that can be modified in the harder/different enemy classes
    public class Monster : Creature // Will be a parent class, the monsters for varying difficulties will be inherited from. If this class can't be inherited from multiple times, I'll change it into an interface
	{
        // TO DO: Add a conditional for this; if health hits zero, the monster is defeated and the interaction ends (needs to MAKE SURE the player can't run around the monster)
		public Monster()
		{
			// Constructor -- setting up initial variables
            // TO DO: Change health to get/set for interface
			health = 100;
			description = "Sample description";
		}

        // FOR DEBUGGING (maybe as a test class?): Add a "skip battle" option. Could enter a key/secret word to allow for this to happen (but make sure to remove it afterwords)

		public static void Battle(int health, string description) // Will be a method for the player to engage within
        {

            // PLANS; buncha TO DOs
            // >Turn based
            // >Add a turn counter (additional to running away)?
            // >Would need to add a check for if the player is dead or not (how to handle a game over screen- game should end?)
            // >Options for the player to take in battle: attack (plan for auto-equip message with knife/a weapon with better stats; need to add a check for that)
            // To add healing items- how would they be implemented? Use inventory class, have it as an question 
            // If the player picked up a knife, it is automatically equipped and they can type in "attack" to use it
            // Need to add stats to attacking w/ a knife (and an alternative method if the player hasn't yet found a weapon- their hands, but it would cause less damage + dependant on the monster; inheritance values)
            // >Run should have a 50% chance of success. If fail, the player takes a hit for the next turn
            // >To prevent turns from being eternal- after a certain amount of turns, the monster will get tired and flee. Make this random, but it can't be no less than 8 turns

            // Sample battle logic added BELOW to modify. (TO DO: Modify type of monster- current idea; objects)
            Console.WriteLine($"You encounter a monster! {description}");
            Console.WriteLine($"You attack the monster! It has {health} health left.");

            //TO DO: Implement while loop to keep checking if either player's/monster's HP is zero and how to end it.
           
            Random random = new Random();
            // Need to make this into a loop for the random chances to work as intended. Does random.Next work outside a loop?
            // Add checks for the monster's health for it to end when it reaches 0
            int chance = random.Next(1, 101); // 1-100
            if (chance <= 50) // 50% chance of monster attacking back- if chance is less than or equal to 50, the monster hits the player
            {
                Console.WriteLine("The monster attacks you!");
                // TO DO: PART OF ASSIGNEMNT: DAMAGE needs to be an INTERFACE
                // Modify player health and apply damage to here (say how much health they have left
                //Increment turn counter
            }
            else
            {
                Console.WriteLine("The monster misses!");
                //Increment turn counter
            }
        }

		// Check Google notes for notes on this class/plan it out
	}
}

