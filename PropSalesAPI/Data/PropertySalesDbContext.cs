using Microsoft.EntityFrameworkCore;
using PropertySales.Models.Domain;

namespace PropSalesAPI.Data
{
    public class PropertySalesDbContext : DbContext
    {
        public PropertySalesDbContext(DbContextOptions<PropertySalesDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }

    }
}

