using Application.Abstraction;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ISocialMediaContext context;

        public UserRepository(ISocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<User?> CreateAsync(User entity)
        {
            if (entity != null)
            {
                await context.Users.AddAsync(entity);                
                 
                if(await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }

        public async Task<IEnumerable<User>?> CreateRangeAsync(IEnumerable<User> entities)
        {
            if (entities != null)
            {
                await context.Users.AddRangeAsync(entities);

                if (await context.SaveChangesAsync() > 0)
                    return entities;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            User? user = await context.Users.FindAsync(id);

            if (user != null)
            {
                context.Users.Remove(user);

                if (await context.SaveChangesAsync() > 0)
                    return true;
            }

            return false;
        }

        public async Task<IQueryable<User>?> GetAllAsync() => context.Users
            .Include(u => u.Followers)
            .Include(u => u.Followed)
            .Include(u => u.Posts)
            .Include(u => u.Comments);

        public async Task<IQueryable<User>?> GetAsync(Expression<Func<User, bool>> predicate) => 
            context.Users.Where(predicate)
            .Include(u => u.Followers)
            .Include(u => u.Followed)
            .Include(u => u.Posts)
            .Include(u => u.Comments);        

        public async Task<User?> GetByIdAsync(Guid id) =>
             await context.Users
            .Include(u => u.Followers)
            .Include(u => u.Followed)
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> UpdateAsync(User entity)
        {
            if(entity != null)
            {
                context.Users.Update(entity);
               if(await context.SaveChangesAsync()>0)
                    return entity;
            }

            return null;
        }
    }
}
