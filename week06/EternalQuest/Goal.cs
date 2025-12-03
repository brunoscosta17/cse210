using System;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    protected Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    public string GetShortName() => _shortName;

    /// <summary>
    /// Called when the user records progress on this goal.
    /// The goal updates its internal state here.
    /// </summary>
    public abstract void RecordEvent();

    /// <summary>
    /// Returns true if this goal is fully completed.
    /// For Eternal goals this will always be false.
    /// </summary>
    public abstract bool IsComplete();

    /// <summary>
    /// Text used when listing goals to the user, with [ ] or [X].
    /// </summary>
    public abstract string GetDetailsString();

    /// <summary>
    /// Text used to save the goal to a file.
    /// Example: SimpleGoal:Run 5k|Run a 5k race|1000|true
    /// </summary>
    public abstract string GetStringRepresentation();

    /// <summary>
    /// Returns how many points the user earns in THIS event.
    /// Polymorphic: checklist goal pode devolver bonus, eternal etc.
    /// </summary>
    public virtual int GetPointsForEvent()
    {
        return _points;
    }
}
