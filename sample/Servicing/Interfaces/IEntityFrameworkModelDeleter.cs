using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TinyCqrs.Interfaces;

namespace Servicing.Interfaces
{
    public interface IEntityFrameworkModelDeleter
    {
        Task<ICmdResult> Delete<TEntity>(DbSet<TEntity> dbSet, TEntity entity, bool saveImmediately = true)
            where TEntity : class, IPrimaryKeyInteger;
    }
}