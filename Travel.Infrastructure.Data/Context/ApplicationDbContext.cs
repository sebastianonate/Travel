using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travel.Core.Domain;

namespace Travel.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContextBase 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>(c =>
            {
                c.HasKey(t => t.Id).HasName("ISBN");
                c.HasMany(t=>t.Autores);
                c.HasOne(t => t.Editorial);
            });
            modelBuilder.Entity<Autor>(c =>
            {
                c.HasKey(t => t.Id);
                c.HasMany(t => t.Libros);
            });
            modelBuilder.Entity<Editorial>(c =>
            {
                c.HasKey(t => t.Id);
            });
        }
    }
}