// <copyright file="ITransaction.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Interface for a single transaction
	/// </summary>
	public interface ITransaction
	{
		/// <summary>
		/// Auto increment row id from database.
		/// </summary>
		int RowID { get; set; }

		/// <summary>
		/// Date of Purchased (can be null),
		/// </summary>
		DateTime? PurchasedDate { get; set; }

		/// <summary>					 .
		/// Ticker symbol of equity
		/// </summary>
		string EquitySymbol { get; set; }

		/// <summary>
		/// Number of shares (can be partial shares).
		/// </summary>
		double Quanity { get; set; }

		/// <summary>
		/// Price of equity .
		/// </summary>
		decimal PurchasePrice { get; }

		/// <summary>
		/// Total cost of purchase.
		/// </summary>
		decimal Cost { get; set; }

		/// <summary>
		/// Date of the sale (can be null).
		/// </summary>
		DateTime? SaleDate { get; set; }

		/// <summary>
		/// Price of stock when sold (can be null).
		/// </summary>
		decimal? SalePrice { get; }

		/// <summary>
		/// Total proceeds from the sale of the stock (can be null).
		/// </summary>
		decimal? SaleProceeds { get; set; }

		/// <summary>
		/// Total acculated dividends during ownership (can be null).
		/// </summary>
		decimal? Dividends { get; set; }

		/// <summary>
		/// Converts itself to a list of strings for display purposes.
		/// </summary>
		/// <returns>A list of strings</returns>
		IList<string> ToStringList();
	}
}
