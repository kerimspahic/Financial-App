namespace API.DTOs.FinancialGoal
{
    public class FinancialGoalAdminDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double TotalProfitGoal { get; set; }
        public double YearlyProfitGoal { get; set; }
        public double YearlyGainGoal { get; set; }
        public double YearlySpentLimit { get; set; }
        public double MonthlyProfitGoal { get; set; }
        public double MonthlyGainGoal { get; set; }
        public double MonthlySpentLimit { get; set; }
        public DateTime DateEdited { get; set; }
    }
}