using System;
using System.Collections.Generic;

namespace Lab4_1DiceRolling
{
    class Program
    {
        static void Main(string[] args)
        {
            DiceRollerApp game = new DiceRollerApp();
            bool playing = true;

            Console.WriteLine("Welcome to the Grand Circus Casino!");
            Console.Write("How many sides should each die have? ");

            bool validInput = false;
            while (!validInput)
            {
                validInput = IsValidInteger(Console.ReadLine(), 1, Int32.MaxValue, out game.Sides);
            }

            while (playing)
            {
                game.Play();
                Console.Write("Roll again? (y/n) ");
                validInput = false;
                while(!validInput)
                {
                    string userInput = Console.ReadLine().ToUpper();
                    validInput = IsValidString(userInput, new List<string> { "y", "n" });
                    if (userInput == "N")
                    {
                        Console.WriteLine("Thanks for playing!!!");
                        playing = false;
                    }
                    Console.WriteLine();
                }
            }
        }

        static bool IsValidInteger(string userInput, int min, int max, out int parsedInput)
        {
            while (true)
            {
                // Using TryParse to attempt to get an integer from the string provided by the user.
                bool inputIsNumber = Int32.TryParse(userInput, out parsedInput);

                // If TryParse returns true and the parsed integer is greater than the minimum and less than or equal to max, then return true. If it is greater than or equal the maximum, then return false and direct the user to pick a smaller integer.
                if (inputIsNumber && parsedInput >= min)
                {
                    if (parsedInput > max)
                    {
                        Console.Write($"\nThat is not a valid amount. Please input a quantity no larger than {max}. The program can't handle anything larger... ");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                // If TryParse returns false, then tell the user to input a whole number.
                else if (!inputIsNumber)
                {
                    Console.Write($"Whoops, your input needs to be a whole number between {min} and {max}! Try again: ");
                    return false;
                }
                // If the parsed integer is less than 1, then direct the user to pick a number greater than 0.
                else if (parsedInput < min)
                {
                    Console.Write($"Whoops, your input needs to be at least {min}! Try again: ");
                    parsedInput = 0;
                    return false;
                }
                // Otherwise, return false.
                else
                {
                    return false;
                }
            }
        }

        static bool IsValidString(string userInput, List<string> validInputs)
        {
            bool validString = false;
            while (!validString)
            {
                validString = false;
                foreach (string validInput in validInputs)
                {
                    if (userInput.ToUpper() == validInput.ToUpper())
                    {
                        validString = true;
                    }
                }
                if (validString == false)
                {
                    Console.WriteLine("\nThat not a valid input, try again!");
                    break;
                }
            }
            return validString;
        }
    }

    class DiceRollerApp
    {
        public int Sides;
        private int Roll1;
        private int Roll2;
        private int Total;
        private int gameCounter;

        public void Play()
        {
            Random random = new Random();
            gameCounter++;
            Roll1 = random.Next(1, Sides + 1);
            Roll2 = random.Next(1, Sides + 1);
            Total = Roll1 + Roll2;

            if (Sides == 6)
            {
                PrintSix();
            }
            else
            {
                Print();
            }
        }

        private void Print()
        {
            Console.WriteLine($"Roll {gameCounter}:");
            Console.WriteLine($"You rolled a {Roll1} and a {Roll2} ({Total} total)");
        }

        private void PrintSix()
        {
            Print();
            if (Roll1 == 1 && Roll2 == 1)
            {
                Console.WriteLine("Snake Eyes!");
            }
            else if ((Roll1 == 1 && Roll2 == 2) || (Roll1 == 2 && Roll2 == 1))
            {
                Console.WriteLine("Ace Deuce!");
            }
            else if (Roll1 == 6 && Roll2 == 6)
            {
                Console.WriteLine("Box Cars!");
            }
            else if (Total ==  7 || Total == 11)
            {
                Console.WriteLine("Win!");
            }
            
            if (Total == 2 || Total == 3 || Total == 12)
            {
                Console.WriteLine("Craps!");
            }
        }
    }
}
