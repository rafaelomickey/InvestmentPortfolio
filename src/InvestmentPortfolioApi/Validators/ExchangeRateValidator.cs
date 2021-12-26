using FluentValidation;
using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Resources;

namespace InvestmentPortfolioApi.Validators
{
    public class ExchangeRateValidator : AbstractValidator<ExchangeRateGetRequest>
    {
        public ExchangeRateValidator()
        {
            RuleFor(e => e.FinanceCode).NotNull().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
            RuleFor(e => e.FinanceCode).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
        }
    }
}
