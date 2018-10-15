using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core.DataModels;
namespace Core.Context
{
    public class EbookShopContext : DbContext
    {
        

        public EbookShopContext(DbContextOptions<EbookShopContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<AuthorEbooks> AuthorEbooks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define composite key for the junction table 
            modelBuilder.Entity<AuthorEbooks>()
                .HasKey(ae => new { ae.AuthorId, ae.EbookId });

        }

    }
}
