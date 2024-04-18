using API.DTOs.Admin;
using API.Models;

namespace API.Interface
{
    public interface IAdminTransactionRepository
    {
        Task<List<ExchangeDescriptions>> GetTransactionDescriptions();
        Task<ExchangeDescriptions> SetTransactionDescription(ExchangeDescriptions descriptionName);
        Task<ExchangeDescriptions> DeleteTransactionDescription(int id);
        Task<ExchangeDescriptions> UpdateTransactionDescription(int id, string  descriptionName);
    }
}