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

        public async Task<AutomaticTransactions> CreateAutomaticTransaction(AutomaticTransactions automaticTransaction)
        {
            await _context.AutomaticTransactions.AddAsync(automaticTransaction);
            await _context.SaveChangesAsync();
            return automaticTransaction;
        }

        public async Task<List<AutomaticTransactions>> GetAutomaticTransactions()
        {
            return await _context.AutomaticTransactions.ToListAsync();
        }

        public async Task<AutomaticTransactions> GetAutomaticTransactionById(int id)
        {
            return await _context.AutomaticTransactions.FindAsync(id);
        }

        public async Task<AutomaticTransactions> UpdateAutomaticTransaction(AutomaticTransactions automaticTransaction)
        {
            var existingTransaction = await _context.AutomaticTransactions.FindAsync(automaticTransaction.Id);
            if (existingTransaction == null)
                return null;

            existingTransaction.TransactionAmount = automaticTransaction.TransactionAmount;
            existingTransaction.TransactionType = automaticTransaction.TransactionType;
            existingTransaction.TransactionDescription = automaticTransaction.TransactionDescription;
            existingTransaction.TransactionDate = automaticTransaction.TransactionDate;
            existingTransaction.Frequency = automaticTransaction.Frequency;
            existingTransaction.NextExecutionDate = automaticTransaction.NextExecutionDate;

            await _context.SaveChangesAsync();
            return existingTransaction;
        }

        public async Task<AutomaticTransactions> DeleteAutomaticTransaction(int id)
        {
            var automaticTransaction = await _context.AutomaticTransactions.FindAsync(id);
            if (automaticTransaction == null)
                return null;

            _context.AutomaticTransactions.Remove(automaticTransaction);
            await _context.SaveChangesAsync();
            return automaticTransaction;
        }
    }
}