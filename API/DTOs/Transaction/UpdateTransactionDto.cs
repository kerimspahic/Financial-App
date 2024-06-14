using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateTransactionDto
    {
        [Required]
        public int Id { get; set; }
        public double? TransactionAmount { get; set; }
        public bool? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? TransactionDescription { get; set; }
    }
}
