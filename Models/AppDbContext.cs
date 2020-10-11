using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestApp.Authentication;
using Microsoft.AspNetCore.Identity;

namespace TestApp.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=BOSHRA-PC\\SQLEXPRESS;Data Source=TestAppDatabase.db;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(x =>
            {
                x.HasKey(x => x.BookId);

                x.ToTable("tbBook");
            });



            modelBuilder.Entity<Shelf>(x =>
           {
               x.HasKey(x => x.Id);

               x.ToTable("tbShelf");
           });

            modelBuilder.Entity<BooksShelf>(x =>
           {
               x.HasKey(x => x.Id);

               x.HasOne(t => t.Book)
                   .WithMany(t => t.BooksShelfs)
                   .HasForeignKey(t => t.BookId);

            //    x.HasOne(t => t.Shelf)
            //        .WithMany(t => t.BooksShelfs)
            //        .HasForeignKey(t => t.ShelfId);

               x.ToTable("tbBooksShelf");
           });



        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<BooksShelf> BooksShelfs { get; set; }
    }
}
