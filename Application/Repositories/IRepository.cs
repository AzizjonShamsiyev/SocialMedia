using System.Linq.Expressions;

namespace Application.Repositories;

public interface IRepository<T>
{
    Task<IQueryable<T>> GetAllAsync();
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
