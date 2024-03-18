namespace API.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool Type { get; set; }
        public DateTime Date{ get; set; }
        public DateTime DateInserted { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}