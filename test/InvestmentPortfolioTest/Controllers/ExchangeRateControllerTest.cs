using AutoFixture;
using InvestmentPortfolioApi.Controllers;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Models.Responses.ExchangeRate;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Controllers
{
    public class ExchangeRateControllerTest : BaseControllerTest
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ExchangeRateController _exchangeRateController;

        public ExchangeRateControllerTest()
        {
            _exchangeRateService = Substitute.For<IExchangeRateService>();
            _exchangeRateController = new ExchangeRateController(_exchangeRateService);
        }

        [Fact]
        public async Task Test_Get()
        {
            var request = _fixture.Create<ExchangeRateGetRequest>();
            var response = _fixture.Create<ExchangeRateResponse>();

            _exchangeRateService.Get(request).Returns(response);

            var result = await _exchangeRateController.Get(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }
    }
}
