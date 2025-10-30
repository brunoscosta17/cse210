using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Guess My Number (1–100) ===");

        // We’ll loop the whole game while the player wants to play again.
        string playAgain;
        do
        {
            // 1) Generate a random magic number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101); // upper bound is exclusive

            // 2) Ask for guesses until the user gets it right
            int attempts = 0;
            int guess = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();

                // Be robust: avoid exceptions if input is not a number
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a valid whole number.");
                    continue; // ask again without counting an attempt
                }

                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            // 3) Show how many attempts the user made (stretch)
            Console.WriteLine($"Guesses taken: {attempts}");

            // 4) Ask if the user wants to play again (stretch)
            Console.Write("Do you want to play again? (yes/no) ");
            playAgain = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

            Console.WriteLine(); // blank line between rounds

        } while (playAgain == "yes");

        Console.WriteLine("Thanks for playing!");
    }
}
