using AutoMapper;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Entities;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.Finance;
using InvestmentPortfolioApi.Models.Responses.FinanceEvent;
using InvestmentPortfolioApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace InvestmentPortfolioApi.Services
{
    public class FinanceEventService : IFinanceEventService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FinanceEventService> _logger;
        private readonly IFinanceRepository _financeRepository;
        private readonly IFinanceEventRepository _financeEventRepository;
        private readonly IFinanceParameterRepository _financeParameterRepository;

        public FinanceEventService(
            IFinanceEventRepository financeEventRepository,
            IFinanceRepository financeRepository,
            IFinanceParameterRepository financeRulesRepository,
            IMapper mapper,
            ILogger<FinanceEventService> logger
        )
        {
            _logger = logger;
            _mapper = mapper;
            _financeRepository = financeRepository;
            _financeEventRepository = financeEventRepository;
            _financeParameterRepository = financeRulesRepository;
        }

        public async Task<BaseResponse> Add(FinanceEventAddRequest request)
        {
            try
            {
                var financesExists = await _financeRepository.Exists(new FinanceGetRequest { FinanceCode = request.FinanceCode });

                if (!financesExists)
                    return new BaseResponse { StatusCode = StatusCodes.Status409Conflict, ErrorMessage = Resource.FinanceNotFound };

                var finances = await _financeRepository.Search(new FinanceGetRequest { FinanceCode = request.FinanceCode });
                request.FinanceId = finances.FirstOrDefault().Id;
                request.Date = DateTime.Now;
                request.TotalAmount = await CalculateTotalAmount(request);
                using (var transaction = new TransactionScope())
                {
                    var createdId = await _financeEventRepository.Add(request);
                    transaction.Complete();

                    return new BaseResponse { StatusCode = StatusCodes.Status201Created, CreatedId = createdId };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BaseResponse { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        public async Task<FinanceEventGetResponse> Get(FinanceEventGetRequest request)
        {
            try
            {
                var financesEvents = await _financeEventRepository.Get(request);

                if (financesEvents == null || !financesEvents.Any())
                    return new FinanceEventGetResponse { StatusCode = StatusCodes.Status204NoContent };

                var financesEventsResponse = _mapper.Map<FinanceEventGetResponse>(financesEvents);
                foreach (var financeEvent in financesEventsResponse.FinanceEvents.ToList())
                {
                    var finances = await _financeRepository.Search(new FinanceGetRequest { Id = financeEvent.Finance.Id });
                    financeEvent.Finance = _mapper.Map<FinanceResponse>(finances.FirstOrDefault());
                }
                return financesEventsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new FinanceEventGetResponse { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        private async Task<decimal> CalculateTotalAmount(FinanceEventAddRequest request)
        {
            var fees = await _financeParameterRepository.Get(FinanceParameter.Fees);
            var brokerageCost = await _financeParameterRepository.Get(FinanceParameter.BrokerageCost);

            var totalAmount = request.Price * request.Quantity;
            totalAmount += Convert.ToDecimal(brokerageCost.Value);
            totalAmount += Convert.ToDecimal((totalAmount * Convert.ToDecimal(fees.Value)) / 100);

            return totalAmount;
        }
    }
}
