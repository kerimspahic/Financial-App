using API.DTOs;
using API.DTOs.Transaction;
using API.Models;

namespace API.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto ToTransactionDto (this Transaction transactionModel)
        {
            return new TransactionDto
            {
                Id = transactionModel.Id,
                TransactionAmount = transactionModel.TransactionAmount,
                TransactionType = transactionModel.TransactionType,
                TransactionDate = transactionModel.TransactionDate,
                TransactionDescription = transactionModel.TransactionDescription,
                AppUserId = transactionModel.AppUserId
            };
        }

        public static Transaction ToTransactionFromSet (this SetTransactionDto setTransaction, string id)
        {
            return new Transaction
            {
                TransactionAmount = setTransaction.TransactionAmount,
                TransactionType = setTransaction.TransactionType,
                TransactionDate = setTransaction.TransactionDate,
                TransactionDescription = setTransaction.TransactionDescription,
                AppUserId = id
            };
        }
    }
}