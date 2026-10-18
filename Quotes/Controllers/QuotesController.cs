using Microsoft.AspNetCore.Mvc;
using Quotes.Controllers.Interfaces;
using Quotes.DTOs;

[ApiController]
[Route("quotes")]
public class QuotesController : ControllerBase
{
    private readonly IQuoteRepository _repository;

    public QuotesController(IQuoteRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create(QuoteRequest request)
    {
        var quote = _repository.Add(request.Author, request.Text);
        return CreatedAtAction(nameof(GetById), new { id = quote.Id }, new { id = quote.Id });
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var quote = _repository.Get(id);
        return quote is null ? NotFound() : Ok(quote);
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] QuoteListQuery query)
    {
        var (items, total) = _repository.GetPage(query.Author, query.Page, query.PageSize);

        return Ok(new
        {
            items,
            page = query.Page,
            pageSize = query.PageSize,
            total
        });
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, QuoteRequest request)
    {
        var updated = _repository.Update(id, request.Author, request.Text);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}