using System.Collections.Generic;

namespace Domain.Models.Read
{
    public class AuthorDisplay
    {
        public AuthorDisplay(Author author, List<Post> posts)
        {
            Author = author;
            Posts = posts;
        }
        
        public Author Author { get; }
        public List<Post> Posts { get; }
    }
}