namespace InvestmentPortfolioApi.Models.Responses.ExchangeRate
{
    public class ExchangeRateResponse : BaseResponse
    {
        public string CompanyName { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
    }
}
