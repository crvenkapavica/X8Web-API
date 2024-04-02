namespace X8WebAPI._dtos.Comment;

public class UpsertCommentDto
{
    public string Title { get; init; } = string.Empty;
    
    public string Content { get; init; } = string.Empty;
    
    public DateTime CreatedOn { get; init; } = DateTime.Now;
    
    public int? StockId { get; init; }
}