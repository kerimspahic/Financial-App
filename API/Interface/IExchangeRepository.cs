using API.DTOs.Transaction;
using API.Helpers;
using API.Models;

namespace API.Interface
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransactions(QueryObject query);
        Task<List<Transaction>> GetUserTransactions(QueryObject query, string id);
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> SetTransaction(Transaction transactionModel);
        Task<Transaction> UpdateTransaction(Transaction transactionModel);
        Task<Transaction> DeleteTransaction(int id);
        Task<bool> UserExists(string id);
        Task<DasboardDto> GetDashboardValues(string id);
        Task<DashboardChartsDto> GetDashboardChartValues(string id);
    }
}