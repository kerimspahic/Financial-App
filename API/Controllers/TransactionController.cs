using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.DTOs.Transaction;
using API.Interface;
using API.Mappers;
using API.Models;
using API.Helpers;

namespace API.Controllers
{
    [Authorize] // Apply authorization to all actions in this controller
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionRepository _transaction;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(ITransactionRepository transaction, UserManager<AppUser> userManager)
        {
            _transaction = transaction;
            _userManager = userManager;
        }

        // Get all transactions
        [HttpGet("Get")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetAllTransactions([FromQuery] QueryObject query)
        {
            var transactions = await _transaction.GetAllTransactions(query);
            var transactionDto = transactions.Select(s => s.ToTransactionDto());

            return Ok(transactionDto);
        }

        // Get transactions for a specific user
        [HttpGet("GetUserTransactions")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetUserTransactions([FromQuery] QueryObject query)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            var transactions = await _transaction.GetUserTransactions(query, userId);
            var transactionDto = transactions.Select(s => s.ToTransactionDto());

            return Ok(transactionDto);
        }
        
        // Get transaction by ID
        [HttpGet("Get/{id:int}")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetTransactionById([FromRoute] int transactionId)
        {
            var transaction = await _transaction.GetTransactionById(transactionId);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // Create a new transaction
        [HttpPost("Set")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> SetTransaction(SetTransactionDto setTransactionDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            var transactionModel = setTransactionDto.ToTransactionFromSet(userId);
            await _transaction.SetTransaction(transactionModel);

            // Return the newly created transaction
            return CreatedAtAction(nameof(GetTransactionById), new { id = transactionModel.Id }, transactionModel.ToTransactionDto());
        }

        // Delete an transaction
        [HttpDelete("Remove")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            var transactionModel = await _transaction.DeleteTransaction(id);

            if (transactionModel == null)
                return NotFound("Transaction does not exist");

            return Ok(transactionModel);
        }

        [HttpGet("GetDashboardValues")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetDashboardValues()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            var transactionModel = await _transaction.GetDashboardValues(userId);

            return Ok(transactionModel);
        }
    }
}