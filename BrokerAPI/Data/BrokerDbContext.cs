using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core for database operations
using BrokerAPI.Models; // Importing models for the application
using BrokerAPI.Models.Domain; // Importing domain models, likely including Broker

namespace BrokerAPI.Data
{
    // DbContext class for managing the database connection and entity configurations
    public class BrokerDbContext : DbContext
    {
        // Constructor that takes DbContextOptions for configuration
        public BrokerDbContext(DbContextOptions<BrokerDbContext> options) : base(options) { }

        // DbSet property representing the Brokers table in the database
        public DbSet<Broker> Brokers { get; set; }
    }
}
