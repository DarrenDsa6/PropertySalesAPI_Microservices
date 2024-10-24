using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PropertySales.Data;
using PropertySales.Models.Domain;


namespace PropertySales.Tests
{
    public class PropertyControllerTests
    {
        private PropertySalesDbContext _context;
        private PropertyController _controller;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database for testing
            var options = new DbContextOptionsBuilder<PropertySalesDbContext>()
                .UseInMemoryDatabase(databaseName: "PropertyDb")
                .Options;

            _context = new PropertySalesDbContext(options);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(m => m["ImageStorage:Path"]).Returns("Uploads");

            _controller = new PropertyController(_context, mockConfig.Object);
        }

        [Test]
        public async Task AddProperty_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new PropertyUploadRequest
            {
                AddedBy = 1,
                PropertyType = PropertyType.Sale,
                Location = "123 Main St",
                Pincode = "12345",
                Price = 100000,
                Description = "A beautiful house",
                Amenities = "Pool, Garage",
                Status = PropertyStatus.Active,
                ImageFiles = new List<IFormFile>() // Mock image files if necessary
            };

            // Act
            var result = await _controller.AddProperty(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetAllProperties_ReturnsAllProperties()
        {
            // Arrange
            var property = new Property
            {
                PropertyId = 1,
                PropertyType = PropertyType.Rent,
                Location = "456 Main St",
                Pincode = "54321",
                Price = 1500,
                Description = "A lovely apartment",
                Amenities = "Gym, WiFi",
                Status = PropertyStatus.Active,
                AddedBy = 1
            };
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllProperties();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var properties = okResult.Value as List<Property>;
            Assert.IsNotNull(properties);
            Assert.AreEqual(1, properties.Count);
        }

        [Test]
        public async Task UpdatePropertyPartial_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var property = new Property
            {
                PropertyId = 1,
                PropertyType = PropertyType.Sale,
                Location = "789 Main St",
                Pincode = "67890",
                Price = 200000,
                Description = "A mansion",
                Amenities = "Garden",
                Status = PropertyStatus.Active,
                AddedBy = 1
            };
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            var request = new PropertyPatchDto
            {
                Price = 250000
            };

            // Act
            var result = await _controller.UpdatePropertyPartial(1, request);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            var updatedProperty = await _context.Properties.FindAsync(1);
            Assert.AreEqual(250000, updatedProperty.Price);
        }

        [Test]
        public async Task DeleteProperty_ValidId_ReturnsOkResult()
        {
            // Arrange
            var property = new Property
            {
                PropertyId = 1,
                PropertyType = PropertyType.Sale,
                Location = "321 Main St",
                Pincode = "11223",
                Price = 300000,
                Description = "Luxury villa",
                Amenities = "Pool",
                Status = PropertyStatus.Active,
                AddedBy = 1
            };
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteProperty(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetPropertiesByUserId_ValidUserId_ReturnsProperties()
        {
            // Arrange
            var property1 = new Property
            {
                PropertyId = 1,
                PropertyType = PropertyType.Sale,
                Location = "111 Main St",
                Pincode = "11111",
                Price = 400000,
                Description = "Charming cottage",
                Amenities = "Fireplace",
                Status = PropertyStatus.Active,
                AddedBy = 1
            };
            var property2 = new Property
            {
                PropertyId = 2,
                PropertyType = PropertyType.Rent,
                Location = "222 Main St",
                Pincode = "22222",
                Price = 1200,
                Description = "Modern apartment",
                Amenities = "Elevator",
                Status = PropertyStatus.Active,
                AddedBy = 1
            };
            await _context.Properties.AddRangeAsync(property1, property2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetPropertiesByUserId(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var properties = okResult.Value as List<Property>;
            Assert.IsNotNull(properties);
            Assert.AreEqual(2, properties.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Dispose of the context
            _context.Database.EnsureDeleted(); // Clean up after each test
        }
    }
}
