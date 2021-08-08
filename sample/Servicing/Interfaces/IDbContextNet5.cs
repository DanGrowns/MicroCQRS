using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Servicing.Interfaces
{
    public interface IDbContextNet5
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        
        EntityEntry Add(object entity);
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken);
        
        EntityEntry Update(object entity);

        void AddRange(params object[] entities);
        void AddRange(IEnumerable<object> entities);
        Task AddRangeAsync(params object[] entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken);

        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities);
        
        void RemoveRange(params object[] entities);
        void RemoveRange(IEnumerable<object> entities);

        object Find(Type entityType, params object[] keyValues);
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);
        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
    }
}