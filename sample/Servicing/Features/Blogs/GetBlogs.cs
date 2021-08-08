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

namespace Servicing.Features.Blogs
{
    public class GetBlogs : IQueryHandlerAsync<EmptyQry, List<Blog>>
    {
        private string ConnectionString { get; }

        public GetBlogs(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();
        
        public async Task<List<Blog>> Get(EmptyQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);

            var sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM Blogs ");

            var model = await connection.QueryAsync<Blog>(sql.ToString());
            return model.ToList();
        }
    }
}