using System.Collections.Generic;
using System.Threading.Tasks;
using UserAPI.Models.Domain;

namespace UserAPI.Repositories
{
    // The IUserRepository interface defines the contract for user-related data access operations
    public interface IUserRepository
    {
        // Retrieves a user by their unique ID
        Task<User> GetUserByIdAsync(int userId);

        // Retrieves a list of all users
        Task<IEnumerable<User>> GetAllUsersAsync();

        // Adds a new user and returns the created user
        Task<User> AddUserAsync(User user);

        // Updates an existing user and returns the updated user
        Task<User> UpdateUserAsync(User user);

        // Deletes a user by their unique ID and returns a boolean indicating success or failure
        Task<bool> DeleteUserAsync(int userId);

        // Validates user login credentials and returns the user if valid
        Task<User> ValidateUserLoginAsync(string email, string password);

        // Validates an Aadhaar number and returns the associated user ID
        Task<int> ValidateAadhaarAsync(long aadhaar);
    }
}
