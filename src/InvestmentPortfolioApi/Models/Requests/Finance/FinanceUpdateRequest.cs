namespace InvestmentPortfolioApi.Models.Requests.Finance
{
    public class FinanceUpdateRequest
    {
        public int Id { get; set; }
        public string FinanceCode { get; set; }
        public string CompanyName { get; set; }
    }
}
