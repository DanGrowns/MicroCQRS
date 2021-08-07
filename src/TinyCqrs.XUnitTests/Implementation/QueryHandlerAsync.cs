using System.Collections.Generic;
using System.Threading.Tasks;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    public class QueryHandlerAsync : IQueryHandlerAsync<Query, List<string>>
    {
        public Task<List<string>> Get(Query query)
        {
            return Task.FromResult(new List<string>());
        }
    }
}