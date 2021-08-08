using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Servicing.Interfaces
{
    public interface IProjectDbContext : IDbContextNet5
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<PostAuthor> PostAuthors { get; set; }
    }
}