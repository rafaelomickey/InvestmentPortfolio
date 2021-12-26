using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace InvestmentPortfolioApi.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly ILogger<FinanceService> _logger;
        private readonly IFinanceRepository _financeRepository;
        public FinanceService(ILogger<FinanceService> logger, IFinanceRepository financeRepository)
        {
            _logger = logger;
            _financeRepository = financeRepository;
        }

        public async Task<BaseResponse> Add(FinanceAddRequest request)
        {
            try
            {
                var exists = await _financeRepository.Exists(new FinanceGetRequest { FinanceCode = request.FinanceCode });

                if (exists)
                    return new BaseResponse { StatusCode = StatusCodes.Status409Conflict, ErrorMessage = Resource.AlreadyExists };

                using (var transaction = new TransactionScope())
                {
                    var createdId = await _financeRepository.Add(request);
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

        public async Task<BaseResponse> Update(FinanceUpdateRequest request)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    await _financeRepository.Update(request);
                    transaction.Complete();

                    return new BaseResponse { StatusCode = StatusCodes.Status200OK };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new BaseResponse { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
