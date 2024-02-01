using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Repositories
{
    public class NameRepository<T> : INameRepository<T> where T : BaseNameEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _context;

        public NameRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAllBySearch(string? search, int take, bool IsTracking = true, params string[] includes)
        {
            var query = _dbSet.AsQueryable();

            query = _addIncludes(query, includes);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            return IsTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllWhereByOrderBySearch(string? search, Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderException = null, bool IsDescending = false, int skip = 0, int take = 0, bool IsTracking = true, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (expression != null) query = query.Where(expression);

            if (orderException != null)
            {
                if (IsDescending) query = query.OrderByDescending(orderException);
                else query = query.OrderBy(orderException);
            }

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = _addIncludes(query, includes);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            return IsTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllWhereBySearch(string? search, Expression<Func<T, bool>>? expression = null, int skip = 0, int take = 0, bool IsTracking = true, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            if (expression != null) query = query.Where(expression);

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = _addIncludes(query, includes);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            return IsTracking ? query : query.AsNoTracking();
        }

        private IQueryable<T> _addIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

        private IQueryable<T> _addIncludesGetById(IQueryable<T> query, int take, int skip, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]).Skip((skip - 1)).Take(take);
                }
            }
            return query;
        }
    }
}
