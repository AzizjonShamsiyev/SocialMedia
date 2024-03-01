namespace Domain.Entities;

public class Post
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public byte[]? Content { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
