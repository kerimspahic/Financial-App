using API.DTOs.Exchange;
using API.Helpers;
using API.Models;

namespace API.Interface
{
    public interface IExchangeRepository
    {
        Task<List<Exchange>> GetAllExchanges(QueryObject query);
        Task<List<Exchange>> GetUserExchanges(QueryObject query, string id);
        Task<Exchange> GetExchangeById(int id);
        Task<Exchange> SetExchange(Exchange exchangeModel);
        Task<Exchange> UpdateExchange(Exchange exchangeModel);
        Task<Exchange> DeleteExchange(int id);
        Task<bool> UserExists(string id);
        Task<DasboardDto> GetDashboardValues(string id);
    }
}