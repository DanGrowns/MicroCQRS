using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Servicing.Interfaces;
using Servicing.Queries;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Authors
{
    public class GetAuthors : IQueryHandlerAsync<EmptyQry, List<Author>>
    {
        private string ConnectionString { get; }

        public GetAuthors(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();
        
        public async Task<List<Author>> Get(EmptyQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);

            var sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM Authors ");

            var model = await connection.QueryAsync<Author>(sql.ToString());
            return model.ToList();
        }
    }
}