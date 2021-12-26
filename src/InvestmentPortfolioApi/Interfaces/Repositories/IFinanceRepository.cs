using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Repositories
{
    public interface IFinanceRepository
    {
        Task<int> Add(FinanceAddRequest request);
        Task<IEnumerable<Finance>> Search(FinanceGetRequest request = null);
        Task<bool> Exists(FinanceGetRequest request);
        Task Update(FinanceUpdateRequest request);
    }
}
