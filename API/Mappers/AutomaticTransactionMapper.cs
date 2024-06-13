using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Transaction;
using API.Models;

namespace API.Mappers
{
    public static class AutomaticTransactionMapper
    {
        public static AutomaticTransactionDto ToScheduledTransactionDto(this AutomaticTransactions scheduledTransaction)
        {
            return new AutomaticTransactionDto
            {
                Id = scheduledTransaction.Id,
                TransactionAmount = scheduledTransaction.TransactionAmount,
                TransactionType = scheduledTransaction.TransactionType,
                TransactionDescription = scheduledTransaction.TransactionDescription,
                TransactionDate = scheduledTransaction.TransactionDate,
                AppUserId = scheduledTransaction.AppUserId,
                Frequency = scheduledTransaction.Frequency,
                NextExecutionDate = scheduledTransaction.NextExecutionDate,
                InsertedDate = scheduledTransaction.InsertedDate
            };
        }

        public static AutomaticTransactions ToScheduledTransaction(this AutomaticTransactionDto dto)
        {
            return new AutomaticTransactions
            {
                Id = dto.Id,
                TransactionAmount = dto.TransactionAmount,
                TransactionType = dto.TransactionType,
                TransactionDescription = dto.TransactionDescription,
                TransactionDate = dto.TransactionDate,
                AppUserId = dto.AppUserId,
                Frequency = dto.Frequency,
                NextExecutionDate = dto.NextExecutionDate,
                InsertedDate = dto.InsertedDate
            };
        }


    }
}