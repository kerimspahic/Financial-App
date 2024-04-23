using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Exchange
{
    public class SetExchangeDto
    {
        [Required]
        public double ExchangeAmount { get; set; }
        [Required]
        public bool ExchangeType { get; set; } 
        [Required]
        public DateTime ExchangeDate { get; set; }
        [Required]
        public int ExchangeDescription { get; set; } 
    }
}