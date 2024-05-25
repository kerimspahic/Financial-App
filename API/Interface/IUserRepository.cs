using API.DTOs.Account;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAppUsers();
        Task<AppUser> GetAppUserById(string id);
        Task<AppUser> UpdateAppUserInfo(string userId, UpdateAccountDto updateAccountDto);
        Task<IdentityResult> UpdateAppUserPassword(string id, UpdatePasswordDto passwordDto);
        Task<IdentityResult> UpdateAppUserEmail(string userId, string newEmail);
        Task<AppUser> DeleteAppUser(string id);

    }
}