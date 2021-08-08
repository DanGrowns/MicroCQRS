using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Read
{
    public class PostOverview
    {
        public PostOverview(Post post, List<Author> authors)
        {
            Post = post;

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.Append($"{author.Forename} {author.Surname}, ");
            }

            sb.Length--;
            sb.Length--;

            Authors = sb.ToString();
        }
        
        public Post Post { get; }
        public string Authors { get; }
    }
}