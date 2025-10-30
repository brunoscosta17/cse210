using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int favoriteNumber = PromptUserNumber();

        int squared = SquareNumber(favoriteNumber);

        DisplayResult(userName, squared);
    }

    // 1) Displays the welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // 2) Asks for and returns the user's name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine() ?? string.Empty;
        return name;
    }

    // 3) Asks for and returns the user's favorite number (as an integer)
    static int PromptUserNumber()
    {
        while (true)
        {
            Console.Write("Please enter your favorite number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Please enter a valid whole number.");
        }
    }

    // 4) Accepts an integer and returns that number squared
    static int SquareNumber(int value)
    {
        return value * value;
    }

    // 5) Accepts the user's name and the squared number and displays them
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");
    }
}
