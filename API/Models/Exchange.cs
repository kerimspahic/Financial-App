using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Exchange
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeAmount { get; set; }
        public bool ExchangeType { get; set; }
        public int ExchangeDescription { get; set; }
        public string ExchangeDate { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.UtcNow;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
