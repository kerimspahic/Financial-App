using API.Models;

namespace API.Interface
{
    public interface IAutomaticTransactionRepository
    {
        Task<AutomaticTransactions> CreateAutomaticTransaction(AutomaticTransactions automaticTransaction);
        Task<List<AutomaticTransactions>> GetAutomaticTransactions();
        Task<AutomaticTransactions> GetAutomaticTransactionById(int id);
        Task<AutomaticTransactions> UpdateAutomaticTransaction(AutomaticTransactions automaticTransaction);
        Task<AutomaticTransactions> DeleteAutomaticTransaction(int id);
    }
}
