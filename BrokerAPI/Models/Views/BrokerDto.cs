namespace BrokerAPI.Models.Views
{
    // Data Transfer Object (DTO) for transferring broker data
    public class BrokerDto
    {
        // Unique identifier for the broker
        public int BrokerId { get; set; }

        // Name of the broker
        public string Name { get; set; }

        // Username of the broker
        public string UserName { get; set; }

        // Password of the broker (consider removing or encrypting in practice)
        public string Password { get; set; }

        // Contact number of the broker
        public long ContactNumber { get; set; }

        // Address of the broker
        public string Address { get; set; }

        // Pincode of the broker's location
        public long Pincode { get; set; }

        // Aadhaar card number of the broker
        public long AdhaarCard { get; set; }
    }
}
