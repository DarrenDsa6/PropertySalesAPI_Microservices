using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionAPI.Models.Domain;
using TransactionAPI.Models.Views;
using TransactionAPI.Services;

namespace TransactionAPI.Controllers
{
    // Marks the class as an API controller and sets the route for requests to "api/Transactions".
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _services;

        // Constructor that injects the transaction service for handling transactions.
        public TransactionsController(ITransactionService services)
        {
            _services = services;
        }

        // Endpoint to create a new transaction.
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            // Maps the incoming DTO to a domain Transaction model.
            var transaction = new Transaction
            {
                PropertyId = dto.PropertyId,
                BuyerId = dto.BuyerId,
                BrokerId = dto.BrokerId,
                TransactionDate = dto.TransactionDate,
                Amount = dto.Amount,
                Status = dto.Status
            };

            // Calls the service to create the transaction and gets the result.
            var result = await _services.CreateTransaction(transaction);

            // Returns a CreatedAtAction response, including the URI to the newly created transaction.
            return CreatedAtAction(nameof(GetTransactionById), new { id = result.TransactionId }, result);
        }

        // Endpoint to retrieve all transactions.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            // Calls the service to get all transactions and returns an Ok response.
            return Ok(await _services.GetAllTransactions());
        }

        // Endpoint to retrieve a transaction by its unique ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            // Calls the service to get a specific transaction by ID.
            var transaction = await _services.GetTransactionById(id);

            // If the transaction does not exist, return a 404 NotFound response.
            if (transaction == null) return NotFound();

            // Return the transaction in an Ok response.
            return Ok(transaction);
        }

        // Endpoint to retrieve all transactions for a specific broker.
        [HttpGet("broker/{brokerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByBrokerId(int brokerId)
        {
            // Calls the service to get all transactions by broker ID.
            return Ok(await _services.GetTransactionsByBrokerId(brokerId));
        }

        // Endpoint to retrieve all transactions for a specific buyer.
        [HttpGet("buyer/{buyerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByBuyerId(int buyerId)
        {
            // Calls the service to get all transactions by buyer ID.
            return Ok(await _services.GetTransactionsByBuyerId(buyerId));
        }

        // Endpoint to partially update a transaction by its ID.
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto dto)
        {
            // Checks if the request body is null, and if so, returns a 400 BadRequest response.
            if (dto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            // Calls the service to get the transaction by ID.
            var transaction = await _services.GetTransactionById(id);

            // If the transaction does not exist, return a 404 NotFound response.
            if (transaction == null)
            {
                return NotFound();
            }

            // Update the transaction amount if provided in the request.
            if (dto.Amount.HasValue)
            {
                transaction.Amount = dto.Amount.Value;
            }

            // Update the transaction status if provided in the request.
            if (dto.Status.HasValue)
            {
                transaction.Status = dto.Status.Value;
            }

            // Calls the service to update the transaction in the database.
            await _services.UpdateTransaction(transaction);

            // Returns a 204 NoContent response indicating a successful update.
            return NoContent();
        }
    }
}
