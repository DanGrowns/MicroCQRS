using System.Threading.Tasks;
using Domain.Models;
using Servicing.Abstract;
using Servicing.Interfaces;
using TinyCqrs.Attributes;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Blogs
{
    [CqrsDecoratedBy(typeof(ValidateBlog))]
    public class SaveBlog : EntityFrameworkAlterModel, ICmdHandlerAsync<Blog>
    {
        public SaveBlog(IProjectDbContext dbContext) : base(dbContext) { }

        public async Task<ICmdResult> Execute(Blog cmd)
            => await AddOrUpdate(DbContext.Blogs, cmd);
    }
}