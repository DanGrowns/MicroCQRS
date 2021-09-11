using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Authors
{
    /// <summary>
    /// A separate command has been required for this action, otherwise DI would not be able to distinguish
    /// between the handlers that deal with saving (Validate and Save), and Delete.
    /// </summary>
    public class DeleteAuthorCmd
    {
        public DeleteAuthorCmd(Author model) => Model = model;
        public Author Model { get; }
    }
    
    public class DeleteAuthor : ICmdHandlerAsync<DeleteAuthorCmd>
    {
        private IProjectDbContext DbContext { get; }
        private IEntityFrameworkModelDeleter ModelDeleter { get; }

        public DeleteAuthor(IProjectDbContext dbContext, IEntityFrameworkModelDeleter modelDeleter)
        {
            DbContext = dbContext;
            ModelDeleter = modelDeleter;
        }
        
        public async Task<ICmdResult> Execute(DeleteAuthorCmd cmd)
        {
            var cmdResult = await ModelDeleter.Delete(DbContext.Authors, cmd.Model, false);

            try
            {
                var connections = await DbContext.PostAuthors.Where(x => x.AuthorId == cmd.Model.Id).ToListAsync();
                
                DbContext.PostAuthors.RemoveRange(connections);
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