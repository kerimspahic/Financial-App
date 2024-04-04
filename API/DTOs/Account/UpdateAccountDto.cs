using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class UpdateAccountDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Username must be 5 characters long")]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}