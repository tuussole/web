using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryDB")
        { }
        public DbSet<Student> Students { get; set; }
        public DbSet<OrderForm> OrderForms { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookToAuthor> BooksToAuthors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<BookToAuthor>().HasKey(bta => new { bta.AuthorId, bta.BookId });

            modelBuilder.Entity<BookToAuthor>().HasRequired(bta => bta.Book).WithMany(b => b.Authors).HasForeignKey(bta => bta.BookId);
            modelBuilder.Entity<BookToAuthor>().HasRequired(bta => bta.Author).WithMany(a => a.Books).HasForeignKey(bta => bta.AuthorId);

        }
    }
}