using System.Threading.Tasks;
using Domain.Models;
using Servicing.Abstract;
using Servicing.Interfaces;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Authors
{
    [CqrsDecoratedBy(typeof(ValidateAuthor))]
    public class SaveAuthor : EntityFrameworkAlterModel, ICmdHandlerAsync<Author>
    {
        public SaveAuthor(IProjectDbContext dbContext) : base(dbContext) { }

        public async Task<ICmdResult> Execute(Author cmd)
            => await AddOrUpdate(DbContext.Authors, cmd);
    }
}