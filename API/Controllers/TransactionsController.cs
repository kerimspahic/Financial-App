using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class TransactionsController : BaseApiController
    {

        private readonly ITransactionService _tran;

        public TransactionsController (ITransactionService tran)
        {
            _tran = tran;
        }
        
        [AllowAnonymous]
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] TransactionDto transaction)
        {
            var response = await _tran.Insert(transaction);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("extract")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> Extract()
        {
            var userName = User.Identity.Name;
            var response = await _tran.Extract(userName);
            return Ok(response);
        }


    }
}