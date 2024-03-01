namespace Application.DTOs.CommentDto;

public class CommentCreateDto
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
}
