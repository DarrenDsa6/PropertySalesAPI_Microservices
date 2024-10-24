using Microsoft.EntityFrameworkCore;
using TransactionAPI.Data;
using TransactionAPI.Models.Domain;

namespace TransactionAPI.Repositories
{
    // Concrete implementation of the ITransactionRepository interface.
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        // Constructor that takes a TransactionDbContext instance to interact with the database.
        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        // Adds a new transaction to the database and saves changes asynchronously.
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction); // Adds the transaction to the context.
            await _context.SaveChangesAsync(); // Saves changes to the database.
            return transaction; // Returns the added transaction.
        }

        // Retrieves all transactions from the database asynchronously.
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync(); // Returns a list of all transactions.
        }

        // Retrieves a specific transaction by its unique ID asynchronously.
        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions.FindAsync(id); // Finds and returns the transaction by ID.
        }

        // Retrieves all transactions associated with a specific broker ID asynchronously.
        public async Task<IEnumerable<Transaction>> GetTransactionsByBrokerId(int brokerId)
        {
            return await _context.Transactions
                .Where(t => t.BrokerId == brokerId) // Filters transactions by BrokerId.
                .ToListAsync(); // Returns a list of transactions.
        }

        // Retrieves all transactions associated with a specific buyer ID asynchronously.
        public async Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId)
        {
            return await _context.Transactions
                .Where(t => t.BuyerId == buyerId) // Filters transactions by BuyerId.
                .ToListAsync(); // Returns a list of transactions.
        }

        // Updates an existing transaction in the database and saves changes asynchronously.
        public async Task UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction); // Marks the transaction as modified.
            await _context.SaveChangesAsync(); // Saves changes to the database.
        }
    }
}
