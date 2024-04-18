using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Admin;
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

        public async Task<ExchangeDescriptions> DeleteTransactionDescription(int id)
        {
            var transactionDescription = await _context.ExchangeDescriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (transactionDescription == null)
                return null;

            _context.ExchangeDescriptions.Remove(transactionDescription);
            await _context.SaveChangesAsync();

            return transactionDescription;
        }

        public async Task<List<ExchangeDescriptions>> GetTransactionDescriptions()
        {
            return await _context.ExchangeDescriptions.ToListAsync();
        }

        public async Task<ExchangeDescriptions> SetTransactionDescription(ExchangeDescriptions descriptionName)
        {

            await _context.ExchangeDescriptions.AddAsync(descriptionName);
            await _context.SaveChangesAsync();
            return descriptionName;
        }

        public async Task<ExchangeDescriptions> UpdateTransactionDescription(int id, string descriptionName)
        {
            var transactionName = await _context.ExchangeDescriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (transactionName == null)
                return null;

            transactionName.DescriptionName = descriptionName;
            await _context.SaveChangesAsync();

            return transactionName;
        }
    }
}