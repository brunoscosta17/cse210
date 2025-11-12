using System;
using System.Collections.Generic;

class Program
{

    static void Main()
    {
        var scriptures = new List<(Reference reference, string text)>
        {
            (
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."
            ),
            (
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him " +
                "should not perish, but have everlasting life."
            )
        };

        int current = 0;
        RunMemorizer(scriptures, ref current);
    }

    private static void RunMemorizer(List<(Reference reference, string text)> scriptures, ref int currentIndex)
    {
        Scripture scripture = new Scripture(scriptures[currentIndex].reference, scriptures[currentIndex].text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press <enter> to hide words, 'n' for next scripture, or type 'quit' to end: ");

            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var trimmed = input.Trim().ToLowerInvariant();
                if (trimmed == "quit") return;

                if (trimmed == "n")
                {
                    currentIndex = (currentIndex + 1) % scriptures.Count;
                    scripture = new Scripture(scriptures[currentIndex].reference, scriptures[currentIndex].text);
                    continue;
                }
            }

            scripture.HideRandomWords(numberToHide: 3);

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden. Great job! 👏");
                return;
            }
        }
    }
}
