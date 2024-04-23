using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public double TransactionAmount { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.UtcNow;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
