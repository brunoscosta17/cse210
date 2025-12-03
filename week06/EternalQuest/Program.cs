using System;

class Program
{
    static void Main(string[] args)
    {
        // ============================================================
        // Eternal Quest Program - CSE 210 - Week 06
        //
        // Extra / Creativity:
        // - Simple "Level" system: the player levels up every 1000 points.
        //   The current level is displayed with the score.
        // - Prevent recording events on completed simple/checklist goals
        //   to avoid getting points multiple times by mistake.
        // ============================================================

        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
