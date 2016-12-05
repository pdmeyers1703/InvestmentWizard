// <copyright file="EquityQuoteReadModel.cs" company="Peter Meyers">
// Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Handles the updating of real-time and historical financial data.
	/// </summary>
	public class EquityQuoteReadModel : IEquityQuoteReadModel
	{
		private IFinancialData financialData;

		private Dictionary<string, PriceQuote> realtimePriceQuotes;
		private Dictionary<string, string> historicalPriceQuotes;

		/// <summary>
		///  Constructor
		/// </summary>
		/// <param name="financialData">Source of the realtime quotes</param>
		public EquityQuoteReadModel(IFinancialData financialData)
		{
			this.financialData = financialData;
			this.realtimePriceQuotes = new Dictionary<string, PriceQuote>();
			this.historicalPriceQuotes = new Dictionary<string, string>();
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
			}

			List<string> historicalEquitySymbols = this.historicalPriceQuotes.Keys.ToList();

			foreach (var symbol in historicalEquitySymbols)
			{
				string price = string.Empty;
				this.financialData.GetHistoricalPrice(symbol, DateTimeHelper.GetYTD(), out price);
				this.historicalPriceQuotes[symbol] = price;
			}
		}

		/// <summary>
		///  Adds equity symbols the to realtime repository.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		public void AddRealTimeQuote(string[] equitySymbols)
		{ 
			foreach (var s in equitySymbols)
			{
				this.realtimePriceQuotes.Add(s, new PriceQuote());
			}
		}

		/// <summary>
		/// Adds equity symbols to the histroical repository.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		public void AddHistoricalQuote(string[] equitySymbols)
		{
			foreach (var s in equitySymbols)
			{
				this.historicalPriceQuotes.Add(s, string.Empty);
			}
		}

		/// <summary>
		/// Adds equity symbols to the realtime and historical reposistories
		/// so that the difference between now and ytd can be calcualted.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		public void AddYtdChange(string[] equitySymbols)
		{
			this.AddRealTimeQuote(equitySymbols);
			this.AddHistoricalQuote(equitySymbols);
		}

		/// <summary>
		/// Gets a single realtime price quote
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

		/// <summary>
		/// Gets a sigle historical price quote
		/// </summary>
		/// <param name="equitySymbol">Equity symbol of quote</param>
		/// <returns>Price quote of equity symbol</returns>
		public string GetHistoricalQuote(string equitySymbol)
		{
			if (this.historicalPriceQuotes.ContainsKey(equitySymbol))
			{
				return this.historicalPriceQuotes[equitySymbol];
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets the absolute difference between the current price and 
		/// the price at the beginning of the current year.
		/// </summary>
		/// <param name="equitySymbol">Equity symbol.</param>
		/// <returns>Absolute price different as a string</returns>
		public string GetYtdPriceChanged(string equitySymbol)
		{
			PriceQuote currentQuote = this.GetRealTimeQuote(equitySymbol);
			string ytdPrice = this.GetHistoricalQuote(equitySymbol);

			if (currentQuote != null && ytdPrice != string.Empty)
			{
				return Math.Round(currentQuote.LastPrice - DataConverter.Decimal(ytdPrice), 2).ToString();
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
