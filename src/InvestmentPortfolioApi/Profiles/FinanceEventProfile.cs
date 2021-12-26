using AutoMapper;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Responses.Finance;
using InvestmentPortfolioApi.Models.Responses.FinanceEvent;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace InvestmentPortfolioApi.Profiles
{
    public class FinanceEventProfile : Profile
    {
        public FinanceEventProfile()
        {
            CreateMap<IEnumerable<FinanceEvent>, FinanceEventGetResponse>()
                .ForPath(dest => dest.FinanceEvents, opt => opt.MapFrom(src => src.Select(f => new FinanceEventResponse
                {
                    Date = f.Date,
                    Operation = f.Operation.GetType().GetMember(f.Operation.ToString()).First().GetCustomAttribute<DescriptionAttribute>().Description,
                    Price = f.Price,
                    Quantity = f.Quantity,
                    TotalAmount = f.TotalAmount,
                    Finance = new FinanceResponse
                    {
                        Id = f.FinanceId
                    }
                })))
                .ForPath(dest => dest.StatusCode, opt => opt.MapFrom(src => StatusCodes.Status200OK))
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedId, opt => opt.Ignore());
        }
    }
}
