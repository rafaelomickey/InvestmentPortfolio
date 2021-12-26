using System.ComponentModel;

namespace InvestmentPortfolioApi.Models.Enums
{
    public enum EOperationType
    {
        [Description("C")]
        Buy,
        [Description("V")]
        Sell
    }
}
