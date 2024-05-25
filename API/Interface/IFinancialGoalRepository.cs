using API.Models;

namespace API.Interface
{
    public interface IFinancialGoalRepository
    {
        Task AddFinancialGoalAsync(FinancialGoal financialGoal);
        Task<FinancialGoal> GetFinancialGoalsByUserIdAsync(string userId);
        Task<FinancialGoal> GetFinancialGoalByUserIdAsync(string userId);
        Task UpdateFinancialGoalAsync(FinancialGoal financialGoal);
        Task DeleteFinancialGoalAsync(FinancialGoal financialGoal);
        Task<IEnumerable<FinancialGoal>> GetAllFinancialGoalsAsync();
    }
}