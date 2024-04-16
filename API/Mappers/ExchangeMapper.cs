using API.DTOs;
using API.DTOs.Exchange;
using API.Models;

namespace API.Mappers
{
    public static class ExchangeMapper
    {
        public static ExchangeDto ToExchangeDto (this Exchange exchangeModel)
        {
            return new ExchangeDto
            {
                Id = exchangeModel.Id,
                ExchangeAmount = exchangeModel.ExchangeAmount,
                ExchangeType = exchangeModel.ExchangeType,
                ExchangeDate = exchangeModel.ExchangeDate,
                ExchangeDescription = exchangeModel.ExchangeDescription,
                AppUserId = exchangeModel.AppUserId
            };
        }

        public static Exchange ToExchangeFromSet (this SetExchangeDto setExchange, string id)
        {
            return new Exchange
            {
                ExchangeAmount = setExchange.ExchangeAmount,
                ExchangeType = setExchange.ExchangeType,
                ExchangeDate = setExchange.ExchangeDate,
                ExchangeDescription = setExchange.ExchangeDescription,
                AppUserId = id
            };
        }
    }
}