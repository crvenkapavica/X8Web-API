using System.ComponentModel.DataAnnotations;

namespace X8WebAPI._dtos.Comment;

public class UpsertCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
    [MaxLength(120, ErrorMessage = "Maximum length is 120 characters")]
    public string Title { get; init; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
    [MaxLength(120, ErrorMessage = "Maximum length is 120 characters")]
    public string Content { get; init; } = string.Empty;
}