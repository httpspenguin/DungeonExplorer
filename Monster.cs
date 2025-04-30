using System;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    //TO DO: PART OF ASSIGNMENT; NEED TO MAKE CREATURE AN ABSTRACT CLASS THAT BOTH MONSTER AND PLAYER INHERIT FROM (Polymorphism)!!
    interface IDamageable // Interface for the damage method within battle (TO DO: FINISH IMPLEMENTATION)
    {
        void Damage(string name, int health, int amount); // Method to be implemented in the child classes- call damage in battle; have this as a switch/if/else for attacking monster/player (could have as PlayerTurn as a bool value to determine who takes damage? Or can it be more simple than that)
        // Added properties to be used as part of the interface
        int Health { get; set; }
        string Name { get; set; }
    }
    public abstract class Creature : IDamageable
    {
        public int health;
        public string description; // Will this be used in the player class too?
        public string name; // To be altered via polymorphism in the child classes/object instantiations
        
        public virtual void Damage(string name, int health, int amount)
        {
            //Error checking; Math.Max used to ensure that the damage doesn't go below 0. Regardless, a check for "health <= 0" will be applied too.
            int damageTaken = Math.Max(0, amount - health);
            Console.WriteLine($"{name} took {damageTaken} damage!");
        }

        // Implementation of the interface IDamageable properties
        public int Health { get { return health; } set { health = value; } }
        public string Name { get { return name; } set { name = value; } } 
    }

    public class Monster : Creature, IDamageable // Will be a parent class, the monsters for varying difficulties will be inherited from. If this class can't be inherited from multiple times, I'll change it into an interface
	{
		public Monster()
		{
            // Constructor -- setting up initial variables
            // TO DO: Change health to get/set for interface
            
            health = 100;
			description = "Sample description"; //Remove? Each monster (child classes) will have a different description
		}

        // FOR DEBUGGING (maybe as a test class?): Add a "skip battle" option. (break for loop if skip is entered? or would it bee effective in Game.cs?)

        public static void Battle(Player player, Monster monster) // Modified to accept player and monster objects instead of variables (would be more complicated with previous approach)
        {
            // PLANS; buncha TO DOs
            // >Turn based; Add a turn counter (additional to running away)?
            // >Add check for if the player is dead *(health equal or below 0)* or not (game should end. playing = false)
            // >Options for the player to take in battle: attack (plan for auto-equip message with knife/a weapon with better stats; need to add a check for that)
            // To add option to use healing item + make sure the player actually has it in their inventory to heal/error handling if they don't have any
            // If the player picked up a knife, it is automatically equipped and they can type in "attack" to use it
            // Need to add stats to attacking w/ a knife (and an alternative method if the player hasn't yet found a weapon- their hands, but it would cause less damage + dependant on the monster; inheritance values)
            // >Run should have a 50% chance of success. If fail, the player takes a hit for the next turn

            //TO DO [CURRENT]: Implement FOR loop to handle turns. if either player's/monster's HP is zero; BREAK the loop. Add a check after the battle that if the player hp = 0, the game ends.
            Random random = new Random();
            int maxPlayerTurn = random.Next(1, 9); // Turns can be between 1-8
            
            Console.WriteLine($"You encounter the {monster.Name}! {monster.description}");

            for (int playerTurn = 1; playerTurn <= maxPlayerTurn; playerTurn++) // After a certain amount of turns, the monster will get tired and flee. It'll be random but it'll be no less than 8 turns
            {
                //Player attacks monster
                Console.WriteLine("Your turn to strike!");
                int playerDamage = random.Next(10, 31); //Base damage if player doesn't have a weapon
                if (player.EquippedWeapon != null)
                {
                    playerDamage = player.EquippedWeapon.damage; // If player has weapon, this variable updates appropiately
                    Console.WriteLine($"Using your {player.EquippedWeapon.Name}, you attack!");
                }
                else
                {
                    Console.WriteLine("You attack with your bare hands!"); // If player doesn't have a weapon, this message is displayed
                }
                player.Damage(monster.Name, monster.health, playerDamage); // Call the damage method from the interface
                Console.WriteLine($"The {monster.Name} has {monster.health} health left.");

                //Checks if monster is dead both before max turns and before it attacks- so it doesn't attack the player when it's supposed to be dead
                if (monster.Health <= 0)
                {
                    Console.WriteLine($"The {monster.Name} has been defeated!");
                    Console.WriteLine("You are victorious!");
                }
                
                //Monster attacks player
                int chance = random.Next(1, 101); // 1-100
                if (chance <= 50) // 50% chance of monster attacking back- if chance is less than or equal to 50, the monster hits the player
                {
                    Console.WriteLine($"The {monster.Name} attacks you!");
                    monster.Damage(player.Name, player.Health, random.Next(1, 15));
                    Console.WriteLine($"You have {player.Health} health points left.");

                    //Player healing; have the first part of the loop in a healing statement and have healing as a bool value. Max out health for 120 so the player doesn't infinitely heal

                    // Check if player is dead
                    if (player.Health <= 0)
                    {
                        Console.WriteLine($"{player.Name} collapses...");
                        break; // End the battle loop.
                    }
                }
                else
                {
                    Console.WriteLine($"Phew, the {monster.Name} misses!");
                }
            }
            Console.WriteLine("The monster has fled!"); // If the player doesn't kill the monster, it flees after a certain amount of turns
        }
        // Polymorphism (two children for Monster classes); different monster classes with different damage dialogue
        public class Lamp : Monster
        {
            public Lamp()
            {
                health = 60;
                description = "Seems hostile. From the way it's moving, you presume it's angry.";
                name = "Lamp";
            }
            public override void Damage(string name, int health, int amount)
            {
                Console.WriteLine($"The {name} cracks!");
                base.Damage(name, health, amount); // Calling base class' Damage interface for core logic
            }
        }
        public class Chair : Monster
        {
            public Chair()
            {
                health = 80;
                description = "You hear the chair creaking eerily towards you. You think it might be a monster.";
                name = "Chair";
            }
            public override void Damage(string name, int health, int amount)
            {
                Console.WriteLine($"The {name} splinters!");
                base.Damage(name, health, amount); // Calling base class' Damage interface for core logic
            }
        }
    }
}
