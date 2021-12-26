using System.Collections.Generic;

namespace InvestmentPortfolioApi.Models.Responses.Finance
{
    public class FinanceGetResponse : BaseResponse
    {
        public IEnumerable<FinanceResponse> Finances { get; set; }
    }
}
