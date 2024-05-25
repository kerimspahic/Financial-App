using API.DTOs;
using API.DTOs.Transaction;
using API.Helpers;
using API.Models;
using API.Response;

namespace API.Interface
{
    public interface ITransactionRepository
    {
        //Task<List<Transaction>> GetAllTransactions(QueryObject query);
        Task<PaginatedResponse<TransactionDto>> GetUserTransactions(QueryObject query, string id);
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> SetTransaction(Transaction transactionModel);
        Task<Transaction> UpdateTransaction(Transaction transactionModel);
        Task<Transaction> DeleteTransaction(int id);
        Task<bool> UserExists(string id);
        //Task<DashboardChartsDto> GetDashboardChartValues(string id);
    }
}