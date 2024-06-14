using API.Models;

namespace API.DTOs.Transaction
{
    //to do optimize al dtos
    public class CreateAutomaticTransactionDto
    {
        public double TransactionAmount { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AppUserId { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime NextExecutionDate { get; set; }
    }

    public class UpdateAutomaticTransactionDto
    {
        public double TransactionAmount { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime NextExecutionDate { get; set; }
    }


}