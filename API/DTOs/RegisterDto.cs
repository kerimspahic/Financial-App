using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [MinLength(Consts.UserNameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
        public string Email { get; set; }
        [RegularExpression(Consts.PasswordReqex, ErrorMessage = Consts.PasswordValidationError)]
        public string Password { get; set; }
    }
}