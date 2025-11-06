using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Journal
{
    private readonly List<Entry> _entries = new List<Entry>();

    public int Count => _entries.Count;

    public void AddEntry(Entry entry) => _entries.Add(entry);

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        Console.WriteLine("=== All Entries ===");
        foreach (var e in _entries)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // Helper to display an arbitrary list (used by search)
    public static void DisplayList(List<Entry> list)
    {
        Console.WriteLine($"=== {list.Count} match(es) ===");
        foreach (var e in list)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // Save: detect by extension → .json uses JSON; otherwise pipe format
    public void SaveToFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException("Filename cannot be empty.");

        string ext = Path.GetExtension(filename).ToLowerInvariant();
        if (ext == ".json")
        {
            SaveAsJson(filename);
        }
        else
        {
            SaveAsPipe(filename);
        }
    }

    public void LoadFromFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException("Filename cannot be empty.");
        if (!File.Exists(filename))
            throw new FileNotFoundException("File not found.", filename);

        string ext = Path.GetExtension(filename).ToLowerInvariant();
        if (ext == ".json")
        {
            LoadFromJson(filename);
        }
        else
        {
            LoadFromPipe(filename);
        }
    }

    private void SaveAsPipe(string filename)
    {
        using var writer = new StreamWriter(filename);
        foreach (var e in _entries)
        {
            writer.WriteLine(e.ToPipeLine());
        }
    }

    private void LoadFromPipe(string filename)
    {
        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var entry = Entry.FromPipeLine(line);
            _entries.Add(entry);
        }
    }

    private void SaveAsJson(string filename)
    {
        // Serialize the list directly (includes mood & tags)
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(filename, json);
    }

    private void LoadFromJson(string filename)
    {
        _entries.Clear();
        string json = File.ReadAllText(filename);
        var loaded = JsonSerializer.Deserialize<List<Entry>>(json);
        if (loaded != null) _entries.AddRange(loaded);
    }

    // Extra: basic filters
    public List<Entry> FindByDateSubstring(string query)
    {
        query = (query ?? "").Trim();
        var results = new List<Entry>();
        if (string.IsNullOrEmpty(query)) return results;

        foreach (var e in _entries)
        {
            if (e.DateText.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(e);
            }
        }
        return results;
    }

    public List<Entry> FindByTag(string tag)
    {
        tag = (tag ?? "").Trim();
        var results = new List<Entry>();
        if (string.IsNullOrEmpty(tag)) return results;

        foreach (var e in _entries)
        {
            foreach (var t in e.Tags)
            {
                if (string.Equals(t, tag, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(e);
                    break;
                }
            }
        }
        return results;
    }
}
