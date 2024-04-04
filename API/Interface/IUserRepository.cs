using API.DTOs.Account;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<AppUser>> GetAppUsers();
        Task<AppUser> GetAppUserById(string id);
        Task<AppUser> UpdateAppUserFirstName(string id, string firstName);
        Task<AppUser> UpdateAppUserLastName(string id, string lastName);
        Task<IdentityResult> UpdateAppUserPassword(string id, UpdatePasswordDto passwordDto);
        Task<AppUser> DeleteAppUser(string id);
    }
}