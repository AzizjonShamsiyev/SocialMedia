namespace Application.DTOs.CommentDto;

public class CommentGetDto
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
}
