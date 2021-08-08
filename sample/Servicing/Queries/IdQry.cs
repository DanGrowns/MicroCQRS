using Domain.Models;
using Domain.Models.Read;
using TinyCqrs.Interfaces;

namespace Servicing.Queries
{
    public class IdQry : IQuery<PostDisplay>, IQuery<AuthorDisplay>, IQuery<BlogDisplay>
    {
        public IdQry(int id)
            => Id = id;
        
        public int Id { get; }
    }
}