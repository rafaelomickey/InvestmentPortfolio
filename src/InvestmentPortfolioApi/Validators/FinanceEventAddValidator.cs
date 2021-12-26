using FluentValidation;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Resources;

namespace InvestmentPortfolioApi.Validators
{
    public class FinanceEventAddValidator : AbstractValidator<FinanceEventAddRequest>
    {
        public FinanceEventAddValidator()
        {
            RuleFor(e => e.FinanceCode).NotNull().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
            RuleFor(e => e.FinanceCode).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
            RuleFor(e => e.FinanceCode).MaximumLength(15).WithMessage(string.Format(Resource.MaxSize, Resource.FinanceCode, 15));

            RuleFor(e => e.Operation).NotNull().WithMessage(string.Format(Resource.Required, Resource.Operation));
            RuleFor(e => e.Operation).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.Operation));

            RuleFor(e => e.Price).NotNull().WithMessage(string.Format(Resource.Required, Resource.Price));
            RuleFor(e => e.Price).GreaterThan(0).WithMessage(string.Format(Resource.GreaterThanZero, Resource.Price));

            RuleFor(e => e.Quantity).NotNull().WithMessage(string.Format(Resource.Required, Resource.Quantity));
            RuleFor(e => e.Quantity).GreaterThan(0).WithMessage(string.Format(Resource.GreaterThanZero, Resource.Quantity));
        }
    }
}
