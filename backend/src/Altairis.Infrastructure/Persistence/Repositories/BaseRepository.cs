using Altairis.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Altairis.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AltairisDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AltairisDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().IgnoreAutoIncludes().ToListAsync(cancellationToken);


        public async Task<IReadOnlyList<T>?> GetAllOrderedByAsync(
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = _dbSet.AsNoTracking().IgnoreAutoIncludes().AsQueryable();

            if (orderBy is not null)
            {
                query = !isDescending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>?> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsNoTracking().IgnoreAutoIncludes().AsQueryable();

            query = query.Where(predicate);
            
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>?> GetByConditionOrderedByAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = _dbSet.AsNoTracking().IgnoreAutoIncludes().AsQueryable();

            query = query.Where(predicate);

            if (orderBy is not null)
            {
                query = !isDescending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(id, cancellationToken);

        public async Task<bool> ExistsBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}
