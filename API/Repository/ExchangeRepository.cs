using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.DTOs.Exchange;
namespace API.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly AppDbContext _context;
        public ExchangeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Exchange>> GetUserExchanges(QueryObject query, string id)
        {
            var exchanges = FilterExchangesByQuery(_context.Exchanges.Where(x => x.AppUserId == id), query);
            return await ApplyPagination(exchanges, query).ToListAsync();
        }

        public async Task<List<Exchange>> GetAllExchanges(QueryObject query)
        {
            var exchanges = FilterExchangesByQuery(_context.Exchanges.AsQueryable(), query);
            return await ApplyPagination(exchanges, query).ToListAsync();
        }

        private IQueryable<Exchange> FilterExchangesByQuery(IQueryable<Exchange> exchanges, QueryObject query)
        {
            /* if (!string.IsNullOrWhiteSpace(query.TransactionDescription))
                 exchanges = exchanges.Where(x => x.ExchangeDescription.Contains(query.TransactionDescription));

             if (!string.IsNullOrWhiteSpace(query.TransactionType))
                 exchanges = exchanges.Where(x => x.ExchangeType.Contains(query.TransactionType));

             if (query.TransactionAmount != 0)
                 exchanges = exchanges.Where(x => x.ExchangeAmount.Equals(query.TransactionAmount));*/

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("exchangeType", StringComparison.OrdinalIgnoreCase))
                    exchanges = query.IsDecsending ? exchanges.OrderByDescending(x => x.ExchangeType) : exchanges.OrderBy(x => x.ExchangeType);

                if (query.SortBy.Equals("exchangeDescription", StringComparison.OrdinalIgnoreCase))
                    exchanges = query.IsDecsending ? exchanges.OrderByDescending(x => x.ExchangeDescription) : exchanges.OrderBy(x => x.ExchangeDescription);

                if (query.SortBy.Equals("exchangeDate", StringComparison.OrdinalIgnoreCase))
                    exchanges = query.IsDecsending ? exchanges.OrderByDescending(x => x.ExchangeDate) : exchanges.OrderBy(x => x.ExchangeDate);
            }

            return exchanges;
        }

        private IQueryable<Exchange> ApplyPagination(IQueryable<Exchange> exchanges, QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return exchanges.Skip(skipNumber).Take(query.PageSize);
        }

        public async Task<Exchange> GetExchangeById(int id)
        {
            return await _context.Exchanges.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Exchange> SetExchange(Exchange exchangeModel)
        {
            await _context.Exchanges.AddAsync(exchangeModel);
            await _context.SaveChangesAsync();
            return exchangeModel;
        }

        public Task<Exchange> UpdateExchange(Exchange exchangeModel)
        {
            throw new NotImplementedException();
        }
        public async Task<Exchange> DeleteExchange(int id)
        {
            var exchangeModel = await _context.Exchanges.FirstOrDefaultAsync(x => x.Id == id);

            if (exchangeModel == null)
                return null;

            _context.Exchanges.Remove(exchangeModel);
            await _context.SaveChangesAsync();

            return exchangeModel;
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(s => s.Id == id);
        }

        public async Task<DasboardDto> GetDashboardValues(string id)
        {
            var firstDayOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            var monthlyProfit = await _context.Exchanges
                .Where(e =>e.AppUserId == id && e.ExchangeType  == true && e.ExchangeDate >= firstDayOfMonth && e.ExchangeDate <= lastDayOfMonth).Select(e => (double)e.ExchangeAmount).SumAsync();
            var monthlyExpenses = await _context.Exchanges
                .Where(e =>e.AppUserId == id && e.ExchangeType  == false && e.ExchangeDate >= firstDayOfMonth && e.ExchangeDate <= lastDayOfMonth).Select(e => (double)e.ExchangeAmount).SumAsync();
            var monthlySummary = monthlyProfit - monthlyExpenses;
            
            var totalProfit = await _context.Exchanges
                .Where(e =>e.AppUserId == id && e.ExchangeType  == true).Select(e => (double)e.ExchangeAmount).SumAsync();
            var totalExpenses = await _context.Exchanges
                .Where(e =>e.AppUserId == id && e.ExchangeType  == false).Select(e => (double)e.ExchangeAmount).SumAsync();

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