using System.ComponentModel.DataAnnotations;

namespace X8WebAPI._dtos.Stock;

public class UpsertStockDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length is 5 characters")]
    public string Symbol { get; init; } = string.Empty;
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length is 5 characters")]
    public string CompanyName { get; init; } = string.Empty;
    [Required]
    [Range(1, 100000000)]
    public decimal Purchase { get; init; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; init; }
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length is 5 characters")]
    public string Industry { get; init; } = string.Empty;
    [Range(1, 50000000000)]
    public long MarketCap { get; init; }
}