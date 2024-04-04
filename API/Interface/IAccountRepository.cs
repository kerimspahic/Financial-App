using API.DTOs;
using API.DTOs.Account;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task<CurrentUserDto> Register(RegisterDto registerDto);
        Task<CurrentUserDto> Login(LoginDto loginDto);
    }
}