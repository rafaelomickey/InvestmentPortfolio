using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Repositories
{
    public interface IFinanceEventRepository
    {
        Task<int> Add(FinanceEventAddRequest request);
        Task<IEnumerable<FinanceEvent>> Get(FinanceEventGetRequest request);
    }
}
