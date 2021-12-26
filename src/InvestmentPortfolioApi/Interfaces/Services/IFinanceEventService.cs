using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.FinanceEvent;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Services
{
    public interface IFinanceEventService
    {
        Task<BaseResponse> Add(FinanceEventAddRequest request);
        Task<FinanceEventGetResponse> Get(FinanceEventGetRequest request);
    }
}
