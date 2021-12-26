using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Responses;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Services
{
    public interface IFinanceService
    {
        Task<BaseResponse> Add(FinanceAddRequest request);
        Task<BaseResponse> Update(FinanceUpdateRequest request);
    }
}
