using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Servicing.Interfaces;

namespace Servicing.Classes
{
    public class ProjectDbContext : DbContext, IProjectDbContext
    {
        private string ConnectionString { get; }
        
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostAuthor> PostAuthors { get; set; }

        public ProjectDbContext()
        {
            var dbConnector = new SqliteConnector();
            ConnectionString = dbConnector.GetConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Id).IsUnique();

                e.Property(x => x.Forename).HasMaxLength(100).IsRequired();
                e.Property(x => x.Surname).HasMaxLength(100).IsRequired();
            });
            
            modelBuilder.Entity<Blog>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Id).IsUnique();

                e.Property(x => x.Name).HasMaxLength(255).IsRequired();
            });
            
            modelBuilder.Entity<Post>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Id).IsUnique();

                e.Property(x => x.BlogId).IsRequired();
                e.Property(x => x.Title).HasMaxLength(255).IsRequired();
                e.Property(x => x.Content).IsRequired();
            });
            
            modelBuilder.Entity<PostAuthor>(e =>
            {
                e.HasKey(x => new { x.AuthorId, x.PostId });
            });
        }
    }
}