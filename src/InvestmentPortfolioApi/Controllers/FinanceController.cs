using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentPortfolioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly ILogger<FinanceController> _logger;

        public FinanceController(ILogger<FinanceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Ola";
        }
    }
}
