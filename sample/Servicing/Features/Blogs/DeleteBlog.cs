using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Blogs
{
    /// <summary>
    /// A separate command has been required for this action, otherwise DI would not be able to distinguish
    /// between the handlers that deal with saving (Validate and Save), and Delete.
    /// </summary>
    public class DeleteBlogCmd
    {
        public DeleteBlogCmd(Blog model) => Model = model;
        public Blog Model { get; }
    }
    
    public class DeleteBlog : ICmdHandlerAsync<DeleteBlogCmd>
    {
        private IProjectDbContext DbContext { get; }
        private IEntityFrameworkModelDeleter ModelDeleter { get; }

        public DeleteBlog(IProjectDbContext dbContext, IEntityFrameworkModelDeleter modelDeleter)
        {
            DbContext = dbContext;
            ModelDeleter = modelDeleter;
        }
        
        public async Task<ICmdResult> Execute(DeleteBlogCmd cmd)
        {
            var cmdResult = await ModelDeleter.Delete(DbContext.Blogs, cmd.Model, false);

            try
            {
                var postConnections = 
                    await DbContext.Posts
                        .Where(x => x.BlogId == cmd.Model.Id)
                        .ToListAsync();
                
                var postIds = postConnections.Select(x => x.Id).ToList();
                
                var authorConnections = 
                    await DbContext.PostAuthors
                        .Where(x => postIds.Contains(x.PostId))
                        .ToListAsync();
                
                DbContext.Posts.RemoveRange(postConnections);
                DbContext.PostAuthors.RemoveRange(authorConnections);
                
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