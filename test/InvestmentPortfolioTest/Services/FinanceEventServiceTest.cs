using AutoFixture;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.FinanceEvent;
using InvestmentPortfolioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Services
{
    public class FinanceEventServiceTest : BaseServiceTest
    {
        private readonly ILogger<FinanceEventService> _logger;
        private readonly IFinanceRepository _financeRepository;
        private readonly IFinanceEventRepository _financeEventRepository;
        private readonly IFinanceParameterRepository _financeRulesRepository;
        private readonly IFinanceEventService _financeEventService;

        public FinanceEventServiceTest()
        {
            _logger = Substitute.For<ILogger<FinanceEventService>>();
            _financeRepository = Substitute.For<IFinanceRepository>();
            _financeEventRepository = Substitute.For<IFinanceEventRepository>();
            _financeRulesRepository = Substitute.For<IFinanceParameterRepository>();
            _financeEventService = new FinanceEventService(_financeEventRepository, _financeRepository, _financeRulesRepository, _mapper, _logger);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Test_Add(bool exist)
        {
            var request = _fixture.Create<FinanceEventAddRequest>();
            var financeSearchResponse = _fixture.Create<IEnumerable<Finance>>();

            var financeParameter = _fixture.Create<FinanceParameter>();
            financeParameter.Value = "2";

            var response = _fixture.Create<int>();

            _financeRepository.Exists(Arg.Any<FinanceGetRequest>()).Returns(exist);
            _financeRepository.Search(Arg.Any<FinanceGetRequest>()).Returns(financeSearchResponse);
            _financeEventRepository.Add(request).Returns(response);
            _financeRulesRepository.Get(Arg.Any<int>()).Returns(financeParameter);

            var result = await _financeEventService.Add(request);

            if (!exist)
                Assert.Equal(result.StatusCode, StatusCodes.Status409Conflict);
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BaseResponse>(result);
                Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
            }
        }

        [Fact]
        public async Task Test_Get_With_Result()
        {
            var request = _fixture.Create<FinanceEventGetRequest>();
            var responseSearchRepository = _fixture.Create<IEnumerable<FinanceEvent>>();

            _financeEventRepository.Get(request).Returns(responseSearchRepository);

            var result = await _financeEventService.Get(request);

            Assert.NotNull(result);
            Assert.IsType<FinanceEventGetResponse>(result);
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public async Task Test_Get_Empty_Result()
        {
            var request = _fixture.Create<FinanceEventGetRequest>();
            IEnumerable<FinanceEvent> responseSearchRepository = null;

            _financeEventRepository.Get(request).Returns(responseSearchRepository);

            var result = await _financeEventService.Get(request);

            Assert.NotNull(result);
            Assert.IsType<FinanceEventGetResponse>(result);
            Assert.Equal(result.StatusCode, StatusCodes.Status204NoContent);
        }
    }
}
