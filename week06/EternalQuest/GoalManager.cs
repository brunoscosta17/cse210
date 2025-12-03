using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    // Level is derived from score: every 1000 points is a new level
    // Level 1: 0-999, Level 2: 1000-1999, etc.

    private int Level => (_score / 1000) + 1;

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    Pause();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Pause();
                    break;
            }
        }
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level: {Level}");
    }

    private void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }

        if (_goals.Count == 0)
        {
            Console.WriteLine("  (No goals yet)");
        }
    }

    private void ListGoalDetails()
    {
        Console.WriteLine("Your goals:");
        Console.WriteLine();

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        if (_goals.Count == 0)
        {
            Console.WriteLine("  (No goals yet)");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string typeChoice = Console.ReadLine();
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine() ?? "0");

        Goal goal = null;

        switch (typeChoice)
        {
            case "1": // Simple
                goal = new SimpleGoal(name, description, points);
                break;

            case "2": // Eternal
                goal = new EternalGoal(name, description, points);
                break;

            case "3": // Checklist
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine() ?? "0");

                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;

            default:
                Console.WriteLine("Invalid goal type.");
                Pause();
                return;
        }

        _goals.Add(goal);
        Console.WriteLine("Goal created!");
        Pause();
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet.");
            Pause();
            return;
        }

        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid selection.");
            Pause();
            return;
        }

        int index = choice - 1;
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            Pause();
            return;
        }

        Goal goal = _goals[index];

        if (goal.IsComplete())
        {
            Console.WriteLine("This goal is already complete.");
            Pause();
            return;
        }

        goal.RecordEvent();
        int points = goal.GetPointsForEvent();
        _score += points;

        Console.WriteLine($"Congratulations! You have earned {points} points!");
        Console.WriteLine($"You now have {_score} points.");
        Pause();
    }

    private void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // First line: score
            outputFile.WriteLine(_score);

            // Then each goal
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved.");
        Pause();
    }

    private void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            Pause();
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        _goals.Clear();

        if (lines.Length == 0)
        {
            _score = 0;
            return;
        }

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(':');
            string type = parts[0];
            string[] data = parts[1].Split('|');

            switch (type)
            {
                case "SimpleGoal":
                    {
                        string name = data[0];
                        string description = data[1];
                        int points = int.Parse(data[2]);
                        bool isComplete = bool.Parse(data[3]);
                        _goals.Add(new SimpleGoal(name, description, points, isComplete));
                        break;
                    }
                case "EternalGoal":
                    {
                        string name = data[0];
                        string description = data[1];
                        int points = int.Parse(data[2]);
                        _goals.Add(new EternalGoal(name, description, points));
                        break;
                    }
                case "ChecklistGoal":
                    {
                        string name = data[0];
                        string description = data[1];
                        int points = int.Parse(data[2]);
                        int target = int.Parse(data[3]);
                        int bonus = int.Parse(data[4]);
                        int amountCompleted = int.Parse(data[5]);
                        _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                        break;
                    }
            }
        }

        Console.WriteLine("Goals loaded.");
        Pause();
    }

    private void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }
}
