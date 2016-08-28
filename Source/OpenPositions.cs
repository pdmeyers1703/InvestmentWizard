namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OpenPositions : IOpenPositions
    {
        private string stockTicker;
        private double? quantity;
        private decimal cost;
        private decimal? currentPrice;
        private string priceChange;
        private string priceChangePercent;

        public string StockTicker
        {
            get
            {
                return this.stockTicker;
            }

            set
            {
                this.stockTicker = value.ToUpper();
            }
        }

        public double? Quantity 
        {
            get
            {
                return this.quantity.HasValue ? (double?)Math.Round(this.quantity.Value, 3) : null;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Quantity less than zero");
                }

                this.quantity = value;
            }
        }

        public decimal Cost
        {
            get
            {
                return Math.Round(this.cost, 2);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cost is less than zero");
                }

                this.cost = value;
            }
        }

        public decimal CurrentMarketValue 
        {
            get
            {
                return this.currentPrice.HasValue ? (decimal)(this.Quantity * (double)this.CurrentPrice) : 0.00m;
            }
        }

        public decimal GainLoss 
        {
            get
            {
                return this.CurrentMarketValue - this.Cost;
            }
        }

        public double PercentGainLoss 
        {
            get
            {
                return (double)Math.Round((this.CurrentMarketValue - this.Cost) / this.Cost, 3);
            }
        }

        public decimal? CurrentPrice
        {
            get
            {
                return this.currentPrice.HasValue ? (decimal?)Math.Round(this.currentPrice.Value, 2) : null;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Current price less than zero");
                }

                this.currentPrice = value;
            }
        }

        public string PriceChange
        {
            get
            {
                return this.priceChange;
            }

            set
            {
                this.priceChange = value;
            }
        }

        public string PriceChangePercent
        {
            get
            {
                return this.priceChangePercent;
            }

            set
            {
                this.priceChangePercent = value;
            }
        }

        public double YtdPercentGainLoss { get; set; }
    }
}
