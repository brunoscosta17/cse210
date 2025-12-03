using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override void RecordEvent()
    {
        if (!IsComplete())
        {
            _amountCompleted++;
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string checkmark = IsComplete() ? "[X]" : "[ ]";
        return $"{checkmark} {_shortName} ({_description}) -- Completed {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        // ChecklistGoal:Name|Description|Points|Target|Bonus|AmountCompleted
        return $"ChecklistGoal:{_shortName}|{_description}|{_points}|{_target}|{_bonus}|{_amountCompleted}";
    }

    public override int GetPointsForEvent()
    {
        if (IsComplete() && _amountCompleted > _target)
        {
            // If already past the target, only give base points
            return _points;
        }

        // On the event that reaches the target, give bonus
        if (_amountCompleted == _target)
        {
            return _points + _bonus;
        }

        return _points;
    }
}
