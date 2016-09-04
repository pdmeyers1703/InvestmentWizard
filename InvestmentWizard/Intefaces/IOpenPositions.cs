namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOpenPositions
    {
        string StockTicker { get; set; }

        double? Quantity { get; set; }

        decimal Cost { get; set; }

        decimal? CurrentPrice { get; set; }

        string PriceChange { get; set; }

        string PriceChangePercent { get; set; }

        decimal CurrentMarketValue { get; }

        decimal GainLoss { get; }

        double PercentGainLoss { get; }

        double YtdPercentGainLoss { get; set; }
    }
}