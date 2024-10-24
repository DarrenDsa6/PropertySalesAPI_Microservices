using System.Collections.Generic;
using System.Threading.Tasks;
using UserAPI.DTOs;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    // The IUserService interface defines the contract for user-related business logic operations
    public interface IUserService
    {
        // Retrieves a user by their ID and returns the user data as a UserDto
        Task<UserDto> GetUserByIdAsync(int userId);

        // Retrieves a list of all users, returning them as a collection of UserDto objects
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        // Adds a new user based on the provided UserDto and returns the created user's data
        Task<UserDto> AddUserAsync(UserDto userDto);

        // Updates an existing user's details based on the provided UserDto and returns the updated user data
        Task<UserDto> UpdateUserAsync(UserDto userDto);

        // Deletes a user by their ID and returns a boolean indicating success or failure
        Task<bool> DeleteUserAsync(int userId);

        // Authenticates a user based on the provided LoginDto (credentials) and returns the authenticated user's data
        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Finds a user by their Aadhaar number and returns the associated user ID
        Task<int> FindAadhaarAsync(long aadhaar);
    }
}
