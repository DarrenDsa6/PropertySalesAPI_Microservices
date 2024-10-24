using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models.Domain;
using UserAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    // Constructor that injects the UserDbContext dependency
    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    // Retrieves a user by their ID using Entity Framework's FindAsync method
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    // Retrieves all users as a list asynchronously (consider implementing pagination for large datasets)
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Adds a new user to the database and saves the changes
    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user); // Adds the user entity to the DbContext
        await _context.SaveChangesAsync(); // Saves changes asynchronously to the database
        return user;
    }

    // Updates an existing user's details and saves the changes to the database
    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user); // Marks the user entity as modified
        await _context.SaveChangesAsync(); // Saves the updates asynchronously to the database
        return user;
    }

    // Deletes a user by their ID, returns false if user not found, true if successful
    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId); // Retrieves the user by ID
        if (user == null) return false; // Returns false if no user is found

        _context.Users.Remove(user); // Removes the user entity from the DbContext
        await _context.SaveChangesAsync(); // Saves the changes asynchronously
        return true;
    }

    // Validates a user's login credentials by checking the username and password
    public async Task<User> ValidateUserLoginAsync(string username, string password)
    {
        // Checks the username and password in the database (should ideally hash the password)
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
    }

    // Validates an Aadhaar number, returning the user ID if found, or -1 if not found
    public async Task<int> ValidateAadhaarAsync(long aadhaar)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AadhaarCard == aadhaar);
        return user?.UserId ?? -1; // Return userId if found, otherwise return -1
    }
}
