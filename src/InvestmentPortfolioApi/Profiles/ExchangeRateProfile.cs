using AutoMapper;
using InvestmentPortfolioApi.Models.Responses.ExchangeRate;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace InvestmentPortfolioApi.Profiles
{
    public class ExchangeRateProfile : Profile
    {
        public ExchangeRateProfile()
        {
            CreateMap<ExchangeRateServiceGetResponse, ExchangeRateResponse>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.QuoteResponse.Result.FirstOrDefault().LongName))
                .ForMember(dest => dest.BuyPrice, opt => opt.MapFrom(src => src.QuoteResponse.Result.FirstOrDefault().Bid))
                .ForMember(dest => dest.SellPrice, opt => opt.MapFrom(src => src.QuoteResponse.Result.FirstOrDefault().Ask))
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.QuoteResponse.Result.FirstOrDefault() != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent)
                )
                .ForMember(dest => dest.ErrorMessage, opt=> opt.Ignore())
                .ForMember(dest => dest.CreatedId, opt => opt.Ignore());
        }
    }
}
