using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public GenderType Gender { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<User>? Followers { get; set; }
    public ICollection<User>? Followed { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
