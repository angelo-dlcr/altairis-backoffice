using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Altairis.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        
        Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>?> GetAllOrderedByAsync(
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            bool ignoreAutoIncludes = true,
            CancellationToken cancellationToken = default
        );

        Task<IReadOnlyList<T>?> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>?> GetByConditionOrderedByAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            bool ignoreAutoIncludes = true,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByAsync(Expression<Func<T, bool>> predicates, CancellationToken cancellationToken);

        Task<IReadOnlyList<object>?> GetAllDistinctByAsync(Expression<Func<T, object>> keySelector, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);
        
        bool Update(T entity);
        
        void Delete(T entity);
    }
}
