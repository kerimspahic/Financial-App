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
                ExchangeDescription = exchangeModel.ExchangeDescription,
                ExchangeDate = exchangeModel.ExchangeDate,
                AppUserId = exchangeModel.AppUserId
            };
        }

        public static Exchange ToExchangeFromSet (this SetExchangeDto setExchange, string id)
        {
            return new Exchange
            {
                ExchangeAmount = setExchange.ExchangeAmount,
                ExchangeType = setExchange.ExchangeType,
                ExchangeDescription = setExchange.ExchangeDescription,
                ExchangeDate = setExchange.ExchangeDate,
                AppUserId = id
            };
        }
    }
}