using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction;

public interface ISocialMediaContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
