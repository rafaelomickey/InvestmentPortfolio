using FluentValidation;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Resources;

namespace InvestmentPortfolioApi.Validators
{
    public class FinanceUpdateValidator : AbstractValidator<FinanceUpdateRequest>
    {
        public FinanceUpdateValidator()
        {
            RuleFor(e => e.Id).NotNull().WithMessage(string.Format(Resource.Required, Resource.FinanceId));
            RuleFor(e => e.Id).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.FinanceId));
            RuleFor(e => e.Id).NotEqual(0).WithMessage(string.Format(Resource.GreaterThanZero, Resource.FinanceId));

            RuleFor(e => e.CompanyName).NotNull().WithMessage(string.Format(Resource.Required, Resource.CompanyName));
            RuleFor(e => e.CompanyName).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.CompanyName));
            RuleFor(e => e.CompanyName).MaximumLength(255).WithMessage(string.Format(Resource.MaxSize, Resource.FinanceCode, 255));

            RuleFor(e => e.FinanceCode).NotNull().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
            RuleFor(e => e.FinanceCode).NotEmpty().WithMessage(string.Format(Resource.Required, Resource.FinanceCode));
            RuleFor(e => e.FinanceCode).MaximumLength(15).WithMessage(string.Format(Resource.MaxSize, Resource.FinanceCode, 15));
        }
    }
}
