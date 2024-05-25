using API.DTOs.Admin;
using API.Models;

namespace API.Mappers
{
    public static class AdminTransactionMapper
    {
        public static SetDescriptionNameDto ToDescriptionNameDto (this TransactionDescriptions dto)
        {
            return new SetDescriptionNameDto
            {
                DescriptionName = dto.DescriptionName
            };
        }

        public static TransactionDescriptions ToTransactionDescriptionsFromSet(this SetDescriptionNameDto dto)
        {
            return new TransactionDescriptions
            {
                DescriptionName = dto.DescriptionName
            };
        }
    }
}