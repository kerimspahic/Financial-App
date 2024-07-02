using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.DTOs.Transaction;
using API.Interface;
using API.Mappers;
using API.Models;
using API.Helpers;
using API.DTOs;

namespace API.Controllers
{
    [Authorize]
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionRepository _transaction;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(ITransactionRepository transaction, UserManager<AppUser> userManager)
        {
            _transaction = transaction;
            _userManager = userManager;
        }

        [HttpGet("GetUserTransactions")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetUserTransactions([FromQuery] QueryObject query)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            var page = await _transaction.GetUserTransactions(query, userId);

            var totalCount = page.Total;
            var totalPages = Math.Ceiling((double)totalCount / query.PageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("Get/{id:int}")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int transactionId)
        {
            var transaction = await _transaction.GetTransactionById(transactionId);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost("SetNewTransaction")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> SetTransaction(SetTransactionDto setTransactionDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            var transactionModel = setTransactionDto.ToTransactionFromSet(userId);
            await _transaction.SetTransaction(transactionModel);

            return CreatedAtAction(nameof(GetTransactionById), new { id = transactionModel.Id }, transactionModel.ToTransactionDto());
        }

        [HttpDelete("Remove")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            var transactionModel = await _transaction.DeleteTransaction(id);

            if (transactionModel == null)
                return NotFound("Transaction does not exist");

            return Ok(transactionModel);
        }

        [HttpPut("UpdateTransaction")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> UpdateTransaction(UpdateTransactionDto updateTransactionDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _transaction.UserExists(userId))
                return BadRequest("User does not exist");

            var updatedTransaction = await _transaction.UpdateTransaction(updateTransactionDto, userId);

            if (updatedTransaction == null)
                return NotFound("Transaction not found or you don't have rights to update this transaction");

            return Ok(updatedTransaction.ToTransactionDto());
        }

    }
}
