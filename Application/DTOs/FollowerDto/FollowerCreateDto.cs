namespace Application.DTOs.FollowerDto;

public class FollowerCreateDto
{
    public Guid FollowerUserId { get; set; }
    public Guid FollowedUserId { get; set; }
}
