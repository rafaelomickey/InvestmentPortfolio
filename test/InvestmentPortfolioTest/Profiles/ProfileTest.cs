using AutoMapper;
using InvestmentPortfolioApi.Profiles;
using Xunit;

namespace InvestmentPortfolioTest.Profiles
{
    public class ProfileTest
    {
        protected readonly IConfigurationProvider _mapperConfig;

        public ProfileTest()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ExchangeRateProfile>();
                cfg.AddProfile<FinanceEventProfile>();
                cfg.AddProfile<FinanceProfile>();
            });
        }

        [Fact]
        public void TestConfigMapper()
        {
            _mapperConfig.AssertConfigurationIsValid();
        }
    }
}
