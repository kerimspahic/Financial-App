using API.Models;

namespace API.Interface
{
    public interface IExchangeRepository
    {
        Task<List<Exchange>> GetUserExchanges(string id);
        Task<List<Exchange>> GetAllExchanges();
        Task <Exchange> GetExchangeById(int id);
        Task<Exchange> SetExchange(Exchange exchangeModel);
        Task<Exchange> UpdateExchange(Exchange exchangeModel);
        Task<Exchange> DeleteExchange(int id);
        Task<bool> UserExists(string id);
    }
}