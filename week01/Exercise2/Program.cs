using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt the user
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();

        // Be robust: TryParse avoids a runtime exception if the user types non-numeric input
        if (!int.TryParse(input, out int percentage))
        {
            Console.WriteLine("Invalid input. Please enter a whole number (e.g., 87).");
            return;
        }

        // ---- Core requirement: determine the letter (A, B, C, D, F)
        string letter;

        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // ---- Stretch: determine + / - sign (with special rules)
        // Rule: "+" if last digit >= 7; "-" if last digit < 3; otherwise no sign.
        // BUT: There is no A+ (only A or A-), and no F+ or F- (only F).
        string sign = "";
        int lastDigit = Math.Abs(percentage % 10); // handle negative just in case

        if (letter == "A")
        {
            // No A+. Use A- only when percentage is 90–92, otherwise just A.
            if (percentage < 93)
            {
                sign = "-";
            }
            else
            {
                sign = "";
            }
        }
        else if (letter == "F")
        {
            // No F+ or F-
            sign = "";
        }
        else
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Print the final grade once (per the refactor requirement)
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // ---- Pass/Fail message (separate if): pass if percentage >= 70
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("You did not pass this time. Keep trying—you’ve got this!");
        }
    }
}
