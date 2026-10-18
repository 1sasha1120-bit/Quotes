namespace Quotes.DTOs;

using System.ComponentModel.DataAnnotations;

public class QuoteRequest
{
    [Required(AllowEmptyStrings = false)]
    [StringLength(200)]
    public string Author { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(1000)]
    public string Text { get; set; } = string.Empty;
}
