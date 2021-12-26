using InvestmentPortfolioApi.Models.Responses.Finance;
using System;

namespace InvestmentPortfolioApi.Models.Responses.FinanceEvent
{
    public class FinanceEventResponse
    {
        public FinanceResponse Finance { get; set; }
        public string Operation { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
