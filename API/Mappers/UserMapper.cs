using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Exchanges = appUser.Exchanges.Select(s => s.ToExchangeDto()).ToList()
            };
        }
    }
}