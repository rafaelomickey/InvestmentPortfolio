using InvestmentPortfolioApi.Models.Enums;
using System;
using System.Diagnostics.CodeAnalysis;

namespace InvestmentPortfolioApi.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class FinanceEvent
    {
        public int Id { get; set; }
        public int FinanceId { get; set; }
        public EOperationType Operation { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
