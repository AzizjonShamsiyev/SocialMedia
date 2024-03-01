using Domain.Entities;

namespace Application.DTOs.PostDto;

public class PostCreateDto
{
    public Guid UserId { get; set; }
    public byte[]? Content { get; set; }
    public string Text { get; set; } = string.Empty;
}
