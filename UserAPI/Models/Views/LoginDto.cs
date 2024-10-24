using System.ComponentModel.DataAnnotations;

namespace UserAPI.DTOs
{
    public class LoginDto
    {
        // Username and password data transfer object to login to the site
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
