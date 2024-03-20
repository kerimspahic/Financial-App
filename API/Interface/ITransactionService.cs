using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface ITransactionService
    {
        Task<string> Insert(TransactionDto transaction);
        Task<IEnumerable<Transactions>> Extract(string userName);
    }
}