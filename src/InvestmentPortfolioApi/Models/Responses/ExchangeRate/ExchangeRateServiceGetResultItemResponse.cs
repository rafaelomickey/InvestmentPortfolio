using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Responses.ExchangeRate
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRateServiceGetResultItemResponse
    {
        public string LongName { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }
}
