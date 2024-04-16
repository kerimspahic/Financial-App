using API.DTOs;
using API.DTOs.Account;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task<string> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
    }
}