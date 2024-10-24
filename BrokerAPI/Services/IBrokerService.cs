using BrokerAPI.Models.Views; // Importing view models (DTOs)
using System.Collections.Generic; // Importing collections for list handling
using System.Threading.Tasks; // Importing for asynchronous programming

namespace BrokerAPI.Services
{
    // Interface defining the contract for broker service operations
    public interface IBrokerService
    {
        // Asynchronously retrieves a list of all brokers
        Task<List<BrokerDto>> GetAllBrokersAsync();

        // Asynchronously retrieves a broker by its ID
        Task<BrokerDto> GetBrokerByIdAsync(int id);

        // Asynchronously adds a new broker using the provided BrokerDto
        Task<BrokerDto> AddBrokerAsync(BrokerDto brokerDto);

        // Asynchronously updates an existing broker's information
        Task<BrokerDto> UpdateBrokerAsync(int id, BrokerDto brokerDto);

        // Asynchronously deletes a broker by its ID
        Task<bool> DeleteBrokerAsync(int id);

        // Asynchronously handles broker login using the provided LoginDto
        Task<BrokerDto> LoginAsync(LoginDto loginDto);
    }
}
