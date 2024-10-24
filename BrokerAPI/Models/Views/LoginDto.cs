using System.ComponentModel.DataAnnotations; // Importing data annotations for validation

namespace BrokerAPI.Models.Views
{
    // Data Transfer Object (DTO) for user login information
    public class LoginDto
    {
        // Required property for the user's username
        [Required]
        public string UserName { get; set; }

        // Required property for the user's password with a minimum length of 6 characters
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
