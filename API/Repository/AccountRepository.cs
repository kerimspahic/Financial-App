using API.DTOs;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _tokenService;

        public AccountRepository(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _tokenService = tokenService;
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            try
            {
                var existingUserByUsername = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUserByUsername is not null)
                    throw new ArgumentException($"Username {registerDto.Username} is taken");

                var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUserByEmail is not null)
                    throw new ArgumentException($"Email {registerDto.Email} is taken");

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (!createdUser.Succeeded)
                    throw new ArgumentException($"Unable to register user {registerDto.Username} errors: {GetErrorsText(createdUser.Errors)}");

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                    throw new ArgumentException("Unable to assign role to user");

                return _tokenService.CreateToken(appUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (appUser == null)
                return null;

            var result = await _signinManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
            if (!result.Succeeded)
                return null;

            return _tokenService.CreateToken(appUser);

        }
        private string GetErrorsText(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}