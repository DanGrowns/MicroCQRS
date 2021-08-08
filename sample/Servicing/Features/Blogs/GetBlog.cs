using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Models;
using Domain.Models.Read;
using Microsoft.Data.Sqlite;
using Servicing.Interfaces;
using Servicing.Queries;
using TinyCqrs.Interfaces;

namespace Servicing.Features.Blogs
{
    public class GetBlog : IQueryHandlerAsync<IdQry, BlogDisplay>
    {
        private string ConnectionString { get; }

        public GetBlog(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();
        
        public async Task<BlogDisplay> Get(IdQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);

            if (query.Id > 0)
            {
                var sql = new StringBuilder();
                sql.Append(" SELECT * ");
                sql.Append(" FROM Blogs ");
                sql.Append($" WHERE Id = {query.Id}; ");

                sql.Append(" SELECT * ");
                sql.Append(" FROM Posts ");
                sql.Append($" WHERE BlogId = {query.Id} ");
            
                var reader = await connection.QueryMultipleAsync(sql.ToString());
                
                var blog = reader.Read<Blog>().FirstOrDefault();
                var posts = reader.Read<Post>().ToList();

                return new BlogDisplay(blog, posts);
            }

            return new BlogDisplay(new Blog(), new List<Post>());
        }
    }
}