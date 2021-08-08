using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Read;
using Servicing.Abstract;
using Servicing.Interfaces;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Posts
{
    [CqrsDecoratedBy(typeof(ValidatePost))]
    public class SavePost : EntityFrameworkAlterModel, ICmdHandlerAsync<PostDisplay>
    {
        public SavePost(IProjectDbContext dbContext) : base(dbContext, saveImmediately: false) { }

        public async Task<ICmdResult> Execute(PostDisplay cmd)
        {
            cmd.Post.BlogId = cmd.Blog.Id;
            
            var cmdResult = await AddOrUpdate(DbContext.Posts, cmd.Post);
            await DbContext.SaveChangesAsync();
            
            try
            {
                if (cmd.Post.Id > 0)
                {
                    var currentAuthors = 
                        DbContext.PostAuthors
                            .Where(x => x.PostId == cmd.Post.Id)
                            .ToList();

                    var newAuthors = 
                        cmd.PostAuthors
                            .Select(x => new PostAuthor { AuthorId = x.Id, PostId = cmd.Post.Id })
                            .ToList();

                    DbContext.PostAuthors.RemoveRange(currentAuthors);
                    DbContext.PostAuthors.AddRange(newAuthors);
                }
            }
            catch (Exception ex)
            {
                cmdResult.AddError(ex.Message);
            }

            await DbContext.SaveChangesAsync();
            return cmdResult;
        }
    }
}