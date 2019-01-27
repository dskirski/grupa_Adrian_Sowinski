using Microsoft.EntityFrameworkCore;
using EbookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EbookShop.DataAccess
{
    public class EbookShopContext : IdentityDbContext<AppUser>
    {
        public EbookShopContext(DbContextOptions<EbookShopContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<EbookAuthor> EbookAuthors { get; set; }
        public DbSet<FilePath> Files { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<EbookGenre> EbookGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // define composite key for the junction table 
            modelBuilder.Entity<EbookAuthor>()
                .HasKey(e => new { e.AuthorId, e.EbookId });
            modelBuilder.Entity<EbookGenre>()
                .HasKey(e => new { e.EbookId, e.GenreId });


        }

    }
}
