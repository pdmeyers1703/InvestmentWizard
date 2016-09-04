namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CurrentPositionModel : ICurrentPositionsModel
    {
        private List<IOpenPositions> currentPositions;
        private OpenPositionTotals totals = null;
        private IFinancialData server;
        private ITransactionsModel transactionsModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server">client for real-time quotes</param>
        /// <param name="transactions">database</param>
        public CurrentPositionModel(IFinancialData server, ITransactionsModel transactionsModel)
        {
            this.currentPositions = new List<IOpenPositions>();
            this.server = server;
            this.transactionsModel = transactionsModel;
        }

        /// <summary>
        // Get/Set the list of current positions
        /// </summary>
        public List<IOpenPositions> CurrentPositions 
        {
            get
            {
                return this.currentPositions;
            }

            private set
            {
                this.currentPositions = value;
            }
        }

        /// <summary>
        /// Builds the latest list of current positions
        /// </summary>
        /// <param name="list"></param>
        public void Update(IList<ITransaction> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            // Clear previously built list
            this.currentPositions.Clear();

            foreach (var t in from r in list where r.SaleDate == null select r)
            {
                IOpenPositions open = this.GetPosition(t.EquitySymbol);

                if (open != null)
                {
                    open.Quantity += t.Quanity;
                    open.Cost += t.Cost;
                }
                else
                {
                    OpenPositions newOpenPos = new OpenPositions();
                    newOpenPos.StockTicker = t.EquitySymbol;
                    newOpenPos.Quantity = t.Quanity;
                    newOpenPos.Cost = t.Cost;
                    this.currentPositions.Add(newOpenPos);
                }
            }

            this.Update();
        }

        /// <summary>
        /// Get the last prices, etc for all equities in the 
        /// open positions list.
        /// </summary>
        public void Update()
        {
            List<PriceQuote> quotes = new List<PriceQuote>();
            List<string> symbols = this.GetTickerSymbols();

            this.server.GetPrices(symbols, out quotes);

            foreach (var q in quotes)
            {
                IOpenPositions o = this.currentPositions.Find(a => a.StockTicker == q.Symbol);
                o.CurrentPrice = q.LastPrice;
                o.PriceChange = q.PriceChangeAbsolute;
                o.PriceChangePercent = q.PriceChangePercent;
                
                // Calculate YTD price change
                decimal ytdCost = this.GetEquityYearToDateCost(q.Symbol);
                if (ytdCost == 0)
                {
                    o.YtdPercentGainLoss = 0.00f;
                }
                else
                {
                    o.YtdPercentGainLoss = (double)((o.CurrentMarketValue - ytdCost) / ytdCost);
                }
            }
        }

        /// <summary>
        /// Addes total to the last row in the open positions list.
        /// </summary>
        public void BuildTotals()
        {
            this.DeleteTotals();
            
            this.totals = new OpenPositionTotals();
            this.totals.StockTicker = "Total:";
            this.totals.Quantity = null;
            this.totals.Cost = this.CalcTotalCost();
            this.totals.CurrentPrice = null;
            this.totals.CurrentMarketValue = this.CalcTotalMarketValue();
            this.totals.GainLoss = this.CalcTotalMarketValue() - this.CalcTotalCost();
            this.totals.PercentGainLoss = this.CalcTotalCost() != 0 ? (double)(this.totals.GainLoss / this.CalcTotalCost()) : 0.00;
            this.totals.PriceChange = string.Empty;
            this.totals.PriceChangePercent = this.CalcPriceChangePercent().ToString();
            this.totals.YtdPercentGainLoss = this.CalYTDPriceChangePercentTotal();

            this.currentPositions.Add(this.totals);
        }

        /// <summary>
        /// Returns the OpenPosition recored that matches the specified
        /// ticker symbol. If no matches returns null.
        /// </summary>
        /// <param name="symbol">Equity Symbol to match</param>
        /// <returns>OpenPositions object</returns>
        private IOpenPositions GetPosition(string symbol)
        {
            return this.currentPositions.Find(delegate(IOpenPositions pos) 
            { 
                return pos.StockTicker == symbol; 
            });
        }

        /// <summary>
        /// Returns a list of equity symbols extracted from the open
        /// positions list.
        /// </summary>
        /// <param name="openList"></param>
        /// <returns>List of equity symbols</returns>
        private List<string> GetTickerSymbols()
        {
            List<string> symbols = new List<string>();

            foreach (var o in this.currentPositions)
            {
                symbols.Add(o.StockTicker);
            }

            return symbols;
        }

        /// <summary>
        /// Removes the totals from the last row of the open positions list
        /// </summary>
        private void DeleteTotals()
        {
            this.currentPositions.RemoveAll(item => item.StockTicker == "Total:");
        }

        /// <summary>
        /// Calculates the total cost
        /// </summary>
        /// <returns></returns>
        private decimal CalcTotalCost()
        {
            return this.currentPositions.Sum(x => x.Cost);
        }

        /// <summary>
        /// Calculates the total Market value
        /// </summary>
        /// <returns>Total market value</returns>
        private decimal CalcTotalMarketValue()
        {
            return this.currentPositions.Sum(x => x.CurrentMarketValue);
        }

        /// <summary>
        /// Calculates the daily total price change
        /// </summary>
        /// <returns> total price change</returns>
        private double CalcPriceChangePercent()
        {
            List<IOpenPositions> list = this.currentPositions.GetRange(0, this.currentPositions.Count() - 1);
            double startPrice = list.Sum(x => (Convert.ToDouble(x.CurrentPrice) -
                                               Convert.ToDouble(x.PriceChange)) * 
                                               (double)x.Quantity);

            double currentPrice = list.Sum(x => Convert.ToDouble(x.CurrentPrice) * (double)x.Quantity);

            if (startPrice != 0.0)
            {
                return Math.Round(((currentPrice - startPrice) / startPrice) * 100, 2);
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Calculates the total YTD price change for all open
        /// positions
        /// </summary>
        /// <returns>percent ytd price change total</returns>
        private double CalYTDPriceChangePercentTotal()
        {
            int count = this.currentPositions.Count() - 1;
            double totalCost = this.currentPositions.GetRange(0, count).Sum(p => (double)p.CurrentMarketValue / (1 + p.YtdPercentGainLoss));
            double totalMarketValue = this.currentPositions.GetRange(0, count).Sum(p => (double)p.CurrentMarketValue);

            if (totalCost != 0.0)
            {
                return (totalMarketValue - totalCost) / totalCost;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Get the year to date cost of a single equity
        /// If equity was bought in a previous year the cost would the value on December 31st of the previous year
        /// If the equity was bought this year, the cost is purchased cost
        /// </summary>
        /// <param name="equitySymbol">Equity ticker symbol</param>
        /// <returns>Total year to date cost of a single equity</returns>
        private decimal GetEquityYearToDateCost(string equitySymbol)
        {
            decimal cost = 0;

            IEnumerable<ITransaction> transactions = from t in this.transactionsModel.Transactions
                                                      where t.EquitySymbol == equitySymbol
                                                      select t;
                
            foreach (var t in transactions)
            {
                if (t.PurchasedDate < DateTimeHelper.GetYTD())
                {
                    string lastYearsPriceString = string.Empty;
                    this.server.GetHistoricalPrice(t.EquitySymbol, DateTimeHelper.GetYTD(), ref lastYearsPriceString);
                    if (!string.IsNullOrEmpty(lastYearsPriceString))
                    {
                        cost += Convert.ToDecimal(lastYearsPriceString) * Convert.ToDecimal(t.Quanity);
                    }
                }
                else
                {
                    cost += t.Cost;
                }
            }

            return cost;
        }
    }
}
