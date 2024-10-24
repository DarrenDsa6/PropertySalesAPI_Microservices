using TransactionAPI.Models.Domain;

namespace TransactionAPI.Repositories
{
    // Interface defining the contract for transaction repository operations.
    public interface ITransactionRepository
    {
        // Adds a new transaction to the repository and returns the added transaction.
        Task<Transaction> AddTransaction(Transaction transaction);

        // Retrieves all transactions from the repository.
        Task<IEnumerable<Transaction>> GetAllTransactions();

        // Retrieves a specific transaction by its unique ID.
        Task<Transaction> GetTransactionById(int id);

        // Retrieves all transactions associated with a specific broker ID.
        Task<IEnumerable<Transaction>> GetTransactionsByBrokerId(int brokerId);

        // Retrieves all transactions associated with a specific buyer ID.
        Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId);

        // Updates an existing transaction in the repository.
        Task UpdateTransaction(Transaction transaction);
    }
}
