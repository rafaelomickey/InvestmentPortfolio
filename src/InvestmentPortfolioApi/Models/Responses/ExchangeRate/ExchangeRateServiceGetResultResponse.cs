using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Responses.ExchangeRate
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRateServiceGetResultResponse
    {
        public IEnumerable<ExchangeRateServiceGetResultItemResponse> Result { get; set; }
    }
}
