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

namespace Servicing.Features.Posts
{
    public class GetPost : IQueryHandlerAsync<IdQry, PostDisplay>
    {
        private string ConnectionString { get; }

        public GetPost(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();

        private static string AssociatedSetsQuery() => " SELECT * FROM Authors; SELECT * FROM Blogs; ";
        
        public async Task<PostDisplay> Get(IdQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);

            SqlMapper.GridReader reader;
            List<Author> availableAuthors;
            List<Blog> availableBlogs;
            
            if (query.Id > 0)
            {
                var sql = new StringBuilder();
                sql.Append(" SELECT * ");
                sql.Append(" FROM Posts ");
                sql.Append($" WHERE Id = {query.Id}; ");
            
                sql.Append(" SELECT a.* ");
                sql.Append(" FROM PostAuthors pa ");
                sql.Append(" INNER JOIN Authors a ON pa.AuthorId = a.Id ");
                sql.Append($" WHERE PostId = {query.Id}; ");

                sql.Append(AssociatedSetsQuery());

                reader = await connection.QueryMultipleAsync(sql.ToString());
                
                var post = reader.Read<Post>().FirstOrDefault();
                var postAuthors = reader.Read<Author>().ToList();
                
                availableAuthors = reader.Read<Author>().ToList();
                availableBlogs = reader.Read<Blog>().ToList();

                var blog = availableBlogs.FirstOrDefault(x => x.Id == post.BlogId);
                
                return new PostDisplay(post, blog, postAuthors, availableAuthors, availableBlogs);
            }
           
            reader = await connection.QueryMultipleAsync(AssociatedSetsQuery());
            availableAuthors = reader.Read<Author>().ToList();
            availableBlogs = reader.Read<Blog>().ToList();
            
            return new PostDisplay(new Post(), new Blog(), new List<Author>(), availableAuthors, availableBlogs);
        }
    }
}