using API.DTOs;

namespace API.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterDto request);
        Task<string> Login(LoginDto request);
    }
}