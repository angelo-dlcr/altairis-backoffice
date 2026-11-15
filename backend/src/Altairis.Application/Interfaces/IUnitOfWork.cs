using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
