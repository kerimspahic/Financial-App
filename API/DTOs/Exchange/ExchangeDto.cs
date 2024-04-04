using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ExchangeDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal ExchangeAmount { get; set; }
        [Required]
        public string ExchangeType { get; set; } = string.Empty;
        [Required]
        public string ExchangeDescription { get; set; } = string.Empty;
        [Required]
        public DateOnly ExchangeDate { get; set; }
        [Required]
        public string AppUserId { get; set; }
    }
}