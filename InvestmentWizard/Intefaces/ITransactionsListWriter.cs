// <copyright file="ITransactionsListWriter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright> System;

namespace InvestmentWizard
{
	using System;

	/// <summary>
	/// Interface that writes to transactions model
	/// </summary>
	public interface ITransactionsListWriter
	{
		/// <summary>
		/// Addes a new transaction that was purchased
		/// </summary>
		/// <param name="date">date of transaction</param>
		/// <param name="stock">Stock purchasedd</param>
		/// <param name="quantity">number of shares purchased</param>
		/// <param name="cost">total cost of the transaction</param>
		void Add(DateTime date, string stock, double quantity, decimal cost);

		/// <summary>
		/// Update transaction infomation when the stock is sold
		/// </summary>
		/// <param name="rowIndex">index of the sold stock in the transaction table</param>
		/// <param name="saleDate">date of sale</param>
		/// <param name="quantity">quantity sold</param>
		/// <param name="saleProceeds">total proceeds  of sale</param>
		/// <returns></returns>
		void Sell(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

		/// <summary>
		/// Ajusts a stock share count for a stock split
		/// /// </summary>
		/// <param name="equitySymbol">stock to be split</param>
		/// <param name="splitRatio">ration of of the split</param>
		/// <returns></returns>
		void Split(string equitySymbol, double splitRatio);
	}
}
