using API.DTOs.Account;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetAppUser/{id}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUser([FromRoute] string id)
        {
            var response = await _userRepository.GetAppUserById(id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetAppUsers")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUsers()
        {
            var response = await _userRepository.GetAppUsers();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetUserId")]
        public async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user.Id;//move to reposetory
        }

        [Authorize]
        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UpdateAccountDto updateAccountDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            var result = await _userRepository.UpdateAppUserInfo(userId, updateAccountDto);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdatePasswordDto passwordDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            var result = await _userRepository.UpdateAppUserPassword(userId, passwordDto);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail(string newEmail)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            var result = await _userRepository.UpdateAppUserEmail(userId, newEmail);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string id)
        {
            var currentUser = await _userRepository.DeleteAppUser(id);

            if (currentUser == null)
                return NotFound();

            return Ok(currentUser);
        }

    }
}