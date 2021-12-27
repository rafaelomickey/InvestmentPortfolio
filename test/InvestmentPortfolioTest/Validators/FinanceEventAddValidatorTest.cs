using FluentAssertions;
using InvestmentPortfolioApi.Models.Enums;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Validators;
using Xunit;

namespace InvestmentPortfolioTest.Validators
{
    public class FinanceEventAddValidatorTest : BaseValidatorTest<FinanceEventAddValidator>
    {
        private FinanceEventAddRequest _request;
        public FinanceEventAddValidatorTest()
        {
            _validator = new FinanceEventAddValidator();
        }

        [Theory]
        [InlineData("AAPL", EOperationType.Buy, 11.32, 4)]
        public void Test_Finance_Event_Validator_Ok(string financeCode, EOperationType operation, decimal price, int quantity)
        {
            _request = new FinanceEventAddRequest
            {
                FinanceCode = financeCode,
                Operation = operation,
                Price = price,
                Quantity = quantity
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("AAPL", EOperationType.Buy, 15, 0)]
        [InlineData("AAPL", EOperationType.Buy, 0, 4)]
        [InlineData("AAPL", null, 11.32, 4)]
        [InlineData("", EOperationType.Buy, 11.32, 4)]
        [InlineData(null, EOperationType.Buy, 11.32, 4)]
        public void Test_Finance_Event_Validator_Error(string financeCode, EOperationType? operation, decimal price, int quantity)
        {
            _request = new FinanceEventAddRequest
            {
                FinanceCode = financeCode,
                Operation = operation,
                Price = price,
                Quantity = quantity
            };

            var validationResult = _validator.Validate(_request);
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
