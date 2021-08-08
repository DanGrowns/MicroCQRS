using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace Servicing.Abstract
{
    // Has no need to be a CmdHandler as it doesn't need to be chained.
    // but it should still have singular responsibility.
    public class EntityFrameworkModelDeleter : IEntityFrameworkModelDeleter
    {
        public EntityFrameworkModelDeleter(IProjectDbContext dbContext)
            => DbContext = dbContext;
        
        public IProjectDbContext DbContext { get; }
        
        public async Task<ICmdResult> Delete<TEntity>(DbSet<TEntity> dbSet, TEntity entity, bool saveImmediately = true)
            where TEntity : class, IPrimaryKeyInteger
        {
            var cmdResult = new CmdResult($"Delete {typeof(TEntity).Name}");

            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingEntity == null)
                {
                    cmdResult.AddError("The entity you are trying to delete does not exist.");
                }
                else
                {
                    dbSet.Remove(existingEntity);
                    
                    if (saveImmediately)
                        await DbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                cmdResult.AddError(ex.Message);
            }

            return cmdResult;
        }
    }
}