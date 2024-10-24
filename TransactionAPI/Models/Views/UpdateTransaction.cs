using TransactionAPI.Models.Domain;

namespace TransactionAPI.Models.Views
{
    // DTO (Data Transfer Object) for updating an existing transaction.
    public class UpdateTransactionDto
    {
        // Optional property to update the amount involved in the transaction.
        public decimal? Amount { get; set; }

        // Optional property to update the status of the transaction.
        public TransactionStatus? Status { get; set; }
    }
}
