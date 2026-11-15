using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Altairis.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        
        Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>?> GetAllOrderedByAsync(
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            CancellationToken cancellationToken = default
        );

        Task<IReadOnlyList<T>?> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>?> GetByConditionOrderedByAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsBy(Expression<Func<T, bool>> predicates, CancellationToken cancellationToken);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        
        void Update(T entity);
        
        void Delete(T entity);
    }
}
