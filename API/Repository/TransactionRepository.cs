using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.Response;
using API.DTOs;

namespace API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<TransactionDto>> GetUserTransactions(QueryObject query, string id)
        {
            var data = FilterTransactionsByQuery(_context.Transactions.Where(x => x.AppUserId == id)
                    .Select(x => new TransactionDto
                    {
                        Id = x.Id,
                        TransactionAmount = x.TransactionAmount,
                        TransactionType = x.TransactionType,
                        TransactionDescription = x.TransactionDescription,
                        TransactionDate = x.TransactionDate,
                        AppUserId = x.AppUserId
                    }), query);

            var page = new PaginatedResponse<TransactionDto>(data, query.PageNumber, query.PageSize);

            return page;
        }

        private IQueryable<TransactionDto> FilterTransactionsByQuery(IQueryable<TransactionDto> transactions, QueryObject query)
        {
            if (query.TransactionDescription != null)
                transactions = transactions.Where(x => x.TransactionDescription == query.TransactionDescription);

            if (query.TransactionType != null)
                transactions = transactions.Where(x => x.TransactionType == query.TransactionType);

            if (query.TransactionAmount != 0)
                transactions = transactions.Where(x => x.TransactionAmount.Equals(query.TransactionAmount));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy)
                {
                    case "transactionAmount":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.TransactionAmount) : transactions.OrderBy(x => x.TransactionAmount);
                        break;
                    case "transactionType":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.TransactionType) : transactions.OrderBy(x => x.TransactionType);
                        break;
                    case "transactionDescription":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.TransactionDescription) : transactions.OrderBy(x => x.TransactionDescription);
                        break;
                    case "transactionDate":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.TransactionDate) : transactions.OrderBy(x => x.TransactionDate);
                        break;
                }
            }

            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Transaction> SetTransaction(Transaction transactionModel)
        {
            await _context.Transactions.AddAsync(transactionModel);
            await _context.SaveChangesAsync();
            return transactionModel;
        }

 public async Task<Transaction> UpdateTransaction(UpdateTransactionDto updateTransactionDto, string userId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(x => x.Id == updateTransactionDto.Id && x.AppUserId == userId);

            if (transaction == null)
                return null;

            if (updateTransactionDto.TransactionAmount.HasValue)
                transaction.TransactionAmount = updateTransactionDto.TransactionAmount.Value;

            if (updateTransactionDto.TransactionType.HasValue)
                transaction.TransactionType = updateTransactionDto.TransactionType.Value;

            if (updateTransactionDto.TransactionDescription.HasValue)
                transaction.TransactionDescription = updateTransactionDto.TransactionDescription.Value;

            if (updateTransactionDto.TransactionDate.HasValue)
                transaction.TransactionDate = updateTransactionDto.TransactionDate.Value;

            await _context.SaveChangesAsync();

            return transaction;
        }
        public async Task<Transaction> DeleteTransaction(int id)
        {
            var transactionModel = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);

            if (transactionModel == null)
                return null;

            _context.Transactions.Remove(transactionModel);
            await _context.SaveChangesAsync();

            return transactionModel;
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(s => s.Id == id);
        }
    }
}