using API.Models;

namespace API.Interface
{
    public interface IAdminTransactionRepository
    {
        Task<TransactionDescriptions> DeleteTransactionDescription(int id);
        Task<List<TransactionDescriptions>> GetTransactionDescriptions();
        Task<TransactionDescriptions> SetTransactionDescription(TransactionDescriptions descriptionName);
        Task<TransactionDescriptions> UpdateTransactionDescription(int id, string  descriptionName, bool descriptionType);
    }
}