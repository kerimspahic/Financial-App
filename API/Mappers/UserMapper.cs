using API.DTOs.Account;
using API.Models;

namespace API.Mappers
{
    public static class UserMapper
    {
        public static CurrentUserDto ToUserDto(this AppUser appUser)
        {
            return new CurrentUserDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                Transactions = appUser.Transactions.Select(s => s.ToTransactionDto()).ToList()
            };
        }
    }
}