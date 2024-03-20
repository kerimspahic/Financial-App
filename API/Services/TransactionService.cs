using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly UserManager<User> _userMenager;
        private readonly AppDbContext _context;
        
        public TransactionService (AppDbContext context, UserManager<User> userManager)
        {
            _userMenager = userManager;
            _context = context;
        }
        public async Task<string> Insert(TransactionDto transaction)
        {
            var tranUser = await _userMenager.FindByNameAsync(transaction.UserName);

            if(tranUser is null)
                throw new ArgumentException($"User {transaction.UserName} is not in the database");
            
            var newTransaction = new Transactions
            {
                Amount = transaction.Amount,
                Type = transaction.Type,
                Date = transaction.Date,
                Description = transaction.Description,
                UserName = transaction.UserName
            };

            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            return "ok";
        }

        public async Task<IEnumerable<Transactions>> Extract(string userName)
        {
            return await _context.Transactions.Where(x => x.UserName == userName).ToListAsync();
        }
    }
}