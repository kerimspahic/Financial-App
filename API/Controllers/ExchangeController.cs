using API.DTOs.Exchange;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.Models;
using API.Helpers;
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
        public async Task<IActionResult> GetAllExchanges([FromQuery] QueryObject query)
        {
            var exchanges = await _exchange.GetAllExchanges(query);
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("GetUserExchanges")]
        public async Task<IActionResult> GetUserExchanges([FromQuery] QueryObject query)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _exchange.UserExists(userId))
                return BadRequest(userId);

            var exchanges = await _exchange.GetUserExchanges(query, userId);
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetExchangeById([FromRoute] int exchangeId)
        {

            var exchanges = await _exchange.GetExchangeById(exchangeId);

            if (exchanges == null)
                return NotFound();

            return Ok(exchanges);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpPost("Set")]
        public async Task<IActionResult> SetExchange(SetExchangeDto setExchangeDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

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

    }
}