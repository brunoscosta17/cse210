using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private readonly Random _rng = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(token => new Word(token))
            .ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        if (numberToHide <= 0) return;

        var visibleIndexes = _words
            .Select((w, i) => (w, i))
            .Where(t => !t.w.IsHidden())
            .Select(t => t.i)
            .ToList();

        if (visibleIndexes.Count == 0) return;

        for (int n = 0; n < numberToHide && visibleIndexes.Count > 0; n++)
        {
            int pick = _rng.Next(visibleIndexes.Count);
            int idx = visibleIndexes[pick];
            _words[idx].Hide();
            visibleIndexes.RemoveAt(pick);
        }
    }

    public string GetDisplayText()
    {
        var joined = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n\n{joined}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}
