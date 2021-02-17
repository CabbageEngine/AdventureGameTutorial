using System;

namespace TextAdventure
{
    class Program
    {
        // Create a new instance of player from player class
        public static Player currentPlayer = new Player();
        static void Main(string[] args)
        {
            // This method initiates the instance below
            Start();

            // This method calls the Encounters class where it belongs to
            Encounters.FirstEncounter();
        }

        static void Start()
        {
            Console.WriteLine("Dastardly Dungeon");
            Console.Write("Name: ");
            currentPlayer.name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("You awake to a cold, stone, dark room. You " +
                "feel dazed and are having trouble remembering anything about " +
                "your past.\n");
            if (currentPlayer.name == "")
                Console.WriteLine("You can't even remember your own name...");

            else
                Console.WriteLine("You know your name is " + currentPlayer.name);

            // Wait until a key is pressed
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You feel around in the dark  nothingness until you grasp" +
                "the outlines of a door handle. Trying to turn handle, you feel some " +
                "resistance before hearing the lock break from the slight pressure.\n");
            Console.WriteLine("Opening the door, you see your captor standing with their" +
                "back to you outside the door.\n");

        }
    }
}
