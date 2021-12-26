using AutoFixture;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Services
{
    public class FinanceServiceTest : BaseServiceTest
    {
        private readonly ILogger<FinanceService> _logger;
        private readonly IFinanceRepository _financeRepository;
        private readonly IFinanceService _financeService;
        public FinanceServiceTest()
        {
            _logger = Substitute.For<ILogger<FinanceService>>();
            _financeRepository = Substitute.For<IFinanceRepository>();
            _financeService = new FinanceService(_logger, _financeRepository);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Test_Add(bool exist)
        {
            var request = _fixture.Create<FinanceAddRequest>();
            var response = _fixture.Create<int>();

            _financeRepository.Exists(Arg.Any<FinanceGetRequest>()).Returns(exist);
            _financeRepository.Add(request).Returns(response);

            var result = await _financeService.Add(request);

            if (exist)
                Assert.Equal(result.StatusCode, StatusCodes.Status409Conflict);
            else
            {
                Assert.NotNull(result);
                Assert.IsType<BaseResponse>(result);
                Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
            }
        }

        [Fact]
        public async Task Test_Update()
        {
            var request = _fixture.Create<FinanceUpdateRequest>();

            var result = await _financeService.Update(request);

            Assert.NotNull(result);
            Assert.IsType<BaseResponse>(result);
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
        }
    }
}
