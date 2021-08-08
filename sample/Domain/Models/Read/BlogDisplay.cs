using System.Collections.Generic;

namespace Domain.Models.Read
{
    public class BlogDisplay
    {
        public BlogDisplay(Blog blog, List<Post> posts)
        {
            Blog = blog;
            Posts = posts;
        }
        
        public Blog Blog { get; }
        public List<Post> Posts { get; }
    }
}