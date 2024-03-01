using Application.Abstraction;
using Application.Repositories;
using Infrastructure.DataAccess;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ISocialMediaContext, SocialMediaContext>(option =>
            option.UseNpgsql(configuration.GetConnectionString("ConnectionSocialMedia")));

        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IFollowerRepository,FollowerRepository>();
        services.AddScoped<IPostRepository,PostRepository>();
        services.AddScoped<ICommentRepository,CommentRepository>();

        return services;
    }
}
