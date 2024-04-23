using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.DTOs.Transaction;
namespace API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetUserTransactions(QueryObject query, string id)
        {
            var transactions = FilterTransactionsByQuery(_context.Transactions.Where(x => x.AppUserId == id), query);
            return await ApplyPagination(transactions, query).ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTransactions(QueryObject query)
        {
            var transactions = FilterTransactionsByQuery(_context.Transactions.AsQueryable(), query);
            return await ApplyPagination(transactions, query).ToListAsync();
        }

        private IQueryable<Transaction> FilterTransactionsByQuery(IQueryable<Transaction> transactions, QueryObject query)
        {
            /* if (!string.IsNullOrWhiteSpace(query.TransactionDescription))
                 transactions = transactions.Where(x => x.TransactionDescription.Contains(query.TransactionDescription));

             if (!string.IsNullOrWhiteSpace(query.TransactionType))
                 transactions = transactions.Where(x => x.TransactionType.Contains(query.TransactionType));

             if (query.TransactionAmount != 0)
                 transactions = transactions.Where(x => x.TransactionAmount.Equals(query.TransactionAmount));*/

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("transactionType", StringComparison.OrdinalIgnoreCase))
                    transactions = query.IsDecsending ? transactions.OrderByDescending(x => x.TransactionType) : transactions.OrderBy(x => x.TransactionType);

                if (query.SortBy.Equals("transactionDescription", StringComparison.OrdinalIgnoreCase))
                    transactions = query.IsDecsending ? transactions.OrderByDescending(x => x.TransactionDescription) : transactions.OrderBy(x => x.TransactionDescription);

                if (query.SortBy.Equals("transactionDate", StringComparison.OrdinalIgnoreCase))
                    transactions = query.IsDecsending ? transactions.OrderByDescending(x => x.TransactionDate) : transactions.OrderBy(x => x.TransactionDate);
            }

            return transactions;
        }

        private IQueryable<Transaction> ApplyPagination(IQueryable<Transaction> transactions, QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return transactions.Skip(skipNumber).Take(query.PageSize);
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

        public Task<Transaction> UpdateTransaction(Transaction transactionModel)
        {
            throw new NotImplementedException();
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

        public async Task<DasboardDto> GetDashboardValues(string id)
        {
            var firstDayOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            var monthlyProfit = await _context.Transactions
                .Where(e =>e.AppUserId == id && e.TransactionType  == true && e.TransactionDate >= firstDayOfMonth && e.TransactionDate <= lastDayOfMonth).Select(e => (double)e.TransactionAmount).SumAsync();
            var monthlyExpenses = await _context.Transactions
                .Where(e =>e.AppUserId == id && e.TransactionType  == false && e.TransactionDate >= firstDayOfMonth && e.TransactionDate <= lastDayOfMonth).Select(e => (double)e.TransactionAmount).SumAsync();
            var monthlySummary = monthlyProfit - monthlyExpenses;
            
            var totalProfit = await _context.Transactions
                .Where(e =>e.AppUserId == id && e.TransactionType  == true).Select(e => (double)e.TransactionAmount).SumAsync();
            var totalExpenses = await _context.Transactions
                .Where(e =>e.AppUserId == id && e.TransactionType  == false).Select(e => (double)e.TransactionAmount).SumAsync();

            var totalMoneyAmount = totalProfit - totalExpenses;

            var dasboardDto = new DasboardDto
            {
                TotalMoneyAmount = totalMoneyAmount,
                TotalProfit = totalProfit,
                TotalExpenses = totalExpenses,
                MonthlySummary = monthlySummary,
                MonthlyProfit = monthlyProfit,
                MonthlyExpenses = monthlyExpenses
            };

            return dasboardDto;
        }
    }
}