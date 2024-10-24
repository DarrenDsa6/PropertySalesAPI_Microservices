using BrokerAPI.Models; // Importing models for the application
using BrokerAPI.Models.Domain; // Importing domain models
using BrokerAPI.Models.Views; // Importing view models (DTOs)
using System.Collections.Generic; // Importing collections for list handling
using System.Threading.Tasks; // Importing for asynchronous programming

namespace BrokerAPI.Repositories
{
    // Interface defining the contract for broker data operations
    public interface IBrokerRepository
    {
        // Asynchronously retrieves a list of all brokers
        Task<List<Broker>> GetAllBrokersAsync();

        // Asynchronously retrieves a broker by its ID
        Task<Broker> GetBrokerByIdAsync(int id);

        // Asynchronously adds a new broker to the database
        Task<Broker> AddBrokerAsync(Broker broker);

        // Asynchronously updates an existing broker's information
        Task<Broker> UpdateBrokerAsync(Broker broker);

        // Asynchronously deletes a broker by its ID
        Task<bool> DeleteBrokerAsync(int id);

        // Asynchronously logs in a broker using username and password
        Task<BrokerDto> LoginAsync(string username, string password);
    }
}
