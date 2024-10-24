using TransactionAPI.Models.Domain;
using TransactionAPI.Repositories;

namespace TransactionAPI.Services
{
    // Concrete implementation of the ITransactionService interface.
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository; // Repository for accessing transaction data.

        // Constructor that takes an ITransactionRepository instance for dependency injection.
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository; // Initializes the repository field.
        }

        // Creates a new transaction by calling the repository's AddTransaction method.
        public Task<Transaction> CreateTransaction(Transaction transaction)
        {
            return _repository.AddTransaction(transaction);
        }

        // Retrieves all transactions by calling the repository's GetAllTransactions method.
        public Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return _repository.GetAllTransactions();
        }

        // Retrieves a specific transaction by its unique ID by calling the repository's GetTransactionById method.
        public Task<Transaction> GetTransactionById(int id)
        {
            return _repository.GetTransactionById(id);
        }

        // Retrieves all transactions associated with a specific broker ID by calling the repository's GetTransactionsByBrokerId method.
        public Task<IEnumerable<Transaction>> GetTransactionsByBrokerId(int brokerId)
        {
            return _repository.GetTransactionsByBrokerId(brokerId);
        }

        // Retrieves all transactions associated with a specific buyer ID by calling the repository's GetTransactionsByBuyerId method.
        public Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId)
        {
            return _repository.GetTransactionsByBuyerId(buyerId);
        }

        // Updates an existing transaction by calling the repository's UpdateTransaction method.
        public async Task UpdateTransaction(Transaction transaction)
        {
            await _repository.UpdateTransaction(transaction); // Ensure this method is awaited for proper async handling.
        }
    }
}
