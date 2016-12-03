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
		///  Register one or more symbols with the model
		/// </summary>
		/// <param name="equitySymbols"></param>
		void AddRealTimeQuote(string[] equitySymbols);

		/// <summary>
		///  Get a quote based on the equities symbol
		/// </summary>
		/// <param name="equitySymbol"></param>
		/// <returns></returns>
		PriceQuote GetRealTimeQuote(string equitySymbol);
	}
}
