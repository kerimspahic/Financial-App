using API.Data;
using API.DTOs.Transaction;
using API.Interface;
using API.Mappers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AutomaticTransactionRepository : IAutomaticTransactionRepository
    {
        private readonly AppDbContext _context;

        public AutomaticTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AutomaticTransactionDto>> GetDueScheduledTransactionsAsync()
        {
            var now = DateTime.UtcNow;
            var scheduledTransactions = await _context.AutomaticTransactions
                                                      .Where(st => st.NextExecutionDate <= now)
                                                      .ToListAsync();

            return scheduledTransactions.Select(st => st.ToScheduledTransactionDto());
        }

        public async Task SetTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScheduledTransactionAsync(AutomaticTransactionDto dto)
        {
            var scheduledTransaction = dto.ToScheduledTransaction();
            _context.AutomaticTransactions.Update(scheduledTransaction);
            await _context.SaveChangesAsync();
        }

    }
}