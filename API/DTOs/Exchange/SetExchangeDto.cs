using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Exchange
{
    public class SetExchangeDto
    {
        [Required]
        public decimal ExchangeAmount { get; set; }
        [Required]
        public bool ExchangeType { get; set; } 
        [Required]
        public string ExchangeDate { get; set; }
        [Required]
        public int ExchangeDescription { get; set; } 
    }
}