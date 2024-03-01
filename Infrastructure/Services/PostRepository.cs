using Application.Abstraction;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    internal class PostRepository : IPostRepository
    {
        private readonly ISocialMediaContext context;

        public PostRepository(ISocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<Post?> CreateAsync(Post entity)
        {
            if (entity != null)
            {
                await context.Posts.AddAsync(entity);

                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }

        public async Task<IEnumerable<Post>?> CreateRangeAsync(IEnumerable<Post> entities)
        {
            if (entities != null)
            {
                await context.Posts.AddRangeAsync(entities);

                if (await context.SaveChangesAsync() > 0)
                    return entities;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Post? Post = await context.Posts.FindAsync(id);

            if (Post != null)
            {
                context.Posts.Remove(Post);

                if (await context.SaveChangesAsync() > 0)
                    return true;
            }

            return false;
        }

        public async Task<IQueryable<Post>?> GetAllAsync() => context.Posts;

        public async Task<IQueryable<Post>?> GetAsync(Expression<Func<Post, bool>> predicate) =>
            context.Posts.Where(predicate);


        public async Task<Post?> GetByIdAsync(Guid id) =>
            await context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Post?> UpdateAsync(Post entity)
        {
            if (entity != null)
            {
                context.Posts.Update(entity);
                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }
    }
}
