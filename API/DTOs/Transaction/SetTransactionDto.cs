using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Transaction
{
    public class SetTransactionDto
    {
        [Required]
        public double TransactionAmount { get; set; }
        [Required]
        public bool TransactionType { get; set; } 
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public int TransactionDescription { get; set; } 
    }
}