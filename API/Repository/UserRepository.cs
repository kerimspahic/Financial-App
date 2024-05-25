using System.ComponentModel.DataAnnotations;
using API.Data;
using API.DTOs.Account;
using API.Helpers;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<AppUser> DeleteAppUser(string id)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (currentUser == null)
                return null;

            _context.Users.Remove(currentUser);
            await _context.SaveChangesAsync();

            return (currentUser);
        }

        public async Task<AppUser> GetAppUserById(string id)
        {
            return await _context.Users.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AppUser>> GetAppUsers()
        {
            return await _context.Users.Include(x => x.Transactions).ToListAsync();
        }


        public async Task<AppUser> UpdateAppUserInfo(string userId, UpdateAccountDto updateAccountDto)
        {
            var validationResults = DTOValidator.ValidateDTO(updateAccountDto);
            if (validationResults.Any())
                return null;

            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
                return null;

            if (currentUser.UserName != updateAccountDto.Username)
            {
                var existingUserWithUsername = await _context.Users.FirstOrDefaultAsync(x => x.UserName == updateAccountDto.Username && x.Id != userId);
                if (existingUserWithUsername != null)
                    return null;
                currentUser.UserName = updateAccountDto.Username;
                currentUser.NormalizedUserName = updateAccountDto.Username.ToUpper();
            }

            if (currentUser.FirstName != updateAccountDto.FirstName)
                currentUser.FirstName = updateAccountDto.FirstName;

            if (currentUser.LastName != updateAccountDto.LastName)
                currentUser.LastName = updateAccountDto.LastName;

            await _context.SaveChangesAsync();

            return (currentUser);
        }
        public async Task<IdentityResult> UpdateAppUserPassword(string userId, UpdatePasswordDto passwordDto)
        {
            var validationResults = DTOValidator.ValidateDTO(passwordDto);
            if (validationResults.Any())
                return null;

            var currentUser = await _userManager.FindByIdAsync(userId);

            return await _userManager.ChangePasswordAsync(currentUser, passwordDto.OldPassword, passwordDto.NewPassword);
        }
        public async Task<IdentityResult> UpdateAppUserEmail(string userId, string newEmail)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
                return null;

            var token = await _userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);

            return await _userManager.ChangeEmailAsync(currentUser, newEmail, token);
        }
    }
}