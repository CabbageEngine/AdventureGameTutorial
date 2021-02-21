using System;

namespace TextAdventure
{
    public class Shop
    {

        public static void LoadShop(Player p)
        {

            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int armorP;
            int weaponP;
            int diffP;

            while (true)
            {

                potionP = 20 + 10 * p.mods;
                armorP = 100 * (p.armorValue + 1);
                weaponP = 100 * (p.weaponValue);
                diffP = 300 + 100 * p.mods;
                Console.Clear();
                Console.WriteLine("======== SHOP ========");
                Console.WriteLine("======================");
                Console.WriteLine("(W)eapon:         $" + weaponP);
                Console.WriteLine("(A)rmor:          $" + armorP);
                Console.WriteLine("(P)otions:        $" + potionP);
                Console.WriteLine("(D)ifficulty Mod: $" + diffP);
                Console.WriteLine("======================");
                Console.WriteLine("(E)xit");
                Console.WriteLine("(Q)uit Game");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(p.name + "'s Stats");
                Console.WriteLine("======================");
                Console.WriteLine("Current Health  " + p.health);
                Console.WriteLine("Total Coins    $" + p.coins);
                Console.WriteLine("Weapon Strength " + p.weaponValue);
                Console.WriteLine("Armor Toughness " + p.armorValue);
                Console.WriteLine("Potions         " + p.potions);
                Console.WriteLine("Difficulty Mods " + p.mods);
                Console.WriteLine("======================");
                // Wait for input
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potions")
                {
                    TryBuy("potions", potionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponP, p);

                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorP, p);

                }
                else if (input == "d" || input == "difficulty mod")
                {
                    TryBuy("dif", diffP, p);

                }
                else if (input == "q" || input == "quit")
                {
                    Program.Quit();
                }
                else if (input == "e" || input == "exit")
                {
                    break;
                }


            }
        }

        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "potions")
                    p.potions++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "dif")
                    p.mods++;


                p.coins -= cost;
            }
            else
            {
                Console.WriteLine("You don't have enough gold, go work the streets!");
                Console.ReadKey();
            }
        }

    }
}
