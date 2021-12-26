using AutoMapper;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Responses.Finance;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentPortfolioApi.Profiles
{
    public class FinanceProfile : Profile
    {
        public FinanceProfile()
        {
            CreateMap<IEnumerable<Finance>, FinanceGetResponse>()
                .ForPath(dest => dest.Finances, opt => opt.MapFrom(src => src.Select(f => new FinanceResponse
                {
                    Id = f.Id,
                    CompanyName = f.CompanyName,
                    FinanceCode = f.FinanceCode
                })))
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => StatusCodes.Status200OK))
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedId, opt => opt.Ignore());

            CreateMap<Finance, FinanceResponse>();
        }
    }
}
