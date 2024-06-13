using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Transaction;
using API.Models;

namespace API.Interface
{
    public interface IAutomaticTransactionRepository
    {
        Task<IEnumerable<AutomaticTransactionDto>> GetDueScheduledTransactionsAsync();
        Task SetTransactionAsync(Transaction transaction);
        Task UpdateScheduledTransactionAsync(AutomaticTransactionDto automaticTransactionDto);
    }
}