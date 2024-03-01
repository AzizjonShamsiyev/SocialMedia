namespace Application.DTOs.PostDto;

public class PostUpdateDto
{
    public Guid Id { get; set; }
    public byte[]? Content { get; set; }
    public string Text { get; set; } = string.Empty;
}
