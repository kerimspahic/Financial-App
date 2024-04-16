using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ExchangeCalculations
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountSummarized { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountDeposited { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountWithdrawn { get; set; }
    }
}