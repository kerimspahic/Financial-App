using API.Models;

namespace API.Interface
{
    public interface IAdminTransactionRepository
    {
        Task<List<TransactionDescriptions>> GetTransactionDescriptions();
        Task<TransactionDescriptions> SetTransactionDescription(TransactionDescriptions descriptionName);
        Task<TransactionDescriptions> DeleteTransactionDescription(int id);
        Task<TransactionDescriptions> UpdateTransactionDescription(int id, string  descriptionName);
    }
}