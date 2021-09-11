using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;
using TinyCqrs.Classes;
using TinyCqrs.Interfaces;

namespace Servicing.Abstract
{
    public abstract class EntityFrameworkAlterModel
    {
        protected IProjectDbContext DbContext { get; }
        private bool SaveImmediately { get; }

        protected EntityFrameworkAlterModel(IProjectDbContext dbContext, bool saveImmediately = true)
        {
            DbContext = dbContext;
            SaveImmediately = saveImmediately;
        }

        protected async Task<ICmdResult> AddOrUpdate<TEntity>(DbSet<TEntity> dbSet, TEntity entity)
            where TEntity : class, IPrimaryKeyInteger
        {
            var cmdResult = new CmdResult($"Save {typeof(TEntity).Name}");

            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingEntity == null)
                {
                    await dbSet.AddAsync(entity);
                }
                else
                {
                    var mappedEntity = entity.Adapt(existingEntity);
                    dbSet.Update(mappedEntity);
                }
    
                if (SaveImmediately)
                    await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                cmdResult.AddIssue(ex.Message);
            }

            return cmdResult;
        }
    }
}