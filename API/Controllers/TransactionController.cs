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
        [HttpGet("GetAllTransactions")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetAllTransactions([FromQuery] QueryObject query)
        {
            // Retrieve all transactions
            var transactions = await _transaction.GetAllTransactions(query);
            // Map transactions to DTOs
            var transactionDto = transactions.Select(s => s.ToTransactionDto());
            return Ok(transactionDto);
        }

        // Get transactions for a specific user
        [HttpGet("GetUserTransactions")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetUserTransactions([FromQuery] QueryObject query)
        {
            // Retrieve current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            // Check if user exists
            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            // Retrieve user transactions
            var transactions = await _transaction.GetUserTransactions(query, userId);
            // Map transactions to DTOs
            var transactionDto = transactions.Select(s => s.ToTransactionDto());
            return Ok(transactionDto);
        }

        // Get transaction by ID
        [HttpGet("Get/{id:int}")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> GetTransactionById([FromRoute] int transactionId)
        {
            // Retrieve transaction by ID
            var transaction = await _transaction.GetTransactionById(transactionId);

            // Check if transaction exists
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // Create a new transaction
        [HttpPost("SetNewTransaction")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> SetTransaction(SetTransactionDto setTransactionDto)
        {
            // Retrieve current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            // Check if user exists
            if (!await _transaction.UserExists(userId))
                return BadRequest(userId);

            // Map DTO to transaction model
            var transactionModel = setTransactionDto.ToTransactionFromSet(userId);
            // Save transaction
            await _transaction.SetTransaction(transactionModel);

            // Return the newly created transaction
            return CreatedAtAction(nameof(GetTransactionById), new { id = transactionModel.Id }, transactionModel.ToTransactionDto());
        }

        // Delete a transaction
        [HttpDelete("Remove")]
        [Authorize(Policy = "StandardRights")] // Apply authorization policy
        public async Task<IActionResult> RemoveTransaction(int id)
        {
            // Delete transaction by ID
            var transactionModel = await _transaction.DeleteTransaction(id);

            // Check if transaction exists
            if (transactionModel == null)
                return NotFound("Transaction does not exist");

            return Ok(transactionModel);
        }

        // Get dashboard values
        [HttpGet("GetDashboardValues")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetDashboardValues()
        {
            // Retrieve current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            // Retrieve dashboard values
            var transactionModel = await _transaction.GetDashboardValues(userId);

            return Ok(transactionModel);
        }

        // Get values for dashboard chart
        [HttpGet("GetDashboardChartValues")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetDashboardChartValues()
        {
            // Retrieve current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;

            // Retrieve dashboard charts
            var transactionModel = await _transaction.GetDashboardChartValues(userId);

            return Ok(transactionModel);
        }
    }
}
