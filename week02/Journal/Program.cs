namespace JournalApp;

using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Journal App (W02) ===");
        var journal = new Journal();
        var promptGenerator = new PromptGenerator();

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1) Write a new entry");
            Console.WriteLine("2) Display the journal");
            Console.WriteLine("3) Save the journal to a file");
            Console.WriteLine("4) Load the journal from a file");
            Console.WriteLine("5) Search / Filter (extra)");
            Console.WriteLine("6) Quit");
            Console.Write("Choose an option (1-6): ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    WriteEntry(journal, promptGenerator);
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.txt or journal.json): ");
                    {
                        string filename = (Console.ReadLine() ?? "").Trim();
                        try
                        {
                            journal.SaveToFile(filename);
                            Console.WriteLine($"Saved to '{filename}'.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error saving: {ex.Message}");
                        }
                    }
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.txt or journal.json): ");
                    {
                        string filename = (Console.ReadLine() ?? "").Trim();
                        try
                        {
                            journal.LoadFromFile(filename);
                            Console.WriteLine($"Loaded from '{filename}'.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading: {ex.Message}");
                        }
                    }
                    break;

                case "5":
                    SearchOrFilter(journal);
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please enter a number from 1 to 6.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    private static void WriteEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine() ?? "";

        string dateText = DateTime.Now.ToShortDateString();

        int? mood = null;
        Console.Write("Mood (1–5, optional – press Enter to skip): ");
        string moodText = Console.ReadLine()?.Trim() ?? "";
        if (int.TryParse(moodText, out int moodValue) && moodValue >= 1 && moodValue <= 5)
        {
            mood = moodValue;
        }

        Console.Write("Tags (comma-separated, optional – press Enter to skip): ");
        string tagsText = Console.ReadLine() ?? "";
        var tags = string.IsNullOrWhiteSpace(tagsText)
            ? new System.Collections.Generic.List<string>()
            : new System.Collections.Generic.List<string>(tagsText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

        var entry = new Entry(dateText, prompt, response, mood, tags);
        journal.AddEntry(entry);

        Console.WriteLine("Entry added.");
    }

    private static void SearchOrFilter(Journal journal)
    {
        if (journal.Count == 0)
        {
            Console.WriteLine("No entries to search.");
            return;
        }

        Console.WriteLine("Search / Filter:");
        Console.WriteLine("1) By date (substring match, e.g., '3/21/2025' or '2025')");
        Console.WriteLine("2) By tag");
        Console.WriteLine("3) Back");
        Console.Write("Choose: ");
        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
            case "1":
                Console.Write("Enter date text to match: ");
                {
                    string query = (Console.ReadLine() ?? "").Trim();
                    var matches = journal.FindByDateSubstring(query);
                    if (matches.Count == 0) Console.WriteLine("No matches.");
                    else Journal.DisplayList(matches);
                }
                break;

            case "2":
                Console.Write("Enter tag to match: ");
                {
                    string tag = (Console.ReadLine() ?? "").Trim();
                    var matches = journal.FindByTag(tag);
                    if (matches.Count == 0) Console.WriteLine("No matches.");
                    else Journal.DisplayList(matches);
                }
                break;

            default:
                // back or invalid → do nothing
                break;
        }
    }
}
