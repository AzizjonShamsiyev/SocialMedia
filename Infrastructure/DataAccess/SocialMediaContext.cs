using Application.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class SocialMediaContext : DbContext, ISocialMediaContext
{

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        :base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get ; set ; }
    public DbSet<Post> Posts { get ; set ; }
    public DbSet<Follower> Followers { get ; set ; }
    public DbSet<Comment> Comments { get ; set ; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(x => {
            x.HasMany(f => f.Followers)
            .WithMany(f => f.Followed)
            .UsingEntity<Follower>(
                l => l.HasOne<User>().WithMany().HasForeignKey(e => e.FollowerUserId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.FollowedUserId));

            x.HasIndex(u => new { u.Email, u.Phone })
            .IsUnique(true);
        });        
    }
}
