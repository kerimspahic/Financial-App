using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Transaction
{
    public class DasboardDto
    {
        [Required]
        public double TotalMoneyAmount { get; set; }
        [Required]
        public double TotalProfit { get; set; }
        [Required]
        public double TotalExpenses { get; set; }
        [Required]
        public double MonthlySummary { get; set; }
        [Required]
        public double MonthlyProfit { get; set; }
        [Required]
        public double MonthlyExpenses { get; set; }
    }
}