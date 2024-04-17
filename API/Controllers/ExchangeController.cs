using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.DTOs.Exchange;
using API.Interface;
using API.Mappers;
using API.Models;
using API.Helpers;

namespace API.Controllers
{
    [Authorize] // Apply authorization to all actions in this controller
    public class ExchangeController : BaseApiController
    {
        private readonly IExchangeRepository _exchange;
        private readonly UserManager<AppUser> _userManager;

        public ExchangeController(IExchangeRepository exchange, UserManager<AppUser> userManager)
        {
            _exchange = exchange;
            _userManager = userManager;
        }

        // Get all exchanges
        [HttpGet("Get")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetAllExchanges([FromQuery] QueryObject query)
        {
            var exchanges = await _exchange.GetAllExchanges(query);
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        // Get exchanges for a specific user
        [HttpGet("GetUserExchanges")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
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
        
        // Get exchange by ID
        [HttpGet("Get/{id:int}")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetExchangeById([FromRoute] int exchangeId)
        {
            var exchange = await _exchange.GetExchangeById(exchangeId);

            if (exchange == null)
                return NotFound();

            return Ok(exchange);
        }

        // Create a new exchange
        [HttpPost("Set")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> SetExchange(SetExchangeDto setExchangeDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _exchange.UserExists(userId))
                return BadRequest(userId);

            var exchangeModel = setExchangeDto.ToExchangeFromSet(userId);
            await _exchange.SetExchange(exchangeModel);

            // Return the newly created exchange
            return CreatedAtAction(nameof(GetExchangeById), new { id = exchangeModel.Id }, exchangeModel.ToExchangeDto());
        }

        // Delete an exchange
        [HttpDelete("Remove")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> RemoveExchange(int id)
        {
            var exchangeModel = await _exchange.DeleteExchange(id);

            if (exchangeModel == null)
                return NotFound("Exchange does not exist");

            return Ok(exchangeModel);
        }
    }
}