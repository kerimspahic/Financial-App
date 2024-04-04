using API.DTOs.Exchange;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ExchangeController : BaseApiController
    {
        private readonly IExchangeRepository _exchange;

        public ExchangeController(IExchangeRepository exchange)
        {
            _exchange = exchange;
        }

        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> GetExchanges()
        {
            var exchanges = await _exchange.GetAllExchanges();
            var exchangeDto = exchanges.Select(s => s.ToExchangeDto());

            return Ok(exchangeDto);
        }

        [Authorize]
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetExchangeById([FromRoute] int id)
        {
            var exchanges = await _exchange.GetExchangeById(id);

            if (exchanges == null)
                return NotFound();

            return Ok(exchanges);
        }

        [Authorize]
        [HttpPost("Set")]
        public async Task<IActionResult> SetExchange(string userId, SetExchangeDto setExchangeDto)
        {
            if (!await _exchange.UserExists(userId))
                return BadRequest("User does not exist");

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