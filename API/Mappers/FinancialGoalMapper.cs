using API.DTOs.FinancialGoal;
using API.Models;

namespace API.Mappers
{
    public static class FinancialGoalMapper
    {
        public static FinancialGoalDto ToFinancialGoalDto(this FinancialGoal financialGoal)
        {
            return new FinancialGoalDto
            {
                YearlyProfitGoal = financialGoal.YearlyProfitGoal,
                YearlyGainGoal = financialGoal.YearlyGainGoal,
                YearlySpentLimit = financialGoal.YearlySpentLimit,
                MonthlyProfitGoal = financialGoal.MonthlyProfitGoal,
                MonthlyGainGoal = financialGoal.MonthlyGainGoal,
                MonthlySpentLimit = financialGoal.MonthlySpentLimit
            };
        }

        public static FinancialGoal ToFinancialGoal(this FinancialGoalDto financialGoalDto, string userId)
        {
            return new FinancialGoal
            {
                UserId = userId,
                YearlyProfitGoal = financialGoalDto.YearlyProfitGoal,
                YearlyGainGoal = financialGoalDto.YearlyGainGoal,
                YearlySpentLimit = financialGoalDto.YearlySpentLimit,
                MonthlyProfitGoal = financialGoalDto.MonthlyProfitGoal,
                MonthlyGainGoal = financialGoalDto.MonthlyGainGoal,
                MonthlySpentLimit = financialGoalDto.MonthlySpentLimit
            };
        }

        public static void UpdateFinancialGoalFromDto(this FinancialGoal existingGoal, FinancialGoalDto financialGoalDto)
        {
            existingGoal.YearlyProfitGoal = financialGoalDto.YearlyProfitGoal;
            existingGoal.YearlyGainGoal = financialGoalDto.YearlyGainGoal;
            existingGoal.YearlySpentLimit = financialGoalDto.YearlySpentLimit;
            existingGoal.MonthlyProfitGoal = financialGoalDto.MonthlyProfitGoal;
            existingGoal.MonthlyGainGoal = financialGoalDto.MonthlyGainGoal;
            existingGoal.MonthlySpentLimit = financialGoalDto.MonthlySpentLimit;
            existingGoal.DateEdited = DateTime.UtcNow;
        }

        public static GetFinancialGoalsDto ToYearlyFinancialGoalDto(this FinancialGoalDto financialGoalsDto)
        {
            return new GetFinancialGoalsDto
            {
                ProfitGoal = financialGoalsDto.YearlyProfitGoal,
                GainGoal = financialGoalsDto.YearlyGainGoal,
                SpentLimit = financialGoalsDto.YearlySpentLimit
            };
        }

        public static GetFinancialGoalsDto ToMonthlyFinancialGoalDto(this FinancialGoalDto financialGoalsDto)
        {
            return new GetFinancialGoalsDto
            {
                ProfitGoal = financialGoalsDto.MonthlyProfitGoal,
                GainGoal = financialGoalsDto.MonthlyGainGoal,
                SpentLimit = financialGoalsDto.MonthlySpentLimit
            };
        }
        public static FinancialGoalAdminDto ToFinancialGoalWithoutDateCreatedDto(this FinancialGoal financialGoal)
        {
            return new FinancialGoalAdminDto
            {
                Id = financialGoal.Id,
                UserId = financialGoal.UserId,
                YearlyProfitGoal = financialGoal.YearlyProfitGoal,
                YearlyGainGoal = financialGoal.YearlyGainGoal,
                YearlySpentLimit = financialGoal.YearlySpentLimit,
                MonthlyProfitGoal = financialGoal.MonthlyProfitGoal,
                MonthlyGainGoal = financialGoal.MonthlyGainGoal,
                MonthlySpentLimit = financialGoal.MonthlySpentLimit,
                DateEdited = financialGoal.DateEdited
            };
        }
    }
}