using Application.Abstraction;
using Application.Repositories;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ISocialMediaContext context;

        public FollowerRepository(ISocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<Follower?> CreateAsync(Follower entity)
        {
            if (entity != null)
            {
                await context.Followers.AddAsync(entity);

                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }

        public async Task<IEnumerable<Follower>?> CreateRangeAsync(IEnumerable<Follower> entities)
        {
            if (entities != null)
            {
                await context.Followers.AddRangeAsync(entities);

                if (await context.SaveChangesAsync() > 0)
                    return entities;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Follower? Follower = await context.Followers.FindAsync(id);

            if (Follower != null)
            {
                context.Followers.Remove(Follower);

                if (await context.SaveChangesAsync() > 0)
                    return true;
            }

            return false;
        }

        public async Task<IQueryable<Follower>?> GetAllAsync() => context.Followers;

        public async Task<IQueryable<Follower>?> GetAsync(Expression<Func<Follower, bool>> predicate) =>
            context.Followers.Where(predicate);


        public async Task<Follower?> GetByIdAsync(Guid id) =>
            await context.Followers.FindAsync(id);

        public async Task<Follower?> UpdateAsync(Follower entity)
        {
            if (entity != null)
            {
                context.Followers.Update(entity);
                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }
    }
}
