using AutoFixture;
using InvestmentPortfolioApi.Controllers;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.FinanceEvent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Controllers
{
    public class FinanceEventControllerTest : BaseControllerTest
    {
        private readonly IFinanceEventService _financeEventService;
        private readonly FinanceEventController _financeController;

        public FinanceEventControllerTest()
        {
            _financeEventService = Substitute.For<IFinanceEventService>();
            _financeController = new FinanceEventController(_financeEventService);
        }

        [Fact]
        public async Task Test_Get()
        {
            var request = _fixture.Create<FinanceEventGetRequest>();
            var response = _fixture.Create<FinanceEventGetResponse>();

            _financeEventService.Get(request).Returns(response);

            var result = await _financeController.Get(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Test_Add()
        {
            var request = _fixture.Create<FinanceEventAddRequest>();
            var response = _fixture.Create<BaseResponse>();

            _financeEventService.Add(request).Returns(response);

            var result = await _financeController.Add(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }
    }
}
