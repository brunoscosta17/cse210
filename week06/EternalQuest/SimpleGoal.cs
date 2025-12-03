using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString()
    {
        string checkmark = _isComplete ? "[X]" : "[ ]";
        return $"{checkmark} {_shortName} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_shortName}|{_description}|{_points}|{_isComplete}";
    }

    public override int GetPointsForEvent()
    {
        return _isComplete ? 0 : _points;
    }
}
