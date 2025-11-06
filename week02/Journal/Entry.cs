using System.Collections.Generic;
using System.Text;

public class Entry
{

    private string _dateText;
    private string _prompt;
    private string _response;
    private int? _mood;
    private List<string> _tags;

    public Entry(string dateText, string prompt, string response, int? mood = null, List<string>? tags = null)
    {
        _dateText = dateText;
        _prompt = prompt;
        _response = response;
        _mood = mood;
        _tags = tags ?? new List<string>();
    }

    public string DateText => _dateText;
    public string Prompt => _prompt;
    public string Response => _response;
    public int? Mood => _mood;
    public List<string> Tags => _tags;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Date:    {DateText}");
        sb.AppendLine($"Prompt:  {Prompt}");
        sb.AppendLine($"Entry:   {Response}");
        if (Mood.HasValue) sb.AppendLine($"Mood:    {Mood.Value}/5");
        if (Tags.Count > 0) sb.AppendLine($"Tags:    {string.Join(", ", Tags)}");
        return sb.ToString();
    }

    public string ToPipeLine()
    {
        string moodText = Mood.HasValue ? Mood.Value.ToString() : "";
        string tagsText = Tags.Count > 0 ? string.Join(',', Tags) : "";

        return $"{DateText}|{Prompt}|{Response}|{moodText}|{tagsText}";
    }

    public static Entry FromPipeLine(string line)
    {

        var parts = line.Split('|');
        string date = parts.Length > 0 ? parts[0] : "";
        string prompt = parts.Length > 1 ? parts[1] : "";
        string response = parts.Length > 2 ? parts[2] : "";
        int? mood = null;
        if (parts.Length > 3 && int.TryParse(parts[3], out int moodVal))
        {
            mood = moodVal;
        }

        var tags = new List<string>();
        if (parts.Length > 4 && !string.IsNullOrWhiteSpace(parts[4]))
        {
            tags = new List<string>(parts[4].Split(',', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries));
        }

        return new Entry(date, prompt, response, mood, tags);
    }
}
