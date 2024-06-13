using API.DTOs.Transaction;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AutomaticTransactionController: BaseApiController
    {
        private readonly IAutomaticTransactionRepository _repository;

        public AutomaticTransactionController(IAutomaticTransactionRepository repository)
        {
            _repository = repository;
        }
/*
        [HttpGet("due")]
        public async Task<ActionResult<IEnumerable<AutomaticTransactionDto>>> GetDueScheduledTransactions()
        {
            var dueTransactions = await _repository.GetDueScheduledTransactionsAsync();
            return Ok(dueTransactions);
        }

        [HttpPost]
        public async Task<ActionResult<AutomaticTransactionDto>> CreateScheduledTransaction(AutomaticTransactionDto dto)
        {
            var scheduledTransaction = dto.ToScheduledTransaction();
            _context.AutomaticTransactions.Add(scheduledTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDueScheduledTransactions), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheduledTransaction(int id, AutomaticTransactionDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateScheduledTransactionAsync(dto);

            return NoContent();
        }*/
    }
}