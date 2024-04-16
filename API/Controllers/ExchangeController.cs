using API.DTOs.Exchange;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.Data;
using API.Models;
namespace API.Controllers
{
    public class ExchangeController : BaseApiController
    {
        private readonly IExchangeRepository _exchange;

        private readonly UserManager<AppUser> _userManager;

        public ExchangeController(IExchangeRepository exchange, UserManager<AppUser> userManager)
        {
            _exchange = exchange;
            _userManager = userManager;
        }
        [Authorize(Policy = "StandardRights")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetAllExchanges()
        {
            var exchanges = await _exchange.GetAllExchanges();
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("GetUserExchanges")]
        public async Task<IActionResult> GetUserExchanges()
        {
            string userId = await GetCurrentUserId();
            if (!await _exchange.UserExists(userId))
                return BadRequest(userId);

            var exchanges = await _exchange.GetUserExchanges(userId);
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetExchangeById([FromRoute] int id)
        {
            var exchanges = await _exchange.GetExchangeById(id);

            if (exchanges == null)
                return NotFound();

            return Ok(exchanges);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpPost("Set")]
        public async Task<IActionResult> SetExchange(SetExchangeDto setExchangeDto)
        {
            string userId = await GetCurrentUserId();
            if (!await _exchange.UserExists(userId))
                return BadRequest(userId);

            var exchangeModel = setExchangeDto.ToExchangeFromSet(userId);
            await _exchange.SetExchange(exchangeModel);

            return CreatedAtAction(nameof(GetExchangeById), new { id = exchangeModel.Id }, exchangeModel.ToExchangeDto());
        }

        [Authorize]
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveExchange(int id)
        {
            var exchangeModel = await _exchange.DeleteExchange(id);

            if (exchangeModel == null)
                return NotFound("Exchange does not exist");

            return Ok(exchangeModel);
        }

        private async Task<string> GetCurrentUserId()
        {
            AppUser userId = await GetCurrentUserAsync();
            return userId.Id;
        }

        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}