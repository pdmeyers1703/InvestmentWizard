namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OpenPositionTotals : IOpenPositions
    {
        private string averagePriceChangePercent;

        public string StockTicker { get; set; }

        public double? Quantity { get; set; }

        public decimal Cost { get; set; }

        public decimal? CurrentPrice { get; set; }

        public string PriceChange { get; set; }

        public string PriceChangePercent 
        {
            get
            {
                return this.averagePriceChangePercent + "%";
            }

            set
            {
                this.averagePriceChangePercent = value;
            }
        }

        public decimal CurrentMarketValue { get; set; }

        public decimal GainLoss { get; set; }

        public double PercentGainLoss { get; set; }

        public double YtdPercentGainLoss { get; set; }
    }
}
