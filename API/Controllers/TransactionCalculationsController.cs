using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionCalculationsController : BaseApiController
    {
        private readonly ITransactionCalculationsRepository _transactionCalculations;
        private readonly UserManager<AppUser> _userManager;
        public TransactionCalculationsController(ITransactionCalculationsRepository transactionCalculations, UserManager<AppUser> userManager)
        {
            _transactionCalculations = transactionCalculations;
            _userManager = userManager;
        }

        [HttpGet("GetCardValues")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetCardValues([FromQuery] string mode)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            switch (mode.ToLower())
            {
                case "total":
                    return Ok(await _transactionCalculations.GetTotalValuesAsync(userId));
                case "yearly":
                    return Ok(await _transactionCalculations.GetYearlyValuesAsync(userId));
                case "monthly":
                    return Ok(await _transactionCalculations.GetMonthlyValuesAsync(userId));
                default:
                    return BadRequest("Mode Not Found");
            }
        }

        [HttpGet("GetChartValues")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetChartValues([FromQuery] string type)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            switch (type.ToLower())
            {

                case "totalgain":
                    return Ok(await _transactionCalculations.GetTotalGainChartAsync(userId));
                case "totalspent":
                    return Ok(await _transactionCalculations.GetTotalSpentChartAsync(userId));
                case "totalprofit":
                    return Ok(await _transactionCalculations.GetTotalProfitChartAsync(userId));
                case "yearlygain":
                    return Ok(await _transactionCalculations.GetYearlyGainChartAsync(userId));
                case "yearlyspent":
                    return Ok(await _transactionCalculations.GetYearlySpentChartAsync(userId));
                case "yearlyprofit":
                    return Ok(await _transactionCalculations.GetYearlyProfitChartAsync(userId));
                case "monthlygain":
                    return Ok(await _transactionCalculations.GetMonthlyGainChartAsync(userId));
                case "monthlyspent":
                    return Ok(await _transactionCalculations.GetMonthlySpentChartAsync(userId));
                case "monthlyprofit":
                    return Ok(await _transactionCalculations.GetMonthlyProfitChartAsync(userId));
                default:
                    return BadRequest("Invalid chart type specified.");
            }
        }

        [HttpPost("send-weekly-summary")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> SendWeeklySummary()
        {
            var userId = _userManager.GetUserId(User); // Method to get user ID from the JWT or claims
            await _transactionCalculations.SendWeeklySummaryEmail(userId);

            return Ok("Weekly summary email sent successfully.");
        }
    }
}