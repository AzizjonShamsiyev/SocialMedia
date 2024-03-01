using Application.Abstraction;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISocialMediaContext context;

        public CommentRepository(ISocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<Comment?> CreateAsync(Comment entity)
        {
            if (entity != null)
            {
                await context.Comments.AddAsync(entity);

                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }

        public async Task<IEnumerable<Comment>?> CreateRangeAsync(IEnumerable<Comment> entities)
        {
            if (entities != null)
            {
                await context.Comments.AddRangeAsync(entities);

                if (await context.SaveChangesAsync() > 0)
                    return entities;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Comment? Comment = await context.Comments.FindAsync(id);

            if (Comment != null)
            {
                context.Comments.Remove(Comment);

                if (await context.SaveChangesAsync() > 0)
                    return true;
            }

            return false;
        }

        public async Task<IQueryable<Comment>?> GetAllAsync() => context.Comments;

        public async Task<IQueryable<Comment>?> GetAsync(Expression<Func<Comment, bool>> predicate) =>
            context.Comments.Where(predicate);


        public async Task<Comment?> GetByIdAsync(Guid id) =>
            await context.Comments.AsNoTracking().FirstOrDefaultAsync(comment => comment.Id == id);

        public async Task<Comment?> UpdateAsync(Comment entity)
        {
            if (entity != null)
            {
                context.Comments.Update(entity);
                if (await context.SaveChangesAsync() > 0)
                    return entity;
            }

            return null;
        }
    }
}
