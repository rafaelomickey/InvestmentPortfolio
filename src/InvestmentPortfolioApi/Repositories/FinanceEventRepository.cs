using Dapper;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class FinanceEventRepository : IFinanceEventRepository
    {
        private string _connectionString { get; set; }
        public FinanceEventRepository(string conn)
        {
            _connectionString = conn;
        }

        public async Task<int> Add(FinanceEventAddRequest request)
        {
            var query = @"INSERT INTO TB_FINANCE_EVENT
                        (
                            FNE_FINANCE_ID,
                            FNE_OPERATION,
                            FNE_PRICE,
                            FNE_QUANTITY,
                            FNE_DATE,
                            FNE_TOTAL_AMOUNT
                        ) 
                        VALUES
                        (
                            @FinanceId,
                            @Operation,
                            @Price,
                            @Quantity,
                            @Date,
                            @TotalAmount
                        ); SELECT LAST_INSERT_ID();";

            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.ExecuteScalarAsync<int>(query, request);
        }

        public async Task<IEnumerable<FinanceEvent>> Get(FinanceEventGetRequest request)
        {
            var query = @"SELECT 
                            FNE_ID Id,
                            FNE_FINANCE_ID FinanceId,
                            FNE_OPERATION Operation,
                            FNE_PRICE Price,
                            FNE_QUANTITY Quantity,
                            FNE_DATE Date,
                            FNE_TOTAL_AMOUNT TotalAmount
                          FROM TB_FINANCE_EVENT
                          /**where**/";

            var builder = new SqlBuilder();

            if (!string.IsNullOrEmpty(request.FinanceCode))
                builder.Where("FNE_FINANCE_CODE = @FinanceCode", new { FinanceCode = request.FinanceCode });

            var selector = builder.AddTemplate(query);

            await using var conn = new MySqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.QueryAsync<FinanceEvent>(selector.RawSql, selector.Parameters);
        }
    }
}
