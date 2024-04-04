using API.DTOs;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _account;
        public AccountController(IAccountRepository account)
        {
            _account = account;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var currentUser = await _account.Register(registerDto);

            if (currentUser == null)
                return NotFound();

            return Ok(currentUser);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = await _account.Login(loginDto);

            if (currentUser == null)
                return NotFound();

            return Ok(currentUser);
        }
    }
}