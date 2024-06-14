namespace API.Models
{
    public class AutomaticTransactions
    {
        public int Id { get; set; }
        public double TransactionAmount { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AppUserId { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime NextExecutionDate { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.UtcNow;
    }

    public enum FrequencyType
    {
        Weekly,
        Monthly,
        Yearly
    }

}