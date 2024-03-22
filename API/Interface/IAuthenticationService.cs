using API.DTOs;
using FluentResults;

namespace API.Services
{
    public interface IAuthenticationService
    {
        Task<Result<string>> Register(RegisterDto request);
        Task<Result<string>> Login(LoginDto request);
    }
}