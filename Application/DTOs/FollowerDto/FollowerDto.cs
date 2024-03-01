namespace Application.DTOs.FollowerDto;

public class FollowerDto
{
    public Guid Id { get; set; }
    public Guid FollowerUserId { get; set; }
    public Guid FollowedUserId { get; set; }
}
