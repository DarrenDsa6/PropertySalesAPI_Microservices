using BrokerAPI.Controllers;
using BrokerAPI.Models.Views;
using BrokerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerTest
{
    [TestFixture]
    public class BrokersControllerTests
    {
        private BrokersController _controller;
        private Mock<IBrokerService> _mockService;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IBrokerService>();
            _controller = new BrokersController(_mockService.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkResult_WithListOfBrokers()
        {
            // Arrange
            var brokers = new List<BrokerDto>
            {
                new BrokerDto { BrokerId = 1, Name = "Broker One" },
                new BrokerDto { BrokerId = 2, Name = "Broker Two" }
            };
            _mockService.Setup(s => s.GetAllBrokersAsync()).ReturnsAsync(brokers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as ActionResult<List<BrokerDto>>;
            Assert.IsNotNull(okResult);
            var okObjectResult = okResult.Result as OkObjectResult; // Correct casting here
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(brokers, okObjectResult.Value);
        }

        [Test]
        public async Task GetById_ReturnsOkResult_WithBroker()
        {
            // Arrange
            var broker = new BrokerDto { BrokerId = 1, Name = "Broker One" };
            _mockService.Setup(s => s.GetBrokerByIdAsync(1)).ReturnsAsync(broker);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result as ActionResult<BrokerDto>;
            Assert.IsNotNull(okResult);
            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(broker, okObjectResult.Value);
        }

        [Test]
        public async Task Create_ReturnsCreatedAtActionResult_WithNewBroker()
        {
            // Arrange
            var brokerDto = new BrokerDto { Name = "Broker One" };
            var createdBroker = new BrokerDto { BrokerId = 1, Name = "Broker One" };
            _mockService.Setup(s => s.AddBrokerAsync(brokerDto)).ReturnsAsync(createdBroker);

            // Act
            var result = await _controller.Create(brokerDto);

            // Assert
            var createdAtActionResult = result as ActionResult<BrokerDto>;
            Assert.IsNotNull(createdAtActionResult);
            var createdResult = createdAtActionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(createdBroker, createdResult.Value);
        }

        [Test]
        public async Task Delete_ReturnsNoContent_WhenBrokerIsDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteBrokerAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_ReturnsNotFound_WhenBrokerDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteBrokerAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
