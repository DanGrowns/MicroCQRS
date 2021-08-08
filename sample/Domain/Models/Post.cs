using Domain.Interfaces;

namespace Domain.Models
{
    public class Post : IPrimaryKeyInteger
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}