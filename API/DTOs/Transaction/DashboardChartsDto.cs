namespace API.DTOs.Transaction
{
    public class DashboardChartsDto
    {
        public double[] TransactionDeposits { get; set; }
        public double[] TransactionWithdrawals { get; set; }
        public double[] AllTransactions { get; set; }
    }
}