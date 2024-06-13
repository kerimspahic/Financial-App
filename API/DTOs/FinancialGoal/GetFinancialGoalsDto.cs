namespace API.DTOs.FinancialGoal
{
    public class GetFinancialGoalsDto
    {
        public double TotalProfit { get; set; }
        public double ProfitGoal { get; set; }
        public double GainGoal { get; set; }
        public double SpentLimit { get; set; }
    }
}