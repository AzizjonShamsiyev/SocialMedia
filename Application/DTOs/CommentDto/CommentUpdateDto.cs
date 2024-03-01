namespace Application.DTOs.CommentDto;

public class CommentUpdateDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
}
