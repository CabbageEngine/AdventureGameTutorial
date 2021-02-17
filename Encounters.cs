using System;

namespace TextAdventure
{
    class Encounters
    {
        // Best practice to create a single instance of Random and use it throughout program
        //      otherwise results may not seem as random
        static Random rand = new Random();
        // Encounter Generic



        // Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine("You throw open the door, charging towards the figure of your " +
                "presumed captor. The ambient light from the hallway floods the opening " +
                "of the door. As your body emerges from the silhouetted passage, you spy " +
                "a rusty nail embedded in the exterior frame of the door. You grab one end " +
                "of the nail by tossing your hand over your shoulder and using your momentum " +
                "to pull the nail out. A process that offered no resistance but was quite " +
                "dramatic to witness.");
            Console.WriteLine("The sound of the door being flung open grabs the attention of " +
                "the rear-facing figure. They begin to turn towards the direction of the sound.");
            Console.ReadKey();

            // Set combat to not initiate first, set string name, power of 1 and health of 4
            Combat(false, "Human Rogue", 1, 4);

        }


        // Encounter Tools
        // Stored in Player.cs class. Combat interaction
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;
            if (random)
            {

            }
            else
            {
                n = name;
                p = power;
                h = health;
            }

            // Starting while loop for menu and combat
            while (h > 0)
            {
                // Menu display output
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine(p + "/" + h);
                Console.WriteLine("======================");
                Console.WriteLine("| (A)ttack (D)efend  |");
                Console.WriteLine("|   (R)un  (H)eal    |");
                Console.WriteLine("======================");
                Console.WriteLine("Potions: " + Program.currentPlayer.potions + "  Health " +
                    Program.currentPlayer.health);
                string input = Console.ReadLine();

                // If player types A to Attack
                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    // Attack exchange
                    Console.WriteLine("With haste you surge foward with your nail. " +
                        "As you pass, the " + n + " strikes you as you enter their range.");
                    int damage = p - Program.currentPlayer.armorValue;

                    // If damage is scored but results in negative damage
                    if (damage < 0)
                        damage = 0;

                    // 
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4);
                    Console.WriteLine("You lose" + damage + " health and deal " + attack + "");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    // defend
                    Console.WriteLine("As the " + n + " prepares to strike you as you pass. " +
                        "You ready your sword in a defensive stance");
                    int damage = (p / 4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                    Console.WriteLine("You lose" + damage + " health and deal " + attack + "");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    // run
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("As you sprint away from the " + n + ", it catches you" +
                            " in the back sending you sprawling to the ground.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health and are unable to escape.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("You use your crazy ninja moves to evade the " +
                            name + " and you successfully escape!");
                        Console.ReadKey();
                        // go to store

                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    // heal
                    if (Program.currentPlayer.potions == 0)
                    {
                        Console.WriteLine("You try in vain to find a potion but your bag is empty.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("The " + n + " pokes you with something long, causing a " +
                            "loss of " + damage + " health!");
                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and pull out a glowing, purple" +
                            " Flask. You drink its purple contents.");
                        int potionV = 5;
                        Console.WriteLine("You gain " + potionV + " health");
                        Program.currentPlayer.health += potionV;
                        Console.WriteLine("While you were occupied, the " + n + " makes a move.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("Using its probing fingers, the " + n +
                            " touches you in your special place causing " + damage + " to health!");
                    }
                    Console.ReadKey();
                }

                Console.ReadKey();
            }
        }
    }
}
