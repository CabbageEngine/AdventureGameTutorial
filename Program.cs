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
                Console.WriteLine("Choose your player file: ");

                foreach (Player p in players)
                {
                    Console.WriteLine(p.saveid + ": " + p.name + p.coins);
                }
                Console.WriteLine("Please input player name or id (id:# or playername).\n " +
                    "Create will start a new save.");


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
    }
}
