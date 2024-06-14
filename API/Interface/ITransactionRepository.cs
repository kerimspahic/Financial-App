using API.DTOs;
using API.Helpers;
using API.Models;
using API.Response;

namespace API.Interface
{
    public interface ITransactionRepository
    {
        Task<PaginatedResponse<TransactionDto>> GetUserTransactions(QueryObject query, string id);
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> SetTransaction(Transaction transactionModel);
        Task<Transaction> UpdateTransaction(UpdateTransactionDto updateTransactionDto, string userId);
        Task<Transaction> DeleteTransaction(int id);
        Task<bool> UserExists(string id);
    }
}