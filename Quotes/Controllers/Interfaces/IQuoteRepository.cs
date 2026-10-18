namespace Quotes.Controllers.Interfaces;

public interface IQuoteRepository
{
    Quote Add(string author, string text);
    Quote? Get(int id);
    (IReadOnlyList<Quote> Items, int Total) GetPage(string? author, int page, int pageSize);
    bool Update(int id, string author, string text);
    bool Delete(int id);
}
