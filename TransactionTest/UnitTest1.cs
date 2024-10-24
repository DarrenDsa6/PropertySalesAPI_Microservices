using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionAPI.Controllers;
using TransactionAPI.Models.Domain;
using TransactionAPI.Models.Views;
using TransactionAPI.Services;

namespace TransactionTest
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        private TransactionsController _controller;
        private Mock<ITransactionService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ITransactionService>();
            _controller = new TransactionsController(_mockService.Object);
        }

        [Test]
        public async Task GetAllTransactions_ShouldReturnOkResult_WithListOfTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, PropertyId = 1, BuyerId = 1, BrokerId = 1, TransactionDate = DateTime.Now, Amount = 1000.00M, Status = TransactionStatus.Completed },
                new Transaction { TransactionId = 2, PropertyId = 2, BuyerId = 2, BrokerId = 2, TransactionDate = DateTime.Now, Amount = 1500.00M, Status = TransactionStatus.Pending }
            };

            _mockService.Setup(s => s.GetAllTransactions())
                        .ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetAllTransactions();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transactions, okResult.Value);
        }

        [Test]
        public async Task GetTransactionById_ShouldReturnOkResult_WhenTransactionExists()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionId = 1,
                PropertyId = 1,
                BuyerId = 1,
                BrokerId = 1,
                TransactionDate = DateTime.Now,
                Amount = 1000.00M,
                Status = TransactionStatus.Completed
            };

            _mockService.Setup(s => s.GetTransactionById(1))
                        .ReturnsAsync(transaction);

            // Act
            var result = await _controller.GetTransactionById(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(transaction, okResult.Value);
        }

        [Test]
        public async Task CreateTransaction_ShouldReturnCreatedResult_WhenTransactionIsSuccessful()
        {
            // Arrange
            var dto = new CreateTransactionDto
            {
                PropertyId = 1,
                BuyerId = 1,
                BrokerId = 1,
                TransactionDate = DateTime.Now,
                Amount = 1000.00M,
                Status = TransactionStatus.Pending
            };

            var createdTransaction = new Transaction
            {
                TransactionId = 1,
                PropertyId = dto.PropertyId,
                BuyerId = dto.BuyerId,
                BrokerId = dto.BrokerId,
                TransactionDate = dto.TransactionDate,
                Amount = dto.Amount,
                Status = dto.Status
            };

            _mockService.Setup(s => s.CreateTransaction(It.IsAny<Transaction>()))
                        .ReturnsAsync(createdTransaction);

            // Act
            var result = await _controller.CreateTransaction(dto);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(createdTransaction, createdAtActionResult.Value);
        }

        [Test]
        public async Task UpdateTransaction_ShouldReturnNoContent_WhenTransactionExists()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionId = 1,
                PropertyId = 1,
                BuyerId = 1,
                BrokerId = 1,
                TransactionDate = DateTime.Now,
                Amount = 1000.00M,
                Status = TransactionStatus.Pending
            };

            var updateDto = new UpdateTransactionDto
            {
                Amount = 1200.00M,
                Status = TransactionStatus.Completed
            };

            // Setup the mock to return the transaction when requested
            _mockService.Setup(s => s.GetTransactionById(1))
                        .ReturnsAsync(transaction);

            // Act
            var result = await _controller.UpdateTransaction(1, updateDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task GetTransactionById_ShouldReturnNotFound_WhenTransactionDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetTransactionById(1))
                        .ReturnsAsync((Transaction)null); // No transaction found

            // Act
            var result = await _controller.GetTransactionById(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
