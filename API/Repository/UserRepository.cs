using API.Data;
using API.DTOs.Account;
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
            return await _context.Users.Include(x => x.Exchanges).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AppUser>> GetAppUsers()
        {
            return await _context.Users.Include(x => x.Exchanges).ToListAsync();
        }

        public async Task<AppUser> UpdateAppUserFirstName(string id, string firstName)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (currentUser == null)
                return null;

            currentUser.FirstName = firstName;
            await _context.SaveChangesAsync();

            return (currentUser);
        }

        public async Task<AppUser> UpdateAppUserLastName(string id, string lastName)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (currentUser == null)
                return null;

            currentUser.LastName = lastName;
            await _context.SaveChangesAsync();

            return (currentUser);
        }

        public async Task<IdentityResult> UpdateAppUserPassword(string id, UpdatePasswordDto passwordDto)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return await _userManager.ChangePasswordAsync(currentUser, passwordDto.OldPassword, passwordDto.NewPassword);
        }

        
    }
}