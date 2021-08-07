using System.Collections.Generic;
using System.Threading.Tasks;
using MicroCqrs.Interfaces;

namespace MicroCqrs.XUnitTests.Implementation
{
    public class QueryHandlerAsync : IQueryHandlerAsync<Query, List<string>>
    {
        public Task<List<string>> Get(Query query)
        {
            return Task.FromResult(new List<string>());
        }
    }
}