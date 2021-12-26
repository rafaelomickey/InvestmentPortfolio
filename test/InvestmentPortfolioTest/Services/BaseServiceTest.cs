using AutoFixture;
using AutoMapper;
using InvestmentPortfolioApi.Profiles;

namespace InvestmentPortfolioTest.Services
{
    public class BaseServiceTest
    {
        public readonly Fixture _fixture;
        public readonly IMapper _mapper;

        public BaseServiceTest()
        {
            _fixture = new Fixture();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ExchangeRateProfile>();
                cfg.AddProfile<FinanceEventProfile>();
                cfg.AddProfile<FinanceProfile>();
                cfg.ConstructServicesUsing(x => _mapper);
            });

            _mapper = config.CreateMapper();
        }
    }
}
