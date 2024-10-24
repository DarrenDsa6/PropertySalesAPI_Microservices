using Microsoft.AspNetCore.Mvc;
using UserAPI.DTOs;
using UserAPI.Models.DTO;
using UserAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    // Constructor to inject the IUserService dependency
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // Endpoint to get a user by their ID (HTTP GET api/users/{id})
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        // Fetch the user based on their ID using the service
        var user = await _userService.GetUserByIdAsync(id);

        // If user is not found, return 404 Not Found, otherwise return 200 OK with the user data
        return user == null ? NotFound() : Ok(user);
    }

    // Endpoint to get all users (HTTP GET api/users)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        // Fetch all users and return them with 200 OK status
        return Ok(await _userService.GetAllUsersAsync());
    }

    // Endpoint to add a new user (HTTP POST api/users)
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto userDto)
    {
        // Validate the model state; if invalid, return 400 Bad Request
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Add the user using the service and return 201 Created response with the new user data
        var user = await _userService.AddUserAsync(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    // Endpoint to update an existing user (HTTP PUT api/users)
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        // Validate the model state; if invalid, return 400 Bad Request
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Update the user using the service and return 204 No Content if successful
        await _userService.UpdateUserAsync(userDto);
        return NoContent();
    }

    // Endpoint to delete a user by their ID (HTTP DELETE api/users/{id})
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        // Attempt to delete the user; if successful, return 204 No Content, otherwise return 404 Not Found
        var deleted = await _userService.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    // Endpoint for user login (HTTP POST api/users/login)
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        // Validate the model state; if invalid, return 400 Bad Request
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Attempt login using the service; if login fails, return 401 Unauthorized, otherwise return 200 OK with user data
        var user = await _userService.LoginAsync(loginDto);
        return user == null ? Unauthorized() : Ok(user);
    }

    // Endpoint to find a user by Aadhaar number (HTTP POST api/users/aadhaar)
    [HttpPost("aadhaar")]
    public async Task<ActionResult> FindAadhaar([FromBody] long aadhaar)
    {
        // Attempt to find the user by their Aadhaar number
        int userId = await _userService.FindAadhaarAsync(aadhaar);

        // If a user is found, return 200 OK with their userId, otherwise return 404 Not Found
        if (userId > 0)
        {
            return Ok(userId);
        }
        return NotFound("User not found");
    }
}
