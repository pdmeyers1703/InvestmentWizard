namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class CurrentPositionModel : IListObservable<ICurrentPosition>, IObserver<ITransaction>
    {
        private List<ICurrentPosition> currentPositions;
		private IList<ITransaction> openTransactions;
        ////private OpenPositionTotals totals = null;
        private IFinancialData server;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="server">client for real-time quotes</param>
		/// <param name="transactions">database</param>
		/// <param name="transactionsModel">transactions model</param>
		public CurrentPositionModel(IFinancialData server)
		{
			this.currentPositions = new List<ICurrentPosition>();
			this.openTransactions = new List<ITransaction>();
			this.server = server;
		}

		/// <summary>
		/// Observer event
		/// </summary>
		/// <param name="list"></param>
		private event ListChangedEventHandler<ICurrentPosition> ListChangedObserver;

		/// <summary>
		/// Registers observer from the view
		/// </summary>
		/// <param name="listChangedObserver">Observer to monitor current open positions</param>
		public void RegisterObserver(ListChangedEventHandler<ICurrentPosition> listChangedObserver)
		{
			this.ListChangedObserver += listChangedObserver;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ListChangedEventHandler<ITransaction> GetObserverEventHandler()
		{
			return this.OnOpenTransactionsChanged;
		}

		/// <summary>
		/// Get the last prices, etc for all equities in the 
		/// open positions list.
		/// </summary>
		public void Update()
		{
			List<PriceQuote> quotes = new List<PriceQuote>();

			this.server.GetPrices(this.GetTickerSymbols(), out quotes);

			foreach (var q in quotes)
			{
				ICurrentPosition o = this.currentPositions.Find(a => a.StockTicker == q.Symbol);
				o.UpdateRealTimeQuote(q);
				decimal ytdCost = this.GetEquityYearToDateCost(q.Symbol);
				o.YtdPercentGainLoss = ytdCost == 0 ? 0.00f : (double)((o.CurrentMarketValue - ytdCost) / ytdCost);
			}

			this.ListChangedObserver(this.currentPositions);
		}

		private void OnOpenTransactionsChanged(IList<ITransaction> openTransactions)
		{
			this.openTransactions = openTransactions;
			this.UpdateList();
		}

		/// <summary>
		/// Builds the latest list of current positions
		/// </summary>
		/// <param name="list"></param>
		private void UpdateList()
		{
			// Clear previously built list
			this.currentPositions.Clear();

			foreach (var t in this.openTransactions)
			{
				ICurrentPosition open = this.GetPosition(t.EquitySymbol);

				if (open != null)
				{
					open.Quantity += t.Quanity;
					open.Cost += t.Cost;
				}
				else
				{
					List<PriceQuote> quotes = new List<PriceQuote>();
					this.server.GetPrices(new List<string>() { t.EquitySymbol }, out quotes);
					OpenPositions newOpenPos =
						new OpenPositions(t.EquitySymbol, t.Quanity, t.Cost, quotes[0].LastPrice, quotes[0].PriceChangeAbsolute, quotes[0].PriceChangePercent);
					this.currentPositions.Add(newOpenPos);
				}
			}

			this.Update();
		}

		/////// <summary>
		/////// Addes total to the last row in the open positions list.
		/////// </summary>
		////public void BuildTotals()
		////{
		////    this.DeleteTotals();

		////    this.totals = new OpenPositionTotals();
		////    this.totals.StockTicker = "Total:";
		////    this.totals.Quantity = null;
		////    this.totals.Cost = this.CalcTotalCost();
		////    this.totals.CurrentPrice = null;
		////    this.totals.CurrentMarketValue = this.CalcTotalMarketValue();
		////    this.totals.GainLoss = this.CalcTotalMarketValue() - this.CalcTotalCost();
		////    this.totals.PercentGainLoss = this.CalcTotalCost() != 0 ? (double)(this.totals.GainLoss / this.CalcTotalCost()) : 0.00;
		////    this.totals.PriceChange = string.Empty;
		////    this.totals.PriceChangePercent = this.CalcPriceChangePercent().ToString();
		////    this.totals.YtdPercentGainLoss = this.CalYTDPriceChangePercentTotal();

		////    this.currentPositions.Add(this.totals);
		////}

		/// <summary>
		/// Returns the OpenPosition recored that matches the specified
		/// ticker symbol. If no matches returns null.
		/// </summary>
		/// <param name="symbol">Equity Symbol to match</param>
		/// <returns>OpenPositions object</returns>
		private ICurrentPosition GetPosition(string symbol)
        {
            return this.currentPositions.Find(delegate(ICurrentPosition pos) 
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
            List<ICurrentPosition> list = this.currentPositions.GetRange(0, this.currentPositions.Count() - 1);
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

            IEnumerable<ITransaction> transactions = from t in this.openTransactions
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
