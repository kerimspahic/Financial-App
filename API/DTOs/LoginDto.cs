using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [MinLength(Consts.UserNameMinLength, ErrorMessage =Consts.UsernameLengthValidationError)]
        public string Username { get; set; }
        [RegularExpression(Consts.PasswordReqex, ErrorMessage = Consts.PasswordValidationError)]
        public string Password { get; set; }
    }
}