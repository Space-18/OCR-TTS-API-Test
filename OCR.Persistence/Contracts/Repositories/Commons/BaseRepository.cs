using Microsoft.EntityFrameworkCore;
using OCR.Application.Contracts.Persistence.Commons;
using OCR.Domain.Entities.Commons;
using System.Linq.Expressions;

namespace OCR.Persistence.Contracts.Repositories.Commons
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : AuditoryBaseModel
    {
        public readonly ApplicationDBContext _context;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(x => x.IsActive).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(x => x.IsActive).Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => x.IsActive);

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => x.IsActive);

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(x => x.IsActive).FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAllByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(x => x.IsActive).Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteAsync(T entity)
        {
            entity.IsActive = false;
            _context.Set<T>().Update(entity);
        }
    }
}
