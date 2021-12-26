using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Models.Responses.ExchangeRate;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Services
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> Get(ExchangeRateGetRequest request);
    }
}
