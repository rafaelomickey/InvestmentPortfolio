using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.FinanceEvent;
using InvestmentPortfolioApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Controllers
{
    [Route("v{version:apiVersion}/financeEvent")]
    [ApiVersion("1")]
    [ApiController]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public class FinanceEventController : ControllerBase
    {
        private readonly IFinanceEventService _financeEventService;
        public FinanceEventController(IFinanceEventService financeEventService)
        {
            _financeEventService = financeEventService;
        }

        /// <summary>
        /// Method that returns Finances Events
        /// </summary>
        /// <param name="request">FinanceGetRequest</param>
        /// <returns>FinanceGetResponse</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] FinanceEventGetRequest request)
        {
            var response = await _financeEventService.Get(request);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        /// <summary>
        /// Method that includes an Finance Event
        /// </summary>
        /// <param name="request">FinanceAddRequest</param>
        /// <returns>BaseResponse</returns>
        [HttpPut()]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromBody] FinanceEventAddRequest request)
        {
            var response = await _financeEventService.Add(request);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
