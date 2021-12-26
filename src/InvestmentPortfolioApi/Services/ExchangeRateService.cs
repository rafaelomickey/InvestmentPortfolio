using AutoMapper;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Models.Responses.ExchangeRate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Services
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ExchangeRateService> _logger;

        public ExchangeRateService(HttpClient httpClient, IMapper mapper, ILogger<ExchangeRateService> logger)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ExchangeRateResponse> Get(ExchangeRateGetRequest request)
        {
            try
            {
                var url = $"v6/finance/quote?lang=pt&symbols={request.FinanceCode}";
                var responseMessage = _httpClient.GetAsync(url).Result;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"ErrorYahooFinance-{responseMessage.Content}");
                    return new ExchangeRateResponse { StatusCode = StatusCodes.Status400BadRequest };
                }

                var result = await responseMessage?.Content.ReadAsStringAsync();
                var financeApiResult = JsonSerializer.Deserialize<ExchangeRateServiceGetResponse>(result, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return _mapper.Map<ExchangeRateResponse>(financeApiResult);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ExchangeRateResponse { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
