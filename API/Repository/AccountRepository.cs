using API.DTOs;
using API.Interface;
using API.Models;
using API.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AccountRepository(UserManager<AppUser> userManager, ITokenService tokenService, IConfiguration configuration, SignInManager<AppUser> signinManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            try
            {
                var existingUserByUsername = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUserByUsername != null)
                    throw new ArgumentException($"Username {registerDto.Username} is taken");

                var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUserByEmail != null)
                    throw new ArgumentException($"Email {registerDto.Email} is taken");

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email
                };

                var result = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (!result.Succeeded)
                    throw new ArgumentException($"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                    throw new ArgumentException("Failed to assign role");

                return appUser.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ArgumentException("User not found");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task SendConfirmationEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ArgumentException("User not found");

            var confirmationLink = $"https://localhost:5001/Account/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var message = $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";

            await _emailService.SendEmailAsync(email, "Confirm your email", message);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (appUser == null)
                throw new UnauthorizedAccessException("Invalid username or password");

            var result = await _signinManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid username or password");

            return _tokenService.CreateToken(appUser);

        }

        
    }
}