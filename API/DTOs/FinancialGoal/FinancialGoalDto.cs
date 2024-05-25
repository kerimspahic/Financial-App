namespace API.DTOs.FinancialGoal
{
    public class FinancialGoalDto
    {
        public double YearlyProfitGoal { get; set; }
        public double YearlyGainGoal { get; set; }
        public double YearlySpentLimit { get; set; }
        public double MonthlyProfitGoal { get; set; }
        public double MonthlyGainGoal { get; set; }
        public double MonthlySpentLimit { get; set; }
    }
}