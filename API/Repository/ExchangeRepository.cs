using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly AppDbContext _context;
        public ExchangeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Exchange>> GetAllExchanges()
        {
            return await _context.Exchanges.ToListAsync();
        }

        public  async Task<Exchange> GetExchangeById(int id)
        {
            return await _context.Exchanges.Include(a =>a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
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

            if(exchangeModel == null)
                return null;

            _context.Exchanges.Remove(exchangeModel);
            await _context.SaveChangesAsync();

            return exchangeModel;
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(s => s.Id == id);
        }
    }
}