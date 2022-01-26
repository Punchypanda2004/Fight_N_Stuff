using System;

namespace Fight_N_Stuff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Fight N Stuff, a game where you fight stuff to get stuff to fight stuff.");
            Console.WriteLine("1.PLAY");
            Console.WriteLine("2.RULES");
            Console.WriteLine("3.EXIT");
            Console.WriteLine("Type the number to Choose one of the options above.");
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                Console.WriteLine("Choose a difficulty");
                Console.WriteLine("Easy   Normal   Hard");
                // Recives difficulty input from player
                string diff = Console.ReadLine();
                Difficulty.applyDifficulty(diff.ToLower());
                Console.WriteLine("You have chosen: " + diff);
                Console.WriteLine("Choose a name for your character.");
                // Player names character
                string charName = Console.ReadLine();
                Console.WriteLine("Now choose a class for your.");
                Console.WriteLine("Berserker, Paladin, Rogue");
                // Player asigns class to character
                string charClass = Console.ReadLine();
                Charater player = new Charater(charClass.ToLower(), charName);
                Console.WriteLine("Hello " + player.name + ". You are starting your journey in the land of WHO CARES ABOUT STORY! ONLY FIGHT!");
                Console.WriteLine("YOU ARE FIGHT NOW!");
                for (int i = 0; i < 10; i++)
                {
                    InitializeFight.ActivateFight(player.hp, player.atk, player.def, player.dead, player.name);
                }
            }
        }
    }

    //Make Enemies to fight
    public class InitializeFight
    {
        static public void ActivateFight(double playerHp, double playerAtk, double playerDef, bool playerDead, string playerName)
        {
            Difficulty.levelOn += 1;
            Enemies monster1 = new Enemies(IfElemental());
            Enemies monster2 = new Enemies(IfElemental());
            Enemies monster3 = new Enemies(IfElemental());
            bool[] monsterDead = { monster1.dead, monster2.dead, monster3.dead };
            double[] monsterHp = { monster1.hp, monster2.hp, monster3.hp };
            while (AllDead(monsterHp, monsterDead) == false)
            {
                Console.WriteLine("Choose an Enemy to attack! (Just type in the number of the enemy!)");
                if (monsterDead[0] == false)
                {
                    Console.WriteLine("Enemy 1 >:( " + monsterHp[0]);
                }
                if (monsterDead[1] == false)
                {
                    Console.WriteLine("Enemy 2 >:( " + monsterHp[1]);
                }
                if (monsterDead[2] == false)
                {
                    Console.WriteLine("Enemy 3 >:( " + monsterHp[2]);
                }
                string whichMonster = Console.ReadLine();

                if (whichMonster == "1")
                {
                    double damage = playerAtk - monster1.def;
                    monsterHp[0] -= damage;
                    Console.WriteLine("You attcked Enemy 1 for " + damage + " damage!");
                }
                else if (whichMonster == "2")
                {
                    double damage = playerAtk - monster2.def;
                    monsterHp[1] -= damage;
                    Console.WriteLine("You attcked Enemy 2 for " + damage + " damage!");
                }
                else if (whichMonster == "3")
                {
                    double damage = playerAtk - monster3.def;
                    monsterHp[2] -= damage;
                    Console.WriteLine("You attcked Enemy 3 for " + damage + " damage!");
                }
                else
                {
                    Console.WriteLine("ERR. Enemy doesn't exist or is dead. Please type in one of the options.");
                }

                Console.WriteLine("The monsters attck You!");

                if (monsterDead[0] == false)
                {
                    double damage = monster1.atk - playerDef;
                    playerHp -= damage;
                    Console.WriteLine("You have been hit by Enemy 1 for " + damage + " damage!");
                }
                if (monsterDead[1] == false)
                {
                    double damage = monster2.atk - playerDef;
                    playerHp -= damage;
                    Console.WriteLine("You have been hit by Enemy 2 for " + damage + " damage!");
                }
                if (monsterDead[2] == false)
                {
                    double damage = monster3.atk - playerDef;
                    playerHp -= damage;
                    Console.WriteLine("You have been hit by Enemy 3 for " + damage + " damage!");
                }
                
                if (playerHp <= 0)
                {
                    Console.WriteLine("Bad Luck! You be dead.");
                    playerDead = true;
                }
            }
        }

        static public bool IfElemental()
        {
            Random random1 = new Random();
            int ifElmnt = random1.Next() * (100 - 1) + 1;
            if (ifElmnt <= Difficulty.elmntChance)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        static public bool AllDead(double[] monsterHp, bool[] monsterDead)
        {
            for (int i = 0; i < monsterHp.Length; i++)
            {
                monsterDead[i] = Enemies.IfDead(monsterHp[i]);
            }

            if (monsterDead[0] == true && monsterDead[1] == true && monsterDead[2] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //Asigns the difficulrt
    public class Difficulty
    {
        static public double diffCof;
        static public int levelOn = 0;
        static public int elmntChance;
        static public void applyDifficulty(string difficulty)
        {
            if (difficulty == "easy")
            {
                diffCof = 1;
                elmntChance = 5;
            }
            else if (difficulty == "normal")
            {
                diffCof = 1.25;
                elmntChance = 15;
            }
            else if (difficulty == "hard")
            {
                diffCof = 1.50;
                elmntChance = 30;
            }
            else
            {
                Console.WriteLine("ERR. That is not a difficulty.");
            }
        }
    }
    
    // Creates Enemy Objects
    public class Enemies
    {
        public double hp = 100;
        public double atk = 10;
        public double def = 2;
        public bool dead;

        //string elementType: REMEBER TO ADD THIS OR REMOVE THIS

        //Create basic enemy type
        public Enemies(bool elemental)
        {
            dead = false;
            if (elemental == false)
            {
                hp = hp + (Difficulty.levelOn * 10 - 10);
                atk = atk + (Difficulty.levelOn * 1 - 1);
                def = def + (Difficulty.levelOn * 0.5 - 0.5);
            }
            if (elemental == true)
            {
                hp = (hp + (Difficulty.levelOn * 10 - 10)) * Difficulty.diffCof;
                atk = (atk + (Difficulty.levelOn * 1 - 1)) * Difficulty.diffCof;
                def = (def + (Difficulty.levelOn * 0.5 - 0.5)) * Difficulty.diffCof;
            }
        }
        public static bool IfDead(double enemyHp)
        {
            if (enemyHp <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
    public class Charater
    {
        public double hp = 250;
        public double atk = 15;
        public double def = 3; 
        public bool dead;
        public string name;

        public Charater(string charClass, string charName)
        {
            name = charName;
            if (charClass == "berserker")
            {
                hp = hp + 150;
                atk = atk + 4;
                def = def - 3;
            }
            else if (charClass == "paladin")
            {
                hp = hp + 25;
                atk = atk + 1;
                def = def + 3;
            }
            else if (charClass == "rogue")
            {
                hp = hp - 50;
                atk = atk + 10;
                def = def - 2;
            }
        }
        public static bool IfDead(double yourHp)
        {
            if (yourHp <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
