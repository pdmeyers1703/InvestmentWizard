// <copyright file="ITransactionController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;

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
		/// Sell open position
		/// </summary>
		/// <param name="rowIndex">The row indexof holding to sell</param>
		/// <param name="saleDate">Date of sale.</param>
		/// <param name="quantity">Number of shares sold.</param>
		/// <param name="saleProceeds">Total proceeds of sale.</param>	
		void SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

		/// <summary>
		/// Current holding is split
		/// </summary>
		/// <param name="equitySymbol">Stock to be split.</param>
		/// <param name="splitRatio">The share split ratio</param>
		void SplitPosition(string equitySymbol, double splitRatio);
	}
}
