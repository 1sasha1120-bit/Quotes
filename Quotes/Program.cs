using Quotes.Controllers.Interfaces;
using Quotes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var quotesPath = builder.Configuration["QuotesFilePath"]
    ?? Path.Combine(builder.Environment.ContentRootPath, "Data", "quotes.json");

builder.Services.AddSingleton<IQuoteRepository>(_ => new FileQuoteRepository(quotesPath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"))
      .ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
