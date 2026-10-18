using Quotes;
using Quotes.Controllers.Interfaces;
using System.Text.Json;

public class FileQuoteRepository : IQuoteRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly string _filePath;
    private readonly List<Quote> _quotes;
    private readonly object _lock = new();
    private int _nextId;

    public FileQuoteRepository(string filePath)
    {
        _filePath = Path.GetFullPath(filePath);
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        _quotes = Load();
        _nextId = _quotes.Count == 0 ? 1 : _quotes.Max(q => q.Id) + 1;
    }

    private List<Quote> Load()
    {
        if (!File.Exists(_filePath)) return new List<Quote>();

        var json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json)) return new List<Quote>();

        return JsonSerializer.Deserialize<List<Quote>>(json) ?? new List<Quote>();
    }

    private void Save()
    {
        var tempPath = _filePath + ".tmp";
        File.WriteAllText(tempPath, JsonSerializer.Serialize(_quotes, JsonOptions));
        File.Move(tempPath, _filePath, overwrite: true);
    }

    public Quote Add(string author, string text)
    {
        lock (_lock)
        {
            var quote = new Quote { Id = _nextId++, Author = author, Text = text };
            _quotes.Add(quote);
            Save();
            return quote;
        }
    }

    public Quote? Get(int id)
    {
        lock (_lock)
        {
            return _quotes.FirstOrDefault(q => q.Id == id);
        }
    }

    public (IReadOnlyList<Quote> Items, int Total) GetPage(string? author, int page, int pageSize)
    {
        lock (_lock)
        {
            var query = _quotes.AsEnumerable();

            if (!string.IsNullOrEmpty(author))
                query = query.Where(q => q.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

            var filtered = query.ToList();
            var items = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return (items, filtered.Count);
        }
    }

    public bool Update(int id, string author, string text)
    {
        lock (_lock)
        {
            var quote = _quotes.FirstOrDefault(q => q.Id == id);
            if (quote is null) return false;

            quote.Author = author;
            quote.Text = text;
            Save();
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var removed = _quotes.RemoveAll(q => q.Id == id) > 0;
            if (removed) Save();
            return removed;
        }
    }
}