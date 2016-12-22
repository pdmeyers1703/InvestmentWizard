// <copyright file="ITransactionController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Controller for transaction model
	/// </summary>
	public interface ITransactionController
	{
		/// <summary>
		/// Sets controllers view
		/// </summary>
		 ITransactionsView TransactionView { get; set; }

		/// <summary>
		/// Initialize controller features
		/// </summary>
		void Initialize();

		/// <summary>
		/// Updates the models
		/// </summary>
		void Update();

		/// <summary>
		/// Add new transaction (buy)
		/// </summary>
		/// <param name="date">Date of purchase.</param>
		/// <param name="stock">Stock purchased.</param>
		/// <param name="quantity">Number of shares purchased.</param>
		/// <param name="cost">Total cost basis.</param>
		void AddPosition(DateTime date, string stock, double quantity, decimal cost);

		/// <summary>
		/// Sell open positions
		/// </summary>
		/// <param name="transactions">List of transactions to be sold.</param>
		/// <param name="saleDate">Date of sale.</param>
		/// <param name="saleProceeds">Total proceeds of sale.</param>
		void SellPositions(IList<ITransaction> transactions, DateTime saleDate, decimal saleProceeds);

		/// <summary>
		/// Current holding is split
		/// </summary>
		/// <param name="equitySymbol">Stock to be split.</param>
		/// <param name="splitRatio">The share split ratio</param>
		void SplitPosition(string equitySymbol, double splitRatio);
	}
}
