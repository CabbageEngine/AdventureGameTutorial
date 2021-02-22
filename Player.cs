namespace TextAdventure
{
    // Setting default values for player class
    public class Player
    {
        // Moving to Program.cs
        // public Random rand = new Random();

        // Default values
        public string name;
        public int saveid;
        public int coins = 1000;
        public int level = 1;
        public int xp = 0;
        public int health = 10;
        public int damage = 1;
        public int armorValue = 0;
        public int potions = 5;
        public int weaponValue = 1;

        public int mods = 0;

        // Creating different player classes - defining the options
        public enum PlayerClass { Mage, Ranger, Warrior };

        // Setting a default class
        public PlayerClass currentClass = PlayerClass.Warrior;

        // Adjusts Player stats in accordance to Modifiers
        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return Program.rand.Next(lower, upper);
        }

        public int GetPower()
        {
            int upper = (2 * mods + 2);
            int lower = (mods + 1);
            return Program.rand.Next(lower, upper);
        }

        public int GetCoins()
        {
            // Starting value of 50 with an increase of 15 * for each mod
            int upper = (15 * mods + 50);
            int lower = (10 * mods + 10);
            return Program.rand.Next(lower, upper);
        }

        // Creating an XP / Level method below
        public int GetXP()
        {
            int upper = (20 * mods + 50);
            int lower = (15 * mods + 10);
            return Program.rand.Next(lower, upper);
        }

        public int GetLevelUpValue()
        {
            return 100 * level + 400;
        }

        public bool CanLevelUp()
        {
            if (xp >= GetLevelUpValue())
                return true;
            else
                return false;
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                xp -= GetLevelUpValue();
                level++;
            }
            Program.Print("Congrats! You are now at level " + level + "! Congrats.");
        }

    }
}
