using X8WebAPI._dtos.Comment;
using X8WebAPI._models;

namespace X8WebAPI.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCommentFromDto(this UpsertCommentDto commentModel)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
        };
    }
}