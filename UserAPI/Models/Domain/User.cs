using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models.Domain
{
    // Defines the User class as part of the domain model in the UserAPI
    public class User
    {
        // Specifies that UserId is the primary key and its value will be generated automatically
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        // Specifies that Name is required
        [Required]
        public string Name { get; set; }

        // Specifies that UserName is required
        [Required]
        public string UserName { get; set; }

        // Specifies that EmailAddress is required and must follow a valid email format
        /*[Required]*/
        [EmailAddress]
        public string? EmailAddress { get; set; }

        // Specifies that Password is required and must have a minimum length of 6 characters
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        // Specifies that ContactNumber is required, storing a long value (e.g., phone number)
        [Required]
        public long ContactNumber { get; set; }

        // Specifies that Address is required
        [Required]
        public string Address { get; set; }

        // Specifies that Pincode is required (for postal codes)
        [Required]
        public long Pincode { get; set; }

        // Specifies that AadhaarCard is required, storing a unique identifier like India's Aadhaar number
        [Required]
        public long AadhaarCard { get; set; }

        // BrokerId is optional (nullable), can be used for linking the user to a broker if needed
        public int? BrokerId { get; set; }
    }
}
