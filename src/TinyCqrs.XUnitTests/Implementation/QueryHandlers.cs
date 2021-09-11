using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TinyCqrs.Interfaces;

namespace TinyCqrs.XUnitTests.Implementation
{
    [ExcludeFromCodeCoverage]
    public class Query : IQuery<List<string>>
    {
        public Query(string startsWith = "This")
            => StartsWith = startsWith;
        
        public string StartsWith { get; }
    }
    
    [ExcludeFromCodeCoverage]
    public class QueryHandler1 : IQueryHandler<List<string>>
    {
        public List<string> Get()
        {
            return new(){"Str"};
        }
    }
    
    [ExcludeFromCodeCoverage]
    public class QueryHandler2 : IQueryHandler<Query, List<string>>
    {
        public List<string> Get(Query query)
        {
            var list = new List<string>()
            {
                "ThisObject",
                "ThatObject"
            };

            var output = 
                list
                    .Where(x => x.StartsWith(query.StartsWith))
                    .ToList();

            return output;
        }
    }
    
    [ExcludeFromCodeCoverage]
    public class QueryHandlerAsync1 : IQueryHandlerAsync<List<string>>
    {
        public Task<List<string>> Get()
        {
            return Task.FromResult(new List<string> {"Str"});
        }
    }
    
    [ExcludeFromCodeCoverage]
    public class QueryHandlerAsync2 : IQueryHandlerAsync<Query, List<string>>
    {
        public Task<List<string>> Get(Query query)
        {
            var list = new List<string>()
            {
                "ThisObject",
                "ThatObject"
            };

            var output = 
                list
                    .Where(x => x.StartsWith(query.StartsWith))
                    .ToList();
            
            return Task.FromResult(output);
        }
    }
}