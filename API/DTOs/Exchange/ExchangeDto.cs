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
        public bool ExchangeType { get; set; }
        [Required]
        public string ExchangeDate { get; set; }
        [Required]
        public int ExchangeDescription { get; set; } 
        [Required]
        public string AppUserId { get; set; }
    }
}