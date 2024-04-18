using API.Data;
using API.DTOs.Admin;
using API.Interface;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class AdminTransactionController : BaseApiController
    {
        private readonly IAdminTransactionRepository _adminTransaction;
        public AdminTransactionController(IAdminTransactionRepository adminTransaction, AppDbContext context)
        {
            _adminTransaction = adminTransaction;

        }

        [HttpPost("SetTransactionDescription")]
        public async Task<IActionResult> SetTransactionDescription(SetDescriptionNameDto transactionDescription)
        {
            var response = transactionDescription.ToExchangeDescriptionsFromSet();
            await _adminTransaction.SetTransactionDescription(response);

            return Ok(response);
        }


        [HttpGet("GetTransactionDescriptions")]
        public async Task<IActionResult> GetTransactionDescription()
        {
            return Ok(await _adminTransaction.GetTransactionDescriptions());
        }

        [HttpPut("UpdateTransactionDescription")]
        public async Task<IActionResult> UpdateTransactionDescription(int id, string  descriptionName)
        {
            

            return Ok(await _adminTransaction.UpdateTransactionDescription(id,descriptionName));


        }

        [HttpDelete("DeleteTransactionDescription")]
        public async Task<IActionResult> DeleteTransactionDescription(int id)
        {
            return Ok(await _adminTransaction.DeleteTransactionDescription(id));
        }
    }
}