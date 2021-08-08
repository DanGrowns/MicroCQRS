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
    public class GetPostsOverview : IQueryHandlerAsync<EmptyQry, List<PostOverview>>
    {
        private class PostAuthorConnection
        {
            public int PostId { get; set; }
            public int AuthorId { get; set; }
            public int Id { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
        }
        
        private string ConnectionString { get; }

        public GetPostsOverview(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();
        
        public async Task<List<PostOverview>> Get(EmptyQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);

            var sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM Posts; ");
            
            sql.Append(" SELECT * ");
            sql.Append(" FROM PostAuthors pa ");
            sql.Append(" INNER JOIN Authors a ON pa.AuthorId = a.Id ");

            var reader = await connection.QueryMultipleAsync(sql.ToString());

            var allPosts = reader.Read<Post>().ToList();
            var allAuthors = reader.Read<PostAuthorConnection>().ToList();

            var list = new List<PostOverview>();
            
            foreach (var post in allPosts)
            {
                var authors = 
                    allAuthors.Where(x => x.PostId == post.Id)
                        .Select(x => new Author{Id = x.Id, Forename = x.Forename, Surname = x.Surname})
                        .ToList();
                
                list.Add(new PostOverview(post, authors));
            }
            
            return list.ToList();
        }
    }
}