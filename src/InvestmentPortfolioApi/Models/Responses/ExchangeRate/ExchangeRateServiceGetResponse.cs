using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Responses.ExchangeRate
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRateServiceGetResponse
    {
        public ExchangeRateServiceGetResultResponse QuoteResponse { get; set; }
    }
}
