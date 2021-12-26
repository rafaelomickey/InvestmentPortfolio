using AutoFixture;
using InvestmentPortfolioApi.Controllers;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Controllers
{
    public class FinanceControllerTest : BaseControllerTest
    {
        private readonly IFinanceService _financeService;
        private readonly IFinanceRepository _financeRepository;
        private readonly FinanceController _financeController;

        public FinanceControllerTest()
        {
            _financeRepository = Substitute.For<IFinanceRepository>();
            _financeService = Substitute.For<IFinanceService>();
            _financeController = new FinanceController(_mapper, _financeService, _financeRepository);
        }

        [Fact]
        public async Task Test_Get_With_Result()
        {
            var request = _fixture.Create<FinanceGetRequest>();
            var responseSearchRepository = _fixture.Create<IEnumerable<Finance>>();

            _financeRepository.Search(request).Returns(responseSearchRepository);

            var result = await _financeController.Get(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(((ObjectResult)result).StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public async Task Test_Get_Empty_Result()
        {
            var request = _fixture.Create<FinanceGetRequest>();
            IEnumerable<Finance> responseSearchRepository = null;

            _financeRepository.Search(request).Returns(responseSearchRepository);

            var result = await _financeController.Get(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(((ObjectResult)result).StatusCode, StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Test_Add()
        {
            var request = _fixture.Create<FinanceAddRequest>();
            var response = _fixture.Create<BaseResponse>();

            _financeService.Add(request).Returns(response);

            var result = await _financeController.Add(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Test_Update()
        {
            var request = _fixture.Create<FinanceUpdateRequest>();
            var response = _fixture.Create<BaseResponse>();

            _financeService.Update(request).Returns(response);

            var result = await _financeController.Update(request);

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
        }
    }
}
