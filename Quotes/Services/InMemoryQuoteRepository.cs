using Quotes.Controllers.Interfaces;

namespace Quotes.Services
{
    public class InMemoryQuoteRepository : IQuoteRepository
    {
        private readonly List<Quote> _quotes = new();
        private readonly object _lock = new();
        private int _nextId = 1;

        public Quote Add(string author, string text)
        {
            lock (_lock)
            {
                var quote = new Quote { Id = _nextId++, Author = author, Text = text };
                _quotes.Add(quote);
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
                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                return _quotes.RemoveAll(q => q.Id == id) > 0;
            }
        }
    }
}
