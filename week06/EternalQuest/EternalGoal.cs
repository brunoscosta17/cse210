using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // Eternal goals never complete, so no state change needed.
    }

    public override bool IsComplete() => false;

    public override string GetDetailsString()
    {
        return $"[ ] {_shortName} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        // EternalGoal:Name|Description|Points
        return $"EternalGoal:{_shortName}|{_description}|{_points}";
    }

    public override int GetPointsForEvent()
    {
        // Always awards points on each event.
        return _points;
    }
}
