using Domain.Interfaces;

namespace Domain.Models
{
    public class Blog : IPrimaryKeyInteger
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}