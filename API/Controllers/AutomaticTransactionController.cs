using API.DTOs.Transaction;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AutomaticTransactionController : BaseApiController
    {
        private readonly IAutomaticTransactionRepository _automaticTransactionRepository;

        public AutomaticTransactionController(IAutomaticTransactionRepository automaticTransactionRepository)
        {
            _automaticTransactionRepository = automaticTransactionRepository;
        }

        [Authorize(Policy = "ElevatedRights")]
        [HttpPost("CreateAutomaticTransaction")]
        public async Task<IActionResult> CreateAutomaticTransaction(CreateAutomaticTransactionDto createDto)
        {
            var transaction = createDto.ToAutomaticTransactions();
            var response = await _automaticTransactionRepository.CreateAutomaticTransaction(transaction);
            return Ok(response);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("GetAutomaticTransactions")]
        public async Task<IActionResult> GetAutomaticTransactions()
        {
            return Ok(await _automaticTransactionRepository.GetAutomaticTransactions());
        }

        [Authorize(Policy = "StandardRights")]
        [HttpGet("GetAutomaticTransactionById")]
        public async Task<IActionResult> GetAutomaticTransactionById(int id)
        {
            return Ok(await _automaticTransactionRepository.GetAutomaticTransactionById(id));
        }

        [Authorize(Policy = "ElevatedRights")]
        [HttpPut("UpdateAutomaticTransaction")]
        public async Task<IActionResult> UpdateAutomaticTransaction(int id, UpdateAutomaticTransactionDto updateDto)
        {
            var transaction = updateDto.ToAutomaticTransactions(id);
            var response = await _automaticTransactionRepository.UpdateAutomaticTransaction(transaction);
            return Ok(response);
        }

        [Authorize(Policy = "StandardRights")]
        [HttpDelete("DeleteAutomaticTransaction")]
        public async Task<IActionResult> DeleteAutomaticTransaction(int id)
        {
            var response = await _automaticTransactionRepository.DeleteAutomaticTransaction(id);
            return Ok(response);
        }
    }
}
