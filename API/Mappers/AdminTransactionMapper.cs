using API.DTOs.Admin;
using API.Models;

namespace API.Mappers
{
    public static class AdminTransactionMapper
    {
        public static SetDescriptionNameDto ToDescriptionNameDto (this ExchangeDescriptions dto)
        {
            return new SetDescriptionNameDto
            {
                DescriptionName = dto.DescriptionName
            };
        }

        public static ExchangeDescriptions ToExchangeDescriptionsFromSet(this SetDescriptionNameDto dto)
        {
            return new ExchangeDescriptions
            {
                DescriptionName = dto.DescriptionName
            };
        }
    }
}