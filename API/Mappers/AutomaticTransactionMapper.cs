using API.DTOs.Admin;
using API.DTOs.Transaction;
using API.Models;

namespace API.Mappers
{
    public static class AutomaticTransactionMapper
    {
        public static AutomaticTransactions ToAutomaticTransactions(this CreateAutomaticTransactionDto dto)
        {
            return new AutomaticTransactions
            {
                TransactionAmount = dto.TransactionAmount,
                TransactionType = dto.TransactionType,
                TransactionDescription = dto.TransactionDescription,
                TransactionDate = dto.TransactionDate,
                AppUserId = dto.AppUserId,
                Frequency = dto.Frequency,
                NextExecutionDate = dto.NextExecutionDate
            };
        }

        public static AutomaticTransactions ToAutomaticTransactions(this UpdateAutomaticTransactionDto dto, int id)
        {
            return new AutomaticTransactions
            {
                Id = id,
                TransactionAmount = dto.TransactionAmount,
                TransactionType = dto.TransactionType,
                TransactionDescription = dto.TransactionDescription,
                TransactionDate = dto.TransactionDate,
                Frequency = dto.Frequency,
                NextExecutionDate = dto.NextExecutionDate
            };
        }
    }
}
