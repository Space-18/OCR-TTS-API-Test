using OCR.Domain.Entities.Commons;
using System.Linq.Expressions;

namespace OCR.Application.Contracts.Persistence.Commons
{
    public interface IBaseRepository<T> where T : AuditoryBaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? include = null,
            bool disableTracking = true);
        Task<T?> GetByIdAsync(string id);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllByAsync(Expression<Func<T, bool>> predicate);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
