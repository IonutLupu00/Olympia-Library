using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Olympia_Library.Models;

namespace Olympia_Library.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Olympia_Library.Models.ApplicationUserModel> ApplicationUserModel { get; set; }

    }
}
