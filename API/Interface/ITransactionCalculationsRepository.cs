using API.DTOs.Transaction;

namespace API.Interface
{
    public interface ITransactionCalculationsRepository
    {
        Task<DashboardDto> GetTotalValuesAsync(string userId);
        Task<DashboardDto> GetYearlyValuesAsync(string userId);
        Task<DashboardDto> GetMonthlyValuesAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetTotalGainChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetTotalSpentChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetTotalProfitChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetYearlyGainChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetYearlySpentChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetYearlyProfitChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetMonthlyGainChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetMonthlySpentChartAsync(string userId);
        Task<IEnumerable<DashboardChartDto>> GetMonthlyProfitChartAsync(string userId);
    }
}
