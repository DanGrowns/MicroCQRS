using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Servicing.Classes;
using Servicing.Features.Authors;
using Servicing.Queries;
using Xunit;

namespace XUnitTests
{
    // Technically these are not unit tests, they're integrations
    // since I have them pointing to the real database.
    // Their function to me is to ensure the query syntax is correct.
    public class QueryTests
    {
        private SqliteConnector Connector { get; }

        public QueryTests() 
            => Connector = new SqliteConnector();

    }
}