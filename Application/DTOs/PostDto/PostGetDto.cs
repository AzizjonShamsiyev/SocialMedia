using Domain.Entities;

namespace Application.DTOs.PostDto;

public class PostGetDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public byte[]? Content { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public Guid[]? CommentsId { get; set; }
}
