using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class FinancialGoalRepository : IFinancialGoalRepository
    {
        private readonly AppDbContext _context;

        public FinancialGoalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFinancialGoalAsync(FinancialGoal financialGoal)
        {
            _context.FinancialGoals.Add(financialGoal);
            await _context.SaveChangesAsync();
        }

        public async Task<FinancialGoal> GetFinancialGoalsByUserIdAsync(string userId)
        {
            return await _context.FinancialGoals.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<FinancialGoal> GetFinancialGoalByUserIdAsync(string userId)
        {
            return await _context.FinancialGoals.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task UpdateFinancialGoalAsync(FinancialGoal financialGoal)
        {
            _context.FinancialGoals.Update(financialGoal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFinancialGoalAsync(FinancialGoal financialGoal)
        {
            _context.FinancialGoals.Remove(financialGoal);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FinancialGoal>> GetAllFinancialGoalsAsync()
        {
            return await _context.FinancialGoals.ToListAsync();
        }
    }
}