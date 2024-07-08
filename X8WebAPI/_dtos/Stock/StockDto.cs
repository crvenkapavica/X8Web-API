using X8WebAPI._dtos.Comment;

namespace X8WebAPI._dtos.Stock;

public class StockDto
{
    public int Id { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string CompanyName { get; init; } = string.Empty;
    public decimal Purchase { get; init; }
    public decimal LastDiv { get; init; }
    public string Industry { get; init; } = string.Empty;
    public long MarketCap { get; init; }

    public List<CommentDto> Comments { get; set; } = new();
}