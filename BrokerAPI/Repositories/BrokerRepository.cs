using BrokerAPI.Data; // Importing the data context for database operations
using BrokerAPI.Models.Domain; // Importing domain models
using BrokerAPI.Models.Views; // Importing view models (DTOs)
using Microsoft.EntityFrameworkCore; // Importing EF Core components for database operations
using System.Collections.Generic; // Importing collections for list handling
using System.Threading.Tasks; // Importing for asynchronous programming

namespace BrokerAPI.Repositories
{
    // Repository class for managing broker data operations
    public class BrokerRepository : IBrokerRepository
    {
        private readonly BrokerDbContext _context; // Database context for interacting with the database

        // Constructor to initialize the repository with the database context
        public BrokerRepository(BrokerDbContext context)
        {
            _context = context; // Assigning the injected context to the private field
        }

        // Asynchronously retrieves all brokers from the database
        public async Task<List<Broker>> GetAllBrokersAsync() => await _context.Brokers.ToListAsync();

        // Asynchronously retrieves a broker by its ID
        public async Task<Broker> GetBrokerByIdAsync(int id) => await _context.Brokers.FindAsync(id);

        // Asynchronously adds a new broker to the database
        public async Task<Broker> AddBrokerAsync(Broker broker)
        {
            await _context.Brokers.AddAsync(broker); // Adding the broker to the context
            await _context.SaveChangesAsync(); // Saving changes to the database
            return broker; // Returning the added broker
        }

        // Asynchronously updates an existing broker in the database
        public async Task<Broker> UpdateBrokerAsync(Broker broker)
        {
            _context.Brokers.Update(broker); // Updating the broker in the context
            await _context.SaveChangesAsync(); // Saving changes to the database
            return broker; // Returning the updated broker
        }

        // Asynchronously deletes a broker by its ID
        public async Task<bool> DeleteBrokerAsync(int id)
        {
            var broker = await GetBrokerByIdAsync(id); // Retrieving the broker by ID
            if (broker == null) return false; // Return false if the broker does not exist

            _context.Brokers.Remove(broker); // Removing the broker from the context
            await _context.SaveChangesAsync(); // Saving changes to the database
            return true; // Return true indicating successful deletion
        }

        // Asynchronously logs in a broker using username and password
        public async Task<BrokerDto> LoginAsync(string username, string password)
        {
            // Fetch the broker by username
            var broker = await _context.Brokers
                .FirstOrDefaultAsync(b => b.UserName == username);

            // Validate the password (ensure you hash the password in a real application)
            if (broker != null && broker.Password == password) // Use a hashing mechanism here
            {
                return new BrokerDto // Creating a DTO to return broker data
                {
                    BrokerId = broker.BrokerId,
                    Name = broker.Name,
                    UserName = broker.UserName,
                    Password = broker.Password, // In practice, do not return the password
                    ContactNumber = broker.ContactNumber,
                    Pincode = broker.Pincode,
                    Address = broker.Address,
                    AdhaarCard = broker.AdhaarCard
                };
            }

            return null; // Return null if login fails
        }
    }
}
