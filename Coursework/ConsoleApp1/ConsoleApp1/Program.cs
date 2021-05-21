using System;
using System.IO;
using System.Threading;

namespace COURSEWORK
{

    class Character
    {
        private int Skill, Stamina, Luck;
        private string Name;
        public Character(string Nam, int Skl, int Sta, int Lck)// Constructor
        {
            Name = Nam;
            Skill = Skl;
            Stamina = Sta;
            Luck = Lck;
        }
        public int getSkl() // Getters
        {
            return Skill;
        }
        public string getName()
        {
            return Name;
        }
        public int getSta()
        {
            return Stamina;
        }
        public int getLuck()
        {
            return Luck;
        }
        public void setSkl(int n)// Setters
        {
            Skill = n;

        }
        public void setSta(int n)
        {
            Stamina = n;
        }
        public void setLuck(int n)
        {
            Luck = n;
        }
    }

    class Program
    {
        const string FILEPATH = @"C:\Coursework\1Chars.csv";
        static Character createCharacter()
        {
            Console.WriteLine("Enter name for your character: ");
            string Name = Console.ReadLine();
            int Skill = rollDice(1) + 6;
            int Stamina = rollDice(2) + 12;
            int Luck = rollDice(1) + 6;
            Character newChar = new Character(Name, Skill, Stamina, Luck);
            saveCharacter(newChar);
            newChar = loadCharacter(Name);
            return newChar;
        }
        static Character loadCharacter(string characterName)
        {
            bool isValid = false;
            int Skill = 0, Stamina = 0, Luck = 0;
            string[] lines = File.ReadAllLines(FILEPATH);//Puts each line from the file into the array
            string[] temp = { };
            Character newChar = new Character(characterName, Skill,Stamina,Luck);
            foreach (string line in lines)
            {
                temp = line.Split(',');//splits the line where there is a comma and puts value into the array
                if (temp[0] == characterName)//checks if the first value of the line is the name inputted by user
                {
                    isValid = true;
                    Skill = int.Parse(temp[1]);
                    Stamina = int.Parse(temp[2]);
                    Luck = int.Parse(temp[3]);
                }
            }
            if (isValid)
            {
                newChar = new Character(characterName, Skill, Stamina, Luck);//creates new object with saved info taken from array
                Console.WriteLine("Loading character: {0}...", characterName);
                Thread.Sleep(1000);
                Console.WriteLine("Skill: " + newChar.getSkl());
                Console.WriteLine("Stamina: " + newChar.getSta());
                Console.WriteLine("Luck: " + newChar.getLuck());
                // start game function goes here
                return newChar;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Character name not found.\n");
                mainMenu();
                return newChar;
            }
        }
        static void saveCharacter(Character playerCharacter)
        {
            StreamWriter wFile;
            string charName = playerCharacter.getName();
            int charAtk = playerCharacter.getSkl();
            int charDef = playerCharacter.getSta();
            int charLuck = playerCharacter.getLuck();
            wFile = new StreamWriter(File.Open(FILEPATH, FileMode.Append));
            wFile.WriteLine("{0},{1},{2},{3}", charName, charAtk, charDef, charLuck);
            wFile.Close();
        }
        static Character mainMenu()
        {
            Character newChar = new Character("", 0, 0, 0);
            bool validInput = false;
            while (validInput == false)
            {
                Console.WriteLine("1. Create New Character");
                Console.WriteLine("2. Load Saved Character");
                Console.WriteLine("9. Quit");
                string input = Console.ReadLine();
                bool badtype = false;
                int numInput;
                try
                {
                    numInput = int.Parse(input);
                }
                catch
                {
                    badtype = true;
                }
                if (!badtype)
                {
                    numInput = int.Parse(input);
                    if (numInput == 1)
                    {
                        validInput = true;
                        newChar = createCharacter();
                        return newChar;
                    }
                    else if (numInput == 2)
                    {
                        validInput = true;
                        Console.WriteLine("Enter character name: ");
                        string name = Console.ReadLine();
                        newChar = loadCharacter(name);
                        return newChar;
                    }
                    else if (numInput == 9)
                    {
                        validInput = true;
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please choose a valid option..");
                    }
                }
                else if (badtype)
                {
                    Console.WriteLine("bad type");
                }
            }
            return newChar;
        }
        static int rollDice(int noOfRolls)
        {
            int diceRoll = 0;
            Random rnd = new Random();
            for (int i = 1; i <= noOfRolls; i++)
            {
                diceRoll += rnd.Next(1, 7);
            }
            return diceRoll;
        }
        static void dilemma(ref int currentPlace)
        {
            Console.WriteLine("");
            int input = 0;
            bool badtype = false;
            try
            {
                input = int.Parse(Console.ReadLine());
            }
            catch
            {
                badtype = true;
            }
            if (!badtype)
            {
                if (input == 1)
                {
                    currentPlace++;
                }
                else if (input == 2)
                {
                    currentPlace += 2;
                }
                else
                {
                    Console.WriteLine("Incorrect input.");
                    badtype = true;
                }
            }
            else
            {
                while (badtype)
                {
                    bool badtype2 = false;
                    Console.WriteLine("bad type.");
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        badtype2 = true;
                    }
                    if (!badtype2)
                    {

                        if (input == 1)
                        {
                            currentPlace++;
                            badtype = false;
                            badtype2 = false;
                        }
                        else if (input == 2)
                        {
                            currentPlace += 2;
                            badtype = false;
                            badtype2 = false;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input.");
                            badtype2 = true;
                        }
                    }
                }
            }
            

        }
        static bool combat(Character newChar, int enemySkill, int enemyStamina)
        {
            while(newChar.getSta() > 0 && enemyStamina > 0)
            {
                bool badtype = false;
                int input = 0;
                int myRoll = rollDice(1) + newChar.getSkl();
                int enemyRoll = rollDice(1) + enemySkill;
                if (myRoll > enemyRoll)
                {
                    Console.WriteLine("You have wounded the creature, test luck? 1 for yes, 2 for no: ");
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        badtype = true;
                    }
                    if (!badtype)
                    {
                        if (input == 1)
                        {
                            bool lucky = testLuck(newChar.getLuck());
                            newChar.setLuck(newChar.getLuck() - 1);
                            if (lucky)
                            {
                                Console.WriteLine("Lucky! Damage doubled.");                               
                                enemyStamina -= 4;
                                Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                            }
                            else
                            {
                                enemyStamina -= 1;
                                Console.WriteLine("Unlucky! Damage halved.");
                                Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                            }
                        }
                        else
                        {
                            enemyStamina -= 2;
                            Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad type.");
                    }
                    while (badtype)
                    {
                        bool badtype2 = false;
                        Console.WriteLine("You have wounded the creature, test luck? 1 for yes, 2 for no: ");
                        try
                        {
                            input = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            badtype2 = true;
                        }
                        if (!badtype2)
                        {
                            if (input == 1)
                            {
                                bool lucky = testLuck(newChar.getLuck());
                                newChar.setLuck(newChar.getLuck() - 1);
                                if (lucky)
                                {
                                    enemyStamina -= 4;
                                    Console.WriteLine("Lucky! Damage doubled.");
                                    Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                                }
                                else
                                {
                                    enemyStamina -= 1;
                                    Console.WriteLine("Unlucky! Damage halved.");
                                    Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                                }
                            }
                            else
                            {
                                enemyStamina -= 2;
                                Console.WriteLine("Enemy has {0} stamina left.", enemyStamina);
                            }
                            badtype = false;
                            badtype2 = false;
                        }
                        else
                        {
                            Console.WriteLine("Bad type.");
                        }
                    }

                }
                else if (myRoll < enemyRoll)
                {
                    Console.WriteLine("You have been wounded, test luck? 1 for yes, 2 for no: ");
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        badtype = true;
                    }
                    if (!badtype)
                    {
                        if (input == 1)
                        {
                            bool lucky = testLuck(newChar.getLuck());
                            newChar.setLuck(newChar.getLuck() - 1);
                            if (lucky)
                            {
                                Console.WriteLine("Lucky! Damage to you halved.");
                                newChar.setSta(newChar.getSta()-1);
                                Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                            }
                            else
                            {
                                newChar.setSta(newChar.getSta()-4);
                                Console.WriteLine("Unlucky! Damage to you doubled.");
                                Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                            }
                        }
                        else
                        {
                            newChar.setSta(newChar.getSta()-2);
                            Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad type.");
                    }
                    while (badtype)
                    {
                        Console.WriteLine("You have been wounded, test luck? 1 for yes, 2 for no: ");
                        try
                        {
                            input = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            badtype = true;
                        }
                        if (!badtype)
                        {
                            badtype = false;
                            if (input == 1)
                            {
                                bool lucky = testLuck(newChar.getLuck());
                                newChar.setLuck(newChar.getLuck() - 1);
                                if (lucky)
                                {
                                    newChar.setSta(newChar.getSta()-1);
                                    Console.WriteLine("Lucky! Damage to you halved.");
                                    Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                                }
                                else
                                {
                                    newChar.setSta(newChar.getSta()-4);
                                    Console.WriteLine("Unlucky! Damage to you doubled.");
                                    Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                                }
                            }
                            else
                            {
                                newChar.setSta(newChar.getSta()-2);
                                Console.WriteLine("You has {0} stamina left.", newChar.getSta());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bad type.");
                        }
                    }
                    
                }
                
            }
            if (newChar.getSta() <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        static bool testLuck(int luck)
        {
            int number = rollDice(2);
            if(number <= luck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool testSkill(int skill)
        {
            int number = rollDice(2);
            if(number <= skill)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string[] loadDescriptions()
        {
            string filepath = @"C:\Coursework\descriptions.csv";
            string[] lines = File.ReadAllLines(filepath);
            return lines;
        }
        static int[,] loadDirections()
        {
            int[,] directions = new int[26,6];
            string[] temp = { };
            string filepath = @"C:\Coursework\directions.csv";
            string[] lines = File.ReadAllLines(filepath);
            int count = 0;
            foreach(string line in lines)
            {
                temp = line.Split(",");
                directions[count, 0] = int.Parse(temp[0]);
                directions[count, 1] = int.Parse(temp[1]);
                directions[count, 2] = int.Parse(temp[2]);
                directions[count, 3] = int.Parse(temp[3]);
                directions[count, 4] = int.Parse(temp[4]);
                directions[count, 5] = int.Parse(temp[5]);
                count++;
            }
            return directions;
        }
        static void Main(string[] args)
        {
            int currentPlace = 0;
            Character newChar = mainMenu();
            string[] descriptions = loadDescriptions();
            int[,] directions = loadDirections();
            while (directions[currentPlace, 2] != 5)
            {
                Console.WriteLine("\n");
                Console.WriteLine(descriptions[currentPlace]);
                if (directions[currentPlace,2] == 0)
                {
                    currentPlace = directions[currentPlace, 0];
                }
                else if (directions[currentPlace, 2] == 1)
                {
                    dilemma(ref currentPlace);
                }
                else if (directions[currentPlace, 2] == 2)
                {
                    bool win = testSkill(newChar.getSkl());
                    if (!win)
                    {
                        currentPlace = directions[currentPlace, 0];
                    }
                    else if (win)
                    {
                        currentPlace = directions[currentPlace, 1];
                    }
                }
                else if (directions[currentPlace, 2] == 3)
                {
                    bool lucky = testLuck(newChar.getLuck());
                    newChar.setLuck(newChar.getLuck() - 1);
                    if (!lucky)
                    {
                        currentPlace = directions[currentPlace, 0];
                    }
                    else if (lucky)
                    {
                        currentPlace = directions[currentPlace, 1];
                    }
                }
                else if (directions[currentPlace, 2] == 4)
                {
                    int number = rollDice(1);
                    if (currentPlace == 7)
                    {
                        if (number == 6)
                        {
                            currentPlace = directions[currentPlace, 1];
                        }
                        else
                        {
                            currentPlace = directions[currentPlace, 0];
                        }
                    }
                    else
                    {
                        if (number <= 3)
                        {
                            currentPlace = directions[currentPlace, 0];
                        }
                        else
                        {
                            currentPlace = directions[currentPlace, 1];
                        }
                    }

                }
                else if (directions[currentPlace, 2] == 6)
                {
                    if (currentPlace == 16)
                    {
                        bool win = combat(newChar, 9, 10);
                        if (!win)
                        {
                            currentPlace = directions[currentPlace, 0];
                            Console.WriteLine(descriptions[currentPlace]);
                            Environment.Exit(1);
                        }
                        else
                        {
                            currentPlace = directions[currentPlace, 1];
                        }
                    }
                    else if (currentPlace == 20)
                    {
                        bool win = combat(newChar, 10, 8);
                        if (!win)
                        {
                            currentPlace = directions[currentPlace, 1];
                            Console.WriteLine(descriptions[currentPlace]);
                            Environment.Exit(1);
                        }
                        else
                        {
                            currentPlace = directions[currentPlace, 0];
                        }
                    }
                }
            }
            Console.WriteLine(descriptions[currentPlace]);
        }
    }
}