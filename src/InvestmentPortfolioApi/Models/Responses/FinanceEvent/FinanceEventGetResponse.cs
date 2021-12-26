using System.Collections.Generic;

namespace InvestmentPortfolioApi.Models.Responses.FinanceEvent
{
    public class FinanceEventGetResponse : BaseResponse
    {
        public IEnumerable<FinanceEventResponse> FinanceEvents { get; set; }
    }
}
