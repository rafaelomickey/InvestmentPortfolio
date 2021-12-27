using FluentAssertions;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Validators;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPortfolioTest.Validators
{
    public class FinanceAddValidatorTest : BaseValidatorTest<FinanceAddValidator>
    {
        private FinanceAddRequest _request;
        public FinanceAddValidatorTest()
        {
            _validator = new FinanceAddValidator();
        }

        [Theory]
        [InlineData("AAPL", "Apple")]
        public void Test_Finance_Validator_Ok(string financeCode, string companyName)
        {
            _request = new FinanceAddRequest
            {
                FinanceCode = financeCode,
                CompanyName = companyName
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "Apple")]
        [InlineData(null, "Apple")]
        [InlineData("AAPL", "")]
        [InlineData("AAPL", null)]
        public void Test_Finance_Validator_Error(string financeCode, string companyName)
        {
            _request = new FinanceAddRequest
            {
                FinanceCode = financeCode,
                CompanyName = companyName
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
