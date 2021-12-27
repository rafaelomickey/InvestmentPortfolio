using FluentAssertions;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Validators;
using Xunit;

namespace InvestmentPortfolioTest.Validators
{
    public class FinanceUpdateValidatorTest : BaseValidatorTest<FinanceUpdateValidator>
    {
        private FinanceUpdateRequest _request;
        public FinanceUpdateValidatorTest()
        {
            _validator = new FinanceUpdateValidator();
        }

        [Theory]
        [InlineData(1, "AAPL", "Apple")]
        public void Test_Finance_Validator_Ok(int id, string financeCode, string companyName)
        {
            _request = new FinanceUpdateRequest
            {
                Id = id,
                FinanceCode = financeCode,
                CompanyName = companyName
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, "AAPL", "Apple")]
        public void Test_Finance_Validator_Id_Error(int id, string financeCode, string companyName)
        {
            _request = new FinanceUpdateRequest
            {
                Id = id,
                FinanceCode = financeCode,
                CompanyName = companyName
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("", "Apple")]
        [InlineData(null, "Apple")]
        [InlineData("AAPL", "")]
        [InlineData("AAPL", null)]
        public void Test_Finance_Validator_Error(string financeCode, string companyName)
        {
            _request = new FinanceUpdateRequest
            {
                FinanceCode = financeCode,
                CompanyName = companyName
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
