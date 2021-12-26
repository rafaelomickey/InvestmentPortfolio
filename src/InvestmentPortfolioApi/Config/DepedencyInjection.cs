using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Repositories;
using InvestmentPortfolioApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace InvestmentPortfolioApi.Config
{

    [ExcludeFromCodeCoverage]
    public static class DepedencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("YahooFinanceApi"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("YahooFinanceApiKey"));
            });
            services.AddTransient<IFinanceService, FinanceService>();
            services.AddTransient<IFinanceEventService, FinanceEventService>();

            var connectionString = Environment.GetEnvironmentVariable("MySqlConnection");
            services.AddTransient<IFinanceRepository>(repo => new FinanceRepository(connectionString));
            services.AddTransient<IFinanceEventRepository>(repo => new FinanceEventRepository(connectionString));
            services.AddTransient<IFinanceParameterRepository>(repo => new FinanceParameterRepository(connectionString));
            

            return services;
        }
    }
}
