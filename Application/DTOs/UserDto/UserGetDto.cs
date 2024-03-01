using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.UserDto;

public class UserGetDto
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
    public Guid[]? FollowersId { get; set; }
    public Guid[]? FollowedId { get; set; }
    public Guid[]? PostsId { get; set; }
    public Guid[]? CommentsId { get; set; }
}
