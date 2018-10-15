using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core.DataModels;
namespace Core.Context
{
    public class EbookShopContext : DbContext
    {
        

        public EbookShopContext(DbContextOptions<EbookShopContext> options) : base(options){}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<AuthorEbooks> AuthorEbooks { get; set; }
        public DbSet<FilePath> Files { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EbookCategories> EbookCategories { get; set; }
        public DbSet<Order> Orders { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define composite key for the junction table 
            modelBuilder.Entity<AuthorEbooks>()
                .HasKey(ae => new { ae.AuthorId, ae.EbookId });

            modelBuilder.Entity<EbookCategories>()
                .HasKey(ec => new { ec.CategoryId, ec.EbookId });

        }

    }
}
