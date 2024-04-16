using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Exchange
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeAmount { get; set; }
        public string ExchangeType { get; set; } = string.Empty;
        public string ExchangeDescription { get; set; } = string.Empty;
        public string ExchangeDate { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.UtcNow;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
