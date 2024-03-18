using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userMenager;
        private readonly IConfiguration _configuration;

        public AuthenticationService (UserManager<User> userManager, IConfiguration configuration)
        {
            _userMenager = userManager;
            _configuration = configuration;
        }

        public async Task<string> Register(RegisterDto register)
        {
            var userByEmail = await _userMenager.FindByEmailAsync(register.Email);
            var userByUsername = await _userMenager.FindByNameAsync(register.Username);

            if(userByEmail is not null)
                throw new ArgumentException($"Email {register.Email} is taken");
            else if(userByUsername is not null)
                throw new ArgumentException($"Username {register.Email} is taken");

            User user = new()
            {
                Email = register.Email,
                UserName = register.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userMenager.CreateAsync(user, register.Password);

            if(!result.Succeeded)
                throw new ArgumentException($"Unable to register user {register.Username} errors: {GetErrorsText(result.Errors)}");

            return await Login(new LoginDto {Username = register.Email, Password = register.Password});
        }

        public async Task<string> Login(LoginDto register)
        {
            var user = await _userMenager.FindByNameAsync(register.Username);

            if(user is null)
                user = await _userMenager.FindByEmailAsync(register.Username);
            
            if(user is null)
                throw new ArgumentException("User does not exist");
            else if(!await _userMenager.CheckPasswordAsync(user, register.Password))
                throw new ArgumentException("Incorect password");

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    
        private string GetErrorsText(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}