using Library.Infrastructure.Configurations;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.DataContext
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.Entity<Administrator>().HasData(new Administrator
            {
                Id = 1,
                UserName = "admin",
                Password = "admin1234",
                Name = "admin",
                Surname = "admin",
            });
        }
    }
}
