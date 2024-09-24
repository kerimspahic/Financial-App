using System.ComponentModel.DataAnnotations;
using API.DTOs;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = await _accountRepository.Register(registerDto);
                var token = await _accountRepository.GenerateEmailConfirmationTokenAsync(registerDto.Email);

                if (!new EmailAddressAttribute().IsValid(registerDto.Email))
                {
                    return BadRequest(new { Error = "Invalid email format" });
                }

                await _accountRepository.SendConfirmationEmail(registerDto.Email, token);
                return Ok("Registration successful. Please check your email to confirm your account.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = await _accountRepository.Login(loginDto);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("User ID and token are required");

            var result = await _accountRepository.ConfirmEmailAsync(userId, token);
            if (result.Succeeded)
                return Ok("Email confirmed successfully");

            return BadRequest(result.Errors);
        }
    }
}