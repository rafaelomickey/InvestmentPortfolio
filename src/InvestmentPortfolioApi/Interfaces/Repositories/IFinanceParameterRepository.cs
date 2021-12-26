using InvestmentPortfolioApi.Models.Entities;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Interfaces.Repositories
{
    public interface IFinanceParameterRepository
    {
        Task<FinanceParameter> Get(int id);
    }
}
