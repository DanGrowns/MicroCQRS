using System.Collections.Generic;
using MicroCqrs.Interfaces;

namespace MicroCqrs.XUnitTests.Implementation
{
    public class Query : IQuery<List<string>> { }
    
    public class QueryHandler : IQueryHandler<Query, List<string>>
    {
        public List<string> Get(Query query)
        {
            return new();
        }
    }
}