using System.Collections.Generic;

namespace Domain.Models.Read
{
    public class PostDisplay
    {
        public PostDisplay(Post post, Blog blog, List<Author> postAuthors, List<Author> availableAuthors, List<Blog> availableBlogs)
        {
            Post = post;
            Blog = blog;
            PostAuthors = postAuthors;
            AvailableAuthors = availableAuthors;
            AvailableBlogs = availableBlogs;
        }
        
        public Post Post { get; }
        public Blog Blog { get; set; }
        public List<Author> PostAuthors { get; }
        public List<Author> AvailableAuthors { get; }
        public List<Blog> AvailableBlogs { get; set; }
    }
}