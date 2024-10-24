using BrokerAPI.Models;
using BrokerAPI.Models.Domain;
using BrokerAPI.Models.Views;
using BrokerAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerAPI.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IBrokerRepository _repository;

        public BrokerService(IBrokerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BrokerDto>> GetAllBrokersAsync()
        {
            var brokers = await _repository.GetAllBrokersAsync();
            return brokers.Select(b => new BrokerDto
            {
                BrokerId = b.BrokerId,
                Name = b.Name,
                UserName = b.UserName,
                Password = b.Password,
                ContactNumber = b.ContactNumber,
                Address = b.Address,
                Pincode = b.Pincode,
                AdhaarCard = b.AdhaarCard
                // Do not return password in a list context
            }).ToList();
        }

        public async Task<BrokerDto> GetBrokerByIdAsync(int id)
        {
            var broker = await _repository.GetBrokerByIdAsync(id);
            if (broker == null) return null;

            return new BrokerDto
            {
                BrokerId = broker.BrokerId,
                Name = broker.Name,
                UserName = broker.UserName,
                Password = broker.Password,
                ContactNumber = broker.ContactNumber,
                Address = broker.Address,
                Pincode = broker.Pincode,
                AdhaarCard = broker.AdhaarCard
                // Do not return password here
            };
        }

        public async Task<BrokerDto> LoginAsync(LoginDto loginDto)
        {
            var broker = await _repository.LoginAsync(loginDto.UserName, loginDto.Password);
            if (broker == null) return null;

            return new BrokerDto
            {
                BrokerId = broker.BrokerId,
                Name = broker.Name,
                UserName = broker.UserName,
                Password = broker.Password, // This is sensitive, use with caution
                ContactNumber = broker.ContactNumber,
                Address = broker.Address,
                Pincode = broker.Pincode,
                AdhaarCard = broker.AdhaarCard
            };
        }

        public async Task<BrokerDto> AddBrokerAsync(BrokerDto brokerDto)
        {
            var broker = new Broker
            {
                Name = brokerDto.Name,
                UserName = brokerDto.UserName,
                Password = brokerDto.Password, // Make sure to hash passwords in production
                ContactNumber = brokerDto.ContactNumber,
                Address = brokerDto.Address,
                Pincode = brokerDto.Pincode,
                AdhaarCard = brokerDto.AdhaarCard
            };

            var addedBroker = await _repository.AddBrokerAsync(broker);
            return new BrokerDto
            {
                BrokerId = addedBroker.BrokerId,
                Name = addedBroker.Name,
                UserName = addedBroker.UserName,
                Password = addedBroker.Password, // Again, use caution
                ContactNumber = addedBroker.ContactNumber,
                Address = addedBroker.Address,
                Pincode = addedBroker.Pincode,
                AdhaarCard = addedBroker.AdhaarCard
            };
        }

        public async Task<BrokerDto> UpdateBrokerAsync(int id, BrokerDto brokerDto)
        {
            var broker = await _repository.GetBrokerByIdAsync(id);
            if (broker == null) return null;

            broker.Name = brokerDto.Name;
            broker.UserName = brokerDto.UserName;
            broker.Password = brokerDto.Password; // Consider hashing here too
            broker.ContactNumber = brokerDto.ContactNumber;
            broker.Address = brokerDto.Address;
            broker.Pincode = brokerDto.Pincode;
            broker.AdhaarCard = brokerDto.AdhaarCard;

            var updatedBroker = await _repository.UpdateBrokerAsync(broker);
            return new BrokerDto
            {
                BrokerId = updatedBroker.BrokerId,
                Name = updatedBroker.Name,
                UserName = updatedBroker.UserName,
                Password = updatedBroker.Password, // Again, be cautious
                ContactNumber = updatedBroker.ContactNumber,
                Address = updatedBroker.Address,
                Pincode = updatedBroker.Pincode,
                AdhaarCard = updatedBroker.AdhaarCard
            };
        }

        public async Task<bool> DeleteBrokerAsync(int id) => await _repository.DeleteBrokerAsync(id);
    }
}
