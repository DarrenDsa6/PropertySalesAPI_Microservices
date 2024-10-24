using Microsoft.AspNetCore.Mvc;
using Moq;
/*using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;*/
using UserAPI.DTOs;
using UserAPI.Models.DTO;
using UserAPI.Services;

namespace UserTest
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _controller;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Test]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            var userId = 1;
            var userDto = new UserDto { UserId = userId, Name = "John Doe" };
            _mockUserService.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(userDto);

            var result = await _controller.GetUser(userId);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            var okResult = result.Result as OkObjectResult; // Cast to OkObjectResult
            Assert.IsNotNull(okResult); // Ensure it's not null
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(userDto, okResult.Value);
        }

        [Test]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync((UserDto)null);

            var result = await _controller.GetUser(userId);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetAllUsers_ReturnsOkWithUserList()
        {
            var users = new List<UserDto>
            {
                new UserDto { UserId = 1, Name = "John Doe" },
                new UserDto { UserId = 2, Name = "Jane Doe" }
            };
            _mockUserService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _controller.GetAllUsers();

            Assert.IsInstanceOf<ActionResult<IEnumerable<UserDto>>>(result);
            var okResult = result.Result as OkObjectResult; // Cast to OkObjectResult
            Assert.IsNotNull(okResult); // Ensure it's not null
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(users, okResult.Value);
        }

        [Test]
        public async Task AddUser_ReturnsCreatedAtAction_WhenUserIsAdded()
        {
            var userDto = new UserDto { Name = "John Doe" };
            var createdUser = new UserDto { UserId = 1, Name = "John Doe" };
            _mockUserService.Setup(service => service.AddUserAsync(userDto)).ReturnsAsync(createdUser);

            var result = await _controller.AddUser(userDto);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            var createdResult = result.Result as CreatedAtActionResult; // Cast to CreatedAtActionResult
            Assert.IsNotNull(createdResult); // Ensure it's not null
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(createdUser, createdResult.Value);
            Assert.AreEqual("GetUser", createdResult.ActionName);
        }

        [Test]
        public async Task AddUser_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var userDto = new UserDto { Name = "" }; // Assume empty name is invalid
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.AddUser(userDto);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            var badRequestResult = result.Result as BadRequestObjectResult; // Cast to BadRequestObjectResult
            Assert.IsNotNull(badRequestResult); // Ensure it's not null
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task UpdateUser_ReturnsNoContent_WhenUserIsUpdated()
        {
            var userDto = new UserDto { UserId = 1, Name = "Updated User" };
            _mockUserService.Setup(service => service.UpdateUserAsync(userDto)).ReturnsAsync(userDto);

            var result = await _controller.UpdateUser(userDto);

            Assert.IsInstanceOf<ActionResult>(result);
            var noContentResult = result as NoContentResult; // Cast to NoContentResult
            Assert.IsNotNull(noContentResult); // Ensure it's not null
        }

        [Test]
        public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.DeleteUserAsync(userId)).ReturnsAsync(true);

            var result = await _controller.DeleteUser(userId);

            Assert.IsInstanceOf<ActionResult>(result);
            var noContentResult = result as NoContentResult; // Cast to NoContentResult
            Assert.IsNotNull(noContentResult); // Ensure it's not null
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var userId = 1;
            _mockUserService.Setup(service => service.DeleteUserAsync(userId)).ReturnsAsync(false);

            var result = await _controller.DeleteUser(userId);

            Assert.IsInstanceOf<ActionResult>(result);
            var notFoundResult = result as NotFoundResult; // Cast to NotFoundResult
            Assert.IsNotNull(notFoundResult); // Ensure it's not null
        }

        [Test]
        public async Task Login_ReturnsOk_WhenUserLogsInSuccessfully()
        {
            var loginDto = new LoginDto { UserName = "user", Password = "password" };
            var userDto = new UserDto { UserId = 1, Name = "John Doe" };
            _mockUserService.Setup(service => service.LoginAsync(loginDto)).ReturnsAsync(userDto);

            var result = await _controller.Login(loginDto);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            var okResult = result.Result as OkObjectResult; // Cast to OkObjectResult
            Assert.IsNotNull(okResult); // Ensure it's not null
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(userDto, okResult.Value);
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenLoginFails()
        {
            var loginDto = new LoginDto { UserName = "wrongUser", Password = "wrongPassword" };
            _mockUserService.Setup(service => service.LoginAsync(loginDto)).ReturnsAsync((UserDto)null);

            var result = await _controller.Login(loginDto);

            Assert.IsInstanceOf<ActionResult<UserDto>>(result);
            Assert.IsInstanceOf<UnauthorizedResult>(result.Result);
        }

    }
        
}
