// <copyright file="IEquityQuoteReadModel.cs" company="Peter Meyers">
// Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	/// <summary>
	/// Interface for model that contains equity quotes
	/// </summary>
	public interface IEquityQuoteReadModel
	{
		/// <summary>
		/// Updates all quotes.
		/// </summary>
		void Update();

		/// <summary>
		///  Register one or more symbols with the model.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols(s)</param>
		void AddRealTimeQuote(string[] equitySymbols);

		/// <summary>
		/// Register one or more symbols with the model.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols.(s)</param>
		void AddHistoricalQuote(string[] equitySymbols);

		/// <summary>
		/// Register one or more symbols with the model.
		/// </summary>
		/// <param name="equitySymbols">Equity symbols.(s)</param>
		void AddYtdChange(string[] equitySymbols);

		/// <summary>
		///  Get a quote based on the equities symbol
		/// </summary>
		/// <param name="equitySymbol"></param>
		/// <returns>Realtime Price.</returns>
		PriceQuote GetRealTimeQuote(string equitySymbol);

		/// <summary>
		///  Get a quote based on the equities symbol
		/// </summary>
		/// <param name="equitySymbol"></param>
		/// <returns>Realtime Price.</returns>
		string GetHistoricalQuote(string equitySymbol);

		/// <summary>
		/// Gets the absolute difference between the current price and 
		/// the price at the beginning of the current year.
		/// </summary>
		/// <param name="equitySymbol">Equity symbol.</param>
		/// <returns>Absolute price different as a string</returns>
		string GetYtdPriceChanged(string equitySymbol);
	}
}
