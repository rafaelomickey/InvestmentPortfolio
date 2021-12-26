using Dapper;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Models.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class FinanceParameterRepository : IFinanceParameterRepository
    {
        private string _connectionString { get; set; }
        public FinanceParameterRepository(string conn)
        {
            _connectionString = conn;
        }

        public async Task<FinanceParameter> Get(int id)
        {
            var query = @"SELECT 
                            FNP_ID Id,
                            FNP_VALUE Value
                          FROM TB_FINANCE_PARAMETER
                          WHERE FNP_ID = @Id";

            using (var _conn = new MySqlConnection(_connectionString))
            {
                if (_conn.State == ConnectionState.Closed)
                    await _conn.OpenAsync();

                return await _conn.QueryFirstOrDefaultAsync<FinanceParameter>(query, new { Id = id });
            }
        }
    }
}
