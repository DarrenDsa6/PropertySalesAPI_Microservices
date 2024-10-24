using TransactionAPI.Models.Domain;

namespace TransactionAPI.Models.Views
{
    // DTO (Data Transfer Object) for representing a transaction.
    public class TransactionDto
    {
        // Unique identifier for the transaction.
        public int TransactionId { get; set; }

        // ID of the property involved in the transaction.
        public int PropertyId { get; set; }

        // ID of the buyer participating in the transaction.
        public int BuyerId { get; set; }

        // ID of the broker handling the transaction.
        public int BrokerId { get; set; }

        // The date on which the transaction occurred.
        public DateTime TransactionDate { get; set; }

        // The amount involved in the transaction.
        public decimal Amount { get; set; }

        // Status of the transaction (e.g., Pending, Completed, or Cancelled).
        public TransactionStatus Status { get; set; }
    }
}
