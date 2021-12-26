using AutoFixture;
using AutoMapper;
using InvestmentPortfolioApi.Profiles;

namespace InvestmentPortfolioTest.Controllers
{
    public class BaseControllerTest
    {
        public readonly Fixture _fixture;
        public readonly IMapper _mapper;

        public BaseControllerTest()
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
