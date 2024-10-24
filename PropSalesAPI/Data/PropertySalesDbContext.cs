using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core for database access
using PropertySales.Models.Domain; // Importing domain models, including Property

namespace PropertySales.Data // Defining the namespace for data access layer
{
    public class PropertySalesDbContext : DbContext // Class representing the database context for property sales
    {
        // Constructor accepting DbContextOptions to configure the context
        public PropertySalesDbContext(DbContextOptions<PropertySalesDbContext> options) : base(options) { }

        // DbSet representing the collection of Property entities in the database
        public DbSet<Property> Properties { get; set; }
    }
}
