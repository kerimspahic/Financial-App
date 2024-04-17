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
		    return user.Id;
        }
        
        [Authorize]
        [HttpPut("UpdateFirstName/{id}")]
        public async Task<IActionResult> UpdateFirstName([FromRoute] string id, [FromBody] string firstName)
        {
            if (firstName == null)
                return BadRequest();

            var currentUser = await _userRepository.UpdateAppUserFirstName(id, firstName);
            return Ok(currentUser);
        }

        [Authorize]
        [HttpPut("UpdateLastName/{id}")]
        public async Task<IActionResult> UpdateLastName([FromRoute] string id, [FromBody] string lastName)
        {
            if (lastName == null)
                return BadRequest();

            var currentUser = await _userRepository.UpdateAppUserLastName(id, lastName);
            return Ok(currentUser);
        }

        [Authorize]
        [HttpPut("UpdatePassword/{id}")]
        public async Task<IActionResult> UpdatePassword([FromRoute] string id, [FromBody] UpdatePasswordDto passwordDto)
        {
            var result = await _userRepository.UpdateAppUserPassword(id, passwordDto);

            if (!result.Succeeded)
                return BadRequest();

            return Ok();
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