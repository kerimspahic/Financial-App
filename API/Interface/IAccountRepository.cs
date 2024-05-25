using API.DTOs;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task<string> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task SendConfirmationEmail(string email, string token);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}