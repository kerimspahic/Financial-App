using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Exchange
{
    public class SetExchangeDto
    {
        [Required]
        public decimal ExchangeAmount { get; set; }
        [Required]
        public string ExchangeType { get; set; } = string.Empty;
        [Required]
        public string ExchangeDate { get; set; }
        [Required]
        public string ExchangeDescription { get; set; } = string.Empty;
    }
}