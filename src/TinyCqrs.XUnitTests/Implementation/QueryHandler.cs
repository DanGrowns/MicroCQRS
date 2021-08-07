using System.Collections.Generic;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
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