using System;

namespace TextAdventure
{
    public class Encounters
    {
        // Best practice to create a single instance of Random and use it throughout program
        //      otherwise results may not seem as random
        static Random rand = new Random();
        // Encounter Generic



        // Encounters
        public static void FirstEncounter()
        {
            Console.WriteLine(" You throw open the door and charge towards the outlined figure\n " +
                "nearby. The ambient light from the hallway floods the opening of the door. \n" +
                " As your body emerges from the silhouetted passage, you spy a feather duster \n" +
                " laying on a chair nearby. In one effortless move, you grab the cleaning utensil\n " +
                "and hold it high in the air like an imaginary sword.\n");
            Console.WriteLine(" The sound of the door being flung open and what could only be\n " +
                "described as the distressed warblings of a turkey quickly draws the figure's\n " +
                "attention. They turn to face you with a broad, toothy grin. \n");
            Console.WriteLine(" You ready yourself for battle with a Human Rogue.");
            Console.ReadKey();

            // Set combat to not initiate first, set string name, power of 1 and health of 4
            Combat(false, "Human Rogue", 1, 4);

        }

        public static void BasicFightEncounter()
        {
            Console.Clear();
            Console.WriteLine(" You turn the corner and there you see another challenger...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Console.WriteLine(" The door creaks open as you force your eyeball past its sockets to\n " +
                "peek past the tiny opening. You spy an old man stroking his beard and murmuring\n " +
                "delightfully to himself.\n");
            Console.WriteLine(" Suddenly, he stands to face you, wiping his hands on his wizard robe\n " +
                "while mutttering about finding \"privacy\" and problems with people rushing to\n " +
                "judgement.\n");
            Console.WriteLine(" You get ready to face a Dark Wizard with a bruised ego.\n");
            Console.ReadKey();
            Combat(false, "Dark Wizard", 4, 2);
        }

        // Encounter Tools

        public static void RandomEncounter()
        {
            switch (rand.Next(0, 2))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
            }
        }

        // Stored in Player.cs class. Combat interaction
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;
            if (random)
            {
                n = GetName();
                // p = rand.Next(1, 5); - deprecated code
                p = Program.currentPlayer.GetPower();
                // h = rand.Next(1, 8); - deprecated code
                h = Program.currentPlayer.GetHealth();
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
                Console.Clear();

                // Display enemy target and stats
                Console.WriteLine(n);
                Console.WriteLine(p + "/" + h);

                // Menu display ascii 
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
                    Console.WriteLine(" With haste you surge foward with your feather duster.\n " +
                        "As you draw closer, the " + n + " was able to grope you. You squeal out!\n");
                    int damage = p - Program.currentPlayer.armorValue;

                    // If damage is scored but results in negative damage
                    if (damage < 0)
                        damage = 0;

                    // Calculating damage scored vs weapon value 
                    // If player is a Warrior, they get a +2 towards attack damage
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Warrior) ? 2 : 0);
                    Console.WriteLine(" You lose " + damage + " health and deal " + attack + " damage.");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    // defend
                    Console.WriteLine(" As the " + n + " prepares to strike, you assume a\n " +
                        "wide stance. You hold your feather duster in front of you defensively.\n");

                    // Calculating damage received mitigated by armor Value
                    int damage = (p / 4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4) / 2;
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage.");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    // Run Away
                    // If current class is not a ranger and the player fails to run away
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Ranger && rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine(" As you sprint away from the " + n + ", it catches you\n" +
                            " in the rear sending you sprawling to the ground. You bruise your knees.\n");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health and are unable to escape.\n");
                        Program.currentPlayer.health -= damage;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(" Using an expertly practiced bunny hop and twirl, you evade the\n " +
                            n + " and successfully escape!\n");
                        Console.ReadKey();
                        // go to store
                        Shop.LoadShop(Program.currentPlayer);

                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    // health
                    if (Program.currentPlayer.potions == 0)
                    {
                        Console.WriteLine(" You try in vain to find a potion, but your bag is empty.\n");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine(" The " + n + " pokes you with something long and fleshy, causing a " +
                            "loss of " + damage + " health!\n");
                        Program.currentPlayer.health -= damage;

                    }
                    else
                    {
                        Console.WriteLine(" You reach into your bag and pull out a purple liquid contained in a" +
                            " flask. You drink\n its purple contents and contemplate the life of DJ Screw.\n");

                        // Checks for Player Class Mage and adds healing of +4 bonus or adds nothing if not Player Class Mage
                        int potionV = rand.Next(3, 6) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Mage) ? +4 : 0);
                        Console.WriteLine("You gain " + potionV + " health");
                        Program.currentPlayer.health += potionV;
                        Program.currentPlayer.potions -= 1;

                        // If you drink a health potion, you receive one attack from enemy
                        Console.WriteLine("While you were occupied, the " + n + " makes a move.\n");

                        // Setting damage received to be  half of what is incurred. 
                        int damage = p / 2 - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine(" Using its probing fingers, the " + n +
                            " touches you in your special place causing " + damage + " damage to health!\n");
                        Program.currentPlayer.health -= damage;

                    }
                    Console.ReadKey();
                }
                if (Program.currentPlayer.health <= 0)
                {
                    // Death code
                    Console.WriteLine(" The " + n + " stands above you victorious. The last thing you see is the\n " +
                        n + "'s probing, beady eyes relishing the delights that is to come next. You have fainted.\n");
                    Console.ReadKey();
                    System.Environment.Exit(0);

                }
                Console.ReadKey();
            }
            // int c = rand.Next(10, 50); - deprecated code
            int c = Program.currentPlayer.GetCoins();

            // Getting XP Method values from Player.cs File
            int x = Program.currentPlayer.GetXP();
            Console.WriteLine(" As you stand victorious over the " + name + ", it's body dissolves\n " +
                "into a putrid, pink mist, leaving behind gold " + c + " coins on the ground. You\n " +
                "have also gained " + x + " XP!");
            Program.currentPlayer.coins += c;
            Program.currentPlayer.xp += x;

            // Checks to see if player can level up
            if (Program.currentPlayer.CanLevelUp())
                Program.currentPlayer.LevelUp();

            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    return "Skeleton";
                case 1:
                    return "Zombie";
                case 2:
                    return "Cultist";
                case 3:
                    return "Grave Robber";
            }
            return "Human Rogue";
        }
    }
}
