using AutoFixture;
using FluentAssertions;
using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Validators;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Validators
{
    public class ExchangeRateValidatorTest : BaseValidatorTest<ExchangeRateValidator>
    {
        private ExchangeRateGetRequest _request;
        public ExchangeRateValidatorTest()
        {
            _validator = new ExchangeRateValidator();
        }

        [Theory]
        [InlineData("AAPL")]
        public async Task Test_Exchange_Rate_Validator_Ok(string financeCode)
        {
            _request = _fixture.Create<ExchangeRateGetRequest>();
            _request.FinanceCode = financeCode;

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Test_Exchange_Rate_Validator_Error(string financeCode)
        {
            _request = _fixture.Create<ExchangeRateGetRequest>();
            _request.FinanceCode = financeCode;
            
            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
