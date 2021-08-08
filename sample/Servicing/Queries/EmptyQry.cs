using System.Collections.Generic;
using Domain.Models;
using Domain.Models.Read;
using TinyCqrs.Interfaces;

namespace Servicing.Queries
{
    public class EmptyQry : IQuery<List<PostOverview>>, IQuery<List<Blog>>, IQuery<List<Author>>
    {
        
    }
}