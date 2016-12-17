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
		private Dictionary<Tuple<string, DateTime>, string> historicalPriceQuotes;

		/// <summary>
		///  Constructor
		/// </summary>
		/// <param name="financialData">Source of the realtime quotes</param>
		public EquityQuoteReadModel(IFinancialData financialData)
		{
			this.financialData = financialData;
			this.realtimePriceQuotes = new Dictionary<string, PriceQuote>();
			this.historicalPriceQuotes = new Dictionary<Tuple<string, DateTime>, string>();
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

			List<Tuple<string, DateTime>> historicalEquitySymbolsAndDate = this.historicalPriceQuotes.Keys.ToList();

			foreach (var symbolAndDate in historicalEquitySymbolsAndDate)
			{
				string price;
				this.financialData.GetHistoricalPrice(symbolAndDate.Item1, symbolAndDate.Item2, out price);
				this.historicalPriceQuotes[symbolAndDate] = price;
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
				this.realtimePriceQuotes[s] = new PriceQuote();
			}
		}

		/// <summary>
		/// Adds equity symbols to the histroical repository.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		public void AddHistoricalQuote(Tuple<string, DateTime>[] equitySymbolsAndDate)
		{
			foreach (var symbolAndDate in equitySymbolsAndDate)
			{
				this.historicalPriceQuotes[new Tuple<string, DateTime>(symbolAndDate.Item1, symbolAndDate.Item2)] = string.Empty;
			}
		}

		/// <summary>
		/// Adds equity symbols to the realtime and historical reposistories
		/// so that the difference between now and ytd can be calcualted.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		public void AddYtdChange(Tuple<string, DateTime>[] equitySymbolsAndDate)
		{
			this.AddRealTimeQuote(equitySymbolsAndDate.Select(s => s.Item1).ToArray());
			this.AddHistoricalQuote(equitySymbolsAndDate);
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
		public string GetHistoricalQuote(Tuple<string, DateTime> equitySymbolsAndDate)
		{
			if (this.historicalPriceQuotes.ContainsKey(equitySymbolsAndDate))
			{
				return this.historicalPriceQuotes[equitySymbolsAndDate];
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
		public string GetYtdPriceChanged(Tuple<string, DateTime> equitySymbolsAndDate)
		{
			PriceQuote currentQuote = this.GetRealTimeQuote(equitySymbolsAndDate.Item1);
			string ytdPrice = this.GetHistoricalQuote(equitySymbolsAndDate);

			if (currentQuote != null && ytdPrice != string.Empty)
			{
				return Math.Round(currentQuote.LastPrice - DataConverter.Decimal(ytdPrice), 2).ToString("0.00");
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets the absolute percent difference between the current price and 
		/// the price at the beginning of the current year.
		/// </summary>
		/// <param name="equitySymbol">Equity symbol.</param>
		/// <returns>Absolute price different as a string</returns>
		public string GetYtdPriceChangedPercent(Tuple<string, DateTime> equitySymbolsAndDate)
		{
			string priceChange = this.GetYtdPriceChanged(equitySymbolsAndDate);
			string beginingOfYearPrice = this.GetHistoricalQuote(equitySymbolsAndDate);

			if (priceChange != null && beginingOfYearPrice != null)
			{
				if (DataConverter.Decimal(beginingOfYearPrice) != 0.00m)
				{
					decimal percentageChange = Math.Round(DataConverter.Decimal(priceChange) / DataConverter.Decimal(beginingOfYearPrice) * 100, 2);
					return percentageChange.ToString("0.00");
				}
				else
				{
					return "0.00";
				}
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
