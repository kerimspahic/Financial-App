using API.DTOs;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IAuthenticationService _auth;

        public UserController(IAuthenticationService auth)
        {
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]

        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = await _auth.Login(login);
            var responseDto = response.ToResultDto();

            if (!responseDto.IsSucces)
            {
                return BadRequest(responseDto);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var response = await _auth.Register(register);
            var responseDto = response.ToResultDto();

            if (!responseDto.IsSucces)
            {
                return BadRequest(responseDto);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<object> CurrentUser()
        {
            string userName = User.Identity.Name;
            return new { userName };
        }
    }
}