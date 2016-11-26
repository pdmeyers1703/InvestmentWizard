namespace InvestmentWizard
{
	using System.Collections.Generic;

    public interface ICurrentPosition
    {
        string StockTicker { get; }

        double? Quantity { get; set; }

        decimal Cost { get; set; }

        decimal? CurrentPrice { get; }

        string PriceChange { get; }

        string PriceChangePercent { get; }

        decimal CurrentMarketValue { get; }

        decimal GainLoss { get; }

        double PercentGainLoss { get; }

        double YtdPercentGainLoss { get; set; }

		void UpdateRealTimeQuote(PriceQuote priceQuote);

		List<string> ToStringList();
    }
}