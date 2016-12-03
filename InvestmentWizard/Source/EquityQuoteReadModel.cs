// <copyright file="EquityQuoteReadModel.cs" company="Peter Meyers">
// Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Handles the updating of real-time and historical financial data.
	/// </summary>
	public class EquityQuoteReadModel : IListObservable<PriceQuote>, IEquityQuoteReadModel
	{
		private IFinancialData financialData;

		private Dictionary<string, PriceQuote> realtimePriceQuotes;

		/// <summary>
		///  Constructor
		/// </summary>
		/// <param name="financialData">Source of the realtime quotes</param>
		public EquityQuoteReadModel(IFinancialData financialData)
		{
			this.financialData = financialData;
			this.realtimePriceQuotes = new Dictionary<string, PriceQuote>();
		}

		/// <summary>
		/// Handler that is called when the financial data is updated.
		/// </summary>
		private event ListChangedEventHandler<PriceQuote> QuoteUpdateHandler;

		/// <summary>
		///  Register event handler for price quotes.
		/// </summary>
		/// <param name="handler">Event handler </param>
		public void RegisterObserver(ListChangedEventHandler<PriceQuote> handler)
		{
			this.QuoteUpdateHandler += handler;
		}

		/// <summary>
		/// Updates all quotes
		/// </summary>
		public void Update()
		{
			List<string> equitySymbols = this.realtimePriceQuotes.Keys.ToList();

			if (equitySymbols.Count() > 0)
			{
				List<PriceQuote> priceQuotes;
				this.financialData.GetPrices(equitySymbols, out priceQuotes);
				this.realtimePriceQuotes = priceQuotes.ToDictionary(q => q.Symbol, x => x);

				this.QuoteUpdateHandler(priceQuotes);
			}
		}

		/// <summary>
		///  Addes equity symbols to symbol repository.
		/// </summary>
		/// <param name="equitySymbols"Equity symbols(s)</param>
		public void AddRealTimeQuote(string[] equitySymbols)
		{ 
			foreach (var s in equitySymbols)
			{
				this.realtimePriceQuotes.Add(s, new PriceQuote());
			}
		}

		/// <summary>
		/// Gets a sigle quote
		/// </summary>
		/// <param name="equitySymbol">Equity symbol of quote</param>
		/// <returns>Price quote of equity symbol</returns>
		public PriceQuote GetRealTimeQuote(string equitySymbol)
		{
			if (this.realtimePriceQuotes.ContainsKey(equitySymbol))
			{
				return this.realtimePriceQuotes[equitySymbol];
			}
			else
			{
				return null;
			}
		}
	}
}
