using InvestmentPortfolioApi.Models.Enums;
using System;
using System.Text.Json.Serialization;

namespace InvestmentPortfolioApi.Models.Requests.FinanceEvent
{
    public class FinanceEventAddRequest
    {
        public string FinanceCode { get; set; }
        public EOperationType? Operation { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public int FinanceId { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public decimal TotalAmount { get; set; }
    }
}
