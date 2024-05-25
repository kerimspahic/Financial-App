using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.DTOs.Transaction;
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
        /*
        public async Task<List<Transaction>> GetAllTransactions(QueryObject query)
        {
            var transactions = FilterTransactionsByQuery(_context.Transactions.AsQueryable(), query);
            return await ApplyPagination(transactions, query).ToListAsync();
        }
        */

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



        /*
public async Task<DashboardChartsDto> GetDashboardChartValues(string id)
{
  var transactions = _context.Transactions.Where(e => e.AppUserId == id);


  var transactionDeposits = await transactions.Where(e => e.AppUserId == id && e.TransactionType == true)
      .Select(e => (double)e.TransactionAmount).ToArrayAsync();

  var transactionWithdrawals = await transactions.Where(e => e.AppUserId == id && !e.TransactionType)
      .Select(e => (double)e.TransactionAmount).ToArrayAsync();

  var allTransactions = await transactions.Where(e => e.AppUserId == id)
      .Select(e => (double)(e.TransactionType ? e.TransactionAmount : -e.TransactionAmount)).ToArrayAsync();

  /*
  foreach(var item in transactions.Where(e => e.AppUserId == id))
  {

  }//ddd
  var dashboardChartsDto = new DashboardChartsDto
  {
      TransactionDeposits = transactionDeposits,
      TransactionWithdrawals = transactionWithdrawals,
      AllTransactions = allTransactions
  };

  return dashboardChartsDto;
}*/
    }
}