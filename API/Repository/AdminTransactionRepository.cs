using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AdminTransactionRepository : IAdminTransactionRepository
    {
        private readonly AppDbContext _context;
        public AdminTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionDescriptions> DeleteTransactionDescription(int id)
        {
            var transactionDescription = await _context.TransactionDescriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (transactionDescription == null)
                return null;

            _context.TransactionDescriptions.Remove(transactionDescription);
            await _context.SaveChangesAsync();

            return transactionDescription;
        }

        public async Task<List<TransactionDescriptions>> GetTransactionDescriptions()
        {
            return await _context.TransactionDescriptions.ToListAsync();
        }

        public async Task<TransactionDescriptions> SetTransactionDescription(TransactionDescriptions descriptionName)
        {

            await _context.TransactionDescriptions.AddAsync(descriptionName);
            await _context.SaveChangesAsync();
            return descriptionName;
        }

        public async Task<TransactionDescriptions> UpdateTransactionDescription(int id, string descriptionName)
        {
            var transactionName = await _context.TransactionDescriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (transactionName == null)
                return null;

            transactionName.DescriptionName = descriptionName;
            await _context.SaveChangesAsync();

            return transactionName;
        }
    }
}