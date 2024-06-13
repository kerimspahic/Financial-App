using API.DTOs.FinancialGoal;
using API.Interface;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FinancialGoalsController : BaseApiController
    {
        private readonly IFinancialGoalRepository _financialGoalRepository;
        private readonly UserManager<AppUser> _userManager;

        public FinancialGoalsController(IFinancialGoalRepository financialGoalRepository, UserManager<AppUser> userManager)
        {
            _financialGoalRepository = financialGoalRepository;
            _userManager = userManager;
        }

        [HttpPost("CreateFinancialGoal")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> CreateFinancialGoal([FromBody] FinancialGoalDto financialGoalDto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingGoal = await _financialGoalRepository.GetFinancialGoalByUserIdAsync(userId);
            if (existingGoal != null)
            {
                existingGoal.UpdateFinancialGoalFromDto(financialGoalDto);
                await _financialGoalRepository.UpdateFinancialGoalAsync(existingGoal);

                return Ok(existingGoal.ToFinancialGoalDto());
            }
            else
            {
                var financialGoal = financialGoalDto.ToFinancialGoal(userId);
                await _financialGoalRepository.AddFinancialGoalAsync(financialGoal);

                return Ok(financialGoal.ToFinancialGoalDto());
            }
        }

        [HttpGet("GetFinancialGoals")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetFinancialGoals([FromQuery] string mode = "all")
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var financialGoals = await _financialGoalRepository.GetFinancialGoalsByUserIdAsync(userId);
            var financialGoalsDto = financialGoals.ToFinancialGoalDto();

            switch (mode.ToLower())
            {
                case "yearly":
                    return Ok(financialGoalsDto.ToYearlyFinancialGoalDto());

                case "monthly":
                    return Ok(financialGoalsDto.ToMonthlyFinancialGoalDto());

                default:
                    return Ok(financialGoals.ToFinancialGoalDto());
            }
        }
        [HttpGet("GetSpecificFinancialGoal")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> GetFinancialGoal([FromQuery] string column)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var financialGoals = await _financialGoalRepository.GetFinancialGoalsByUserIdAsync(userId);

            switch (column.ToLower())
            {
                case "totalprofitgoal":
                    return Ok(financialGoals.TotalProfit);

                case "yearlyprofitgoal":
                    return Ok(financialGoals.YearlyProfitGoal);

                case "yearlygaingoal":
                    return Ok(financialGoals.YearlyGainGoal);

                case "yearlyspentlimit":
                    return Ok(financialGoals.YearlySpentLimit);

                case "monthlyprofitgoal":
                    return Ok(financialGoals.MonthlyProfitGoal);

                case "monthlygaingoal":
                    return Ok(financialGoals.MonthlyGainGoal);

                case "monthlyspentlimit":
                    return Ok(financialGoals.MonthlySpentLimit);

                default:
                    return BadRequest("Invalid column name.");
            }
        }

        [HttpPatch("UpdateFinancialGoalColumn")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> UpdateFinancialGoalColumn(string column, double newValue)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var financialGoal = await _financialGoalRepository.GetFinancialGoalByUserIdAsync(userId);
            if (financialGoal == null)
            {
                return NotFound();
            }

            switch (column.ToLower())
            {
                case "totalprofitgoal":
                    financialGoal.TotalProfit = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "yearlyprofitgoal":
                    financialGoal.YearlyProfitGoal = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "yearlygaingoal":
                    financialGoal.YearlyGainGoal = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "yearlyspentlimit":
                    financialGoal.YearlySpentLimit = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "monthlyprofitgoal":
                    financialGoal.MonthlyProfitGoal = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "monthlygaingoal":
                    financialGoal.MonthlyGainGoal = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                case "monthlyspentlimit":
                    financialGoal.MonthlySpentLimit = newValue;
                    financialGoal.DateEdited = DateTime.Now;
                    break;

                default:
                    return BadRequest("Invalid column name.");
            }

            await _financialGoalRepository.UpdateFinancialGoalAsync(financialGoal);
            return Ok("Financial goal updated successfully.");
        }

        [HttpDelete("DeleteFinancialGoal")]
        [Authorize(Policy = "StandardRights")]
        public async Task<IActionResult> DeleteFinancialGoal()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var financialGoal = await _financialGoalRepository.GetFinancialGoalByUserIdAsync(userId);
            if (financialGoal == null || financialGoal.UserId != userId)
            {
                return NotFound("Financial goal not found.");
            }

            await _financialGoalRepository.DeleteFinancialGoalAsync(financialGoal);
            return Ok("Financial goal deleted successfully.");
        }

        [HttpGet("GetAllUserFinancialGoals")]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<IActionResult> GetAllUserFinancialGoals()
        {
            var financialGoals = await _financialGoalRepository.GetAllFinancialGoalsAsync();
            var financialGoalsWithoutDateCreatedDto = financialGoals.Select(goal => goal.ToFinancialGoalWithoutDateCreatedDto()).ToList();
            return Ok(financialGoalsWithoutDateCreatedDto);
        }
    }
}