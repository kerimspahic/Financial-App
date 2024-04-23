using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TransactionDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double TransactionAmount { get; set; }
        [Required]
        public bool TransactionType { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public int TransactionDescription { get; set; } 
        [Required]
        public string AppUserId { get; set; }
    }
}