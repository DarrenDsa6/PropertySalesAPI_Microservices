using Microsoft.EntityFrameworkCore;
using UserAPI.Models.Domain;

namespace UserAPI.Data
{
    // The UserDbContext class represents the database context used by Entity Framework Core
    public class UserDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base class constructor
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        // DbSet<User> represents the Users table in the database
        public DbSet<User> Users { get; set; }
    }
}
