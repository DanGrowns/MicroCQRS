using System;
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

namespace Servicing.Features.Authors
{
    public class GetAuthor : IQueryHandlerAsync<IdQry, AuthorDisplay>
    {
        private string ConnectionString { get; }

        public GetAuthor(IDbConnector dbConnector)
            => ConnectionString = dbConnector.GetConnection();
        
        public async Task<AuthorDisplay> Get(IdQry query)
        {
            await using var connection = new SqliteConnection(ConnectionString);
            
            if (query.Id > 0)
            {
                var sql = new StringBuilder();
                sql.Append(" SELECT * ");
                sql.Append(" FROM Authors ");
                sql.Append($" WHERE Id = {query.Id}; ");

                sql.Append(" SELECT p.* ");
                sql.Append(" FROM Posts p ");
                sql.Append(" LEFT JOIN PostAuthors pa ");
                sql.Append(" ON p.Id = pa.PostId ");
                sql.Append(" LEFT JOIN Authors a ");
                sql.Append(" ON pa.AuthorId = a.Id ");
                sql.Append($" WHERE AuthorId = {query.Id}; ");
                
                var reader = await connection.QueryMultipleAsync(sql.ToString());

                var author = reader.Read<Author>().FirstOrDefault();
                var posts = reader.Read<Post>().ToList();
                
                return new AuthorDisplay(author, posts);
            }

            return new AuthorDisplay(new Author(), new List<Post>());
        }
    }
}