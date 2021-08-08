using Domain.Interfaces;

namespace Domain.Models
{
    public class Author : IPrimaryKeyInteger
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
    }
}