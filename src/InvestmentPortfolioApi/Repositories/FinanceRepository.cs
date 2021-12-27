using Dapper;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.Finance;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class FinanceRepository : IFinanceRepository
    {
        private string _connectionString { get; set; }
        public FinanceRepository(string conn)
        {
            _connectionString = conn;
        }

        public async Task<int> Add(FinanceAddRequest request)
        {
            var query = @"INSERT INTO TB_FINANCE
                        (
                            FIN_FINANCE_CODE,
                            FIN_COMPANY_NAME
                        ) 
                        VALUES
                        (
                            @FinanceCode,
                            @CompanyName
                        ); SELECT LAST_INSERT_ID();";

            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.ExecuteScalarAsync<int>(query, request);
        }

        public async Task Update(FinanceUpdateRequest request)
        {
            var query = @"UPDATE TB_FINANCE SET
                          FIN_FINANCE_CODE = @FinanceCode,
                          FIN_COMPANY_NAME = @CompanyName
                          WHERE FIN_ID = @Id";

            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await conn.ExecuteScalarAsync(query, request);
        }

        public async Task<IEnumerable<Finance>> Search(FinanceGetRequest request = null)
        {
            var query = @"SELECT 
                            FIN_ID Id, 
                            FIN_FINANCE_CODE FinanceCode,
                            FIN_COMPANY_NAME CompanyName
                          FROM TB_FINANCE
                          /**where**/";

            var builder = new SqlBuilder();

            if (request != null)
            {
                if (request.Id.HasValue)
                    builder.Where("FIN_ID = @Id", new { Id = request.Id });

                if (!string.IsNullOrEmpty(request.FinanceCode))
                    builder.Where("FIN_FINANCE_CODE = @FinanceCode", new { FinanceCode = request.FinanceCode });
            }

            var selector = builder.AddTemplate(query);
            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.QueryAsync<Finance>(selector.RawSql, selector.Parameters);
        }

        public async Task<bool> Exists(FinanceGetRequest request)
        {
            var query = @"SELECT 
                            1
                          FROM TB_FINANCE
                          WHERE FIN_FINANCE_CODE = @FinanceCode
                          LIMIT 1";

            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.QueryFirstOrDefaultAsync<bool>(query, request);
        }
    }
}
