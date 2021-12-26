using AutoMapper;
using InvestmentPortfolioApi.Interfaces.Repositories;
using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.Finance;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.Finance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Controllers
{
    [Route("v{version:apiVersion}/finance")]
    [ApiVersion("1")]
    [ApiController]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public class FinanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFinanceService _financeService;
        private readonly IFinanceRepository _financeRepository;

        public FinanceController(
            IMapper mapper,
            IFinanceService financeService,
            IFinanceRepository financeRepository
        )
        {
            _financeService = financeService;
            _financeRepository = financeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that returns Finances
        /// </summary>
        /// <param name="request">FinanceGetRequest</param>
        /// <returns>FinanceGetResponse</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] FinanceGetRequest request)
        {
            var finances = await _financeRepository.Search(request);

            if (finances == null || !finances.Any())
                return new ObjectResult(null) { StatusCode = StatusCodes.Status204NoContent };

            var response = _mapper.Map<FinanceGetResponse>(finances);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        /// <summary>
        /// Method that includes an Finance
        /// </summary>
        /// <param name="request">FinanceAddRequest</param>
        /// <returns>BaseResponse</returns>
        [HttpPut()]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromBody] FinanceAddRequest request)
        {
            var response = await _financeService.Add(request);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        /// <summary>
        /// Method that update an Finance
        /// </summary>
        /// <param name="request">FinanceUpdateRequest</param>
        /// <returns>BaseResponse</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] FinanceUpdateRequest request)
        {
            var response = await _financeService.Update(request);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
