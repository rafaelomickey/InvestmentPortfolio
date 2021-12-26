using InvestmentPortfolioApi.Interfaces.Services;
using InvestmentPortfolioApi.Models.Requests.ExchangeRate;
using InvestmentPortfolioApi.Models.Responses;
using InvestmentPortfolioApi.Models.Responses.ExchangeRate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Controllers
{
    [Route("v{version:apiVersion}/exchangeRate")]
    [ApiVersion("1")]
    [ApiController]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] ExchangeRateGetRequest request)
        {
            var response = await _exchangeRateService.Get(request);
            return new ObjectResult(response);
        }
    }
}
