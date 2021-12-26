using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class FinanceParameter
    {
        public const int Fees = 1;
        public const int BrokerageCost = 2;

        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
