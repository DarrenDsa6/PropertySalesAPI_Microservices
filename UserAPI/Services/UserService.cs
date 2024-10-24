using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Models.DTO;
using UserAPI.Models.Domain;
using UserAPI.Repositories;
using UserAPI.DTOs;

namespace UserAPI.Services
{
    // The UserService class handles business logic for user-related operations
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor that injects the IUserRepository dependency
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Retrieves a user by their ID and maps the User domain model to UserDto
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user == null ? null : MapToDto(user);
        }

        // Retrieves all users, mapping each User domain model to UserDto
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(MapToDto).ToList(); // Converts each User to UserDto
        }

        // Adds a new user, hashes the password, and maps the UserDto to a User domain model
        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            // Map the UserDto to the User domain model
            var user = MapToDomain(userDto);

            // Hash the password before saving
            user.Password = HashPassword(user.Password);

            // Save the user and return the created user as a UserDto
            return MapToDto(await _userRepository.AddUserAsync(user));
        }

        // Updates an existing user's details
        public async Task<UserDto> UpdateUserAsync(UserDto userDto)
        {
            // Map the UserDto to the User domain model
            var user = MapToDomain(userDto);

            // Update the user in the repository and return the updated UserDto
            return MapToDto(await _userRepository.UpdateUserAsync(user));
        }

        // Deletes a user by their ID, returns a boolean indicating success or failure
        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        // Authenticates a user by checking their username and password
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Ensure the password is checked against the hashed version (implement hashing)
            var user = await _userRepository.ValidateUserLoginAsync(loginDto.UserName, loginDto.Password);
            return user == null ? null : MapToDto(user);
        }

        // Finds a user by their Aadhaar number and returns the associated user ID
        public async Task<int> FindAadhaarAsync(long aadhaar)
        {
            int id = await _userRepository.ValidateAadhaarAsync(aadhaar);
            return id <= 0 ? -1 : id; // Returns -1 if Aadhaar not found
        }

        // Maps the User domain model to UserDto for data transfer purposes
        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName,
                /*EmailAddress = user.EmailAddress,*/
                // Password is not returned for security reasons
                ContactNumber = user.ContactNumber,
                Address = user.Address,
                Pincode = user.Pincode,
                AadhaarCard = user.AadhaarCard,
                BrokerId = user.BrokerId
            };
        }

        // Maps the UserDto to the User domain model for use in database operations
        private User MapToDomain(UserDto userDto)
        {
            return new User
            {
                UserId = userDto.UserId,
                Name = userDto.Name,
                UserName = userDto.UserName,
                /*EmailAddress = userDto.EmailAddress,*/
                // Password will be hashed before storing
                Password = userDto.Password,
                ContactNumber = userDto.ContactNumber,
                Address = userDto.Address,
                Pincode = userDto.Pincode,
                AadhaarCard = userDto.AadhaarCard,
                BrokerId = userDto.BrokerId
            };
        }

        // Placeholder method for password hashing; implement actual hashing logic
        private string HashPassword(string password)
        {
            // Implement password hashing logic (e.g., using BCrypt or another secure method)
            return password; // Placeholder: replace with actual hashing implementation
        }
    }
}
