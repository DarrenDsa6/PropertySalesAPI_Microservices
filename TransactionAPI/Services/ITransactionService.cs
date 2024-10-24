using TransactionAPI.Models.Domain;

namespace TransactionAPI.Services
{
    // Interface defining the contract for transaction service operations.
    public interface ITransactionService
    {
        // Creates a new transaction and returns the created transaction.
        Task<Transaction> CreateTransaction(Transaction transaction);

        // Retrieves all transactions.
        Task<IEnumerable<Transaction>> GetAllTransactions();

        // Retrieves a specific transaction by its unique ID.
        Task<Transaction> GetTransactionById(int id);

        // Retrieves all transactions associated with a specific broker ID.
        Task<IEnumerable<Transaction>> GetTransactionsByBrokerId(int brokerId);

        // Retrieves all transactions associated with a specific buyer ID.
        Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId);

        // Updates an existing transaction.
        Task UpdateTransaction(Transaction transaction);
    }
}
