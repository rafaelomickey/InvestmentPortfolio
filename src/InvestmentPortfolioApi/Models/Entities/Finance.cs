using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class Finance
    {
        public int Id { get; set; }
        public string FinanceCode { get; set; }
        public string CompanyName { get; set; }
    }
}
