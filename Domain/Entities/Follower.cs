namespace Domain.Entities;

public class Follower
{
    public Guid Id { get; set; }
    public Guid FollowerUserId { get; set; }
    public Guid FollowedUserId { get; set; }
}
