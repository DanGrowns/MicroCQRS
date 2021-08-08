using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Read;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Posts
{
    /// <summary>
    /// A separate command has been required for this action, otherwise DI would not be able to distinguish
    /// between the handlers that deal with saving (Validate and Save), and Delete.
    /// </summary>
    public class DeletePostCmd
    {
        public DeletePostCmd(PostOverview model) => Model = model;
        public PostOverview Model { get; }
    }
    
    public class DeletePost : ICmdHandlerAsync<DeletePostCmd>
    {
        private IProjectDbContext DbContext { get; }
        private IEntityFrameworkModelDeleter ModelDeleter { get; }

        public DeletePost(IProjectDbContext dbContext, IEntityFrameworkModelDeleter modelDeleter)
        {
            DbContext = dbContext;
            ModelDeleter = modelDeleter;
        }
        
        public async Task<ICmdResult> Execute(DeletePostCmd cmd)
        {
            var cmdResult = await ModelDeleter.Delete(DbContext.Posts, cmd.Model.Post, false);

            try
            {
                var authorConnections = await DbContext.PostAuthors.Where(x => x.PostId == cmd.Model.Post.Id).ToListAsync();
                
                DbContext.PostAuthors.RemoveRange(authorConnections);
                
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                cmdResult.AddError(ex.Message);
            }

            return cmdResult;
        }
    }
}