using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TransactionDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool Type { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}