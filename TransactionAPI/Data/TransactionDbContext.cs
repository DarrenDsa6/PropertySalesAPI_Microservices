using Microsoft.EntityFrameworkCore;
using TransactionAPI.Models.Domain;
using TransactionAPI;

namespace TransactionAPI.Data
{
    // Represents the database context for transactions.
    // Inherits from DbContext to enable interaction with the database.
    public class TransactionDbContext : DbContext
    {
        // Constructor that passes the database options to the base DbContext class.
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        // DbSet that represents the Transactions table in the database.
        public DbSet<Transaction> Transactions { get; set; }
    }
}
