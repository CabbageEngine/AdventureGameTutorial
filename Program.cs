using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TextAdventure
{

    // Means that the player data can be saved to a file
    [Serializable]
    class Program
    {
        public static Random rand = new Random();


        // Create a new instance of player from player class
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {

            // Creating checking and saves directory - both names need to be identical
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }

            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();

            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Console.WriteLine("The Dungeon of Dangerous Delights\n");
            Console.Write("Victim's Name: ");
            p.name = Console.ReadLine();

            // Creating a class system
            Console.WriteLine("Class: Mage  Ranger  Warrior");
            bool flag = false;
            while (flag == false)
            {
                // Will break out of loop if any inputs are correct
                flag = true;
                string input = Console.ReadLine().ToLower();
                if (input == "mage")
                    p.currentClass = Player.PlayerClass.Mage;
                else if (input == "ranger")
                    p.currentClass = Player.PlayerClass.Ranger;
                else if (input == "warrior")
                    p.currentClass = Player.PlayerClass.Warrior;
                else
                {
                    Console.WriteLine("Please choose a correct class.");
                    // Loop will keep running as long as flag is false
                    flag = false;
                }

            }

            p.saveid = i;
            Console.Clear();

            Console.WriteLine(" You awake to a cold, stone, dark room. You " +
                "feel dazed\n and are having trouble remembering anything about " +
                "your past.\n");
            if (p.name == "")
                Console.WriteLine(" With much difficulty, try as you might, you cannot " +
                    "remember your own name...\n");

            else
                Console.WriteLine(" You only seem to vaguely recall that your slave name is "
                    + p.name + "!\n");
            Console.WriteLine(" For a moment you pause to consider why it's a slave name. " +
                "After deciding\n you got nothing, you shrug and move on.");

            // Wait until a key is pressed
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(" You feel around in the dark nothingness until you grasp\n " +
                "the outlines of a door handle. Trying to turn handle, you feel some\n " +
                "resistance before hearing the lock break from the slight pressure.\n");
            Console.WriteLine(" Peeking through the slightly ajar door, you spy your presumed\n " +
                "captor standing with their back to you a few paces outside the entry.\n");
            return p;

        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }


        // Create a save file from a unique player id set in player.cs
        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.saveid.ToString() + ".play";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }

        // Load a save file
        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                // Takes a file and verifies that it can be accepted
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            // Increment the save count
            idCount = players.Count;

            // Creating an infinite loop
            while (true)
            {
                Console.Clear();

                // Use Print instead of Console.Writeline for style, adding 60 is speed of text
                Print("Choose your player file: ", 60);

                foreach (Player p in players)
                {
                    Console.WriteLine(p.saveid + ": " + p.name + p.coins);
                }

                // Convert from Console.Writeline to Print using function at bottom for style
                Print("Please input player name or id (id:# or playername) or you can\n " +
                    "type 'create' to start a new save.\n");


                // Split takes a string and splits it by character
                string[] data = Console.ReadLine().Split(':');

                // Try Catch block to catch user entries that fail format
                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.saveid == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that ID!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                            Console.ReadKey();
                        }
                    }
                    // Creates a new instance of a character
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;

                        // This method calls the Encounters class where it belongs to
                    }

                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that ID!");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                    Console.ReadKey();
                }
            }
        }

        // This function creates a text style that types out the text
        // 40 is 40ms to spell out the full text. 
        // This function replaces Console.Writeline() with Print()
        public static void Print(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
        }

        public static void ProgressBar(string fillerChar, string backgroundChar, decimal value, int size)
        {
            int diffValue = (int)(value * size);
            for (int i = 0; i < 100; i++)
            {
                if (i < diffValue)
                    Console.Write(fillerChar);
                else
                    Console.Write(backgroundChar);
            }
        }
    }
}
