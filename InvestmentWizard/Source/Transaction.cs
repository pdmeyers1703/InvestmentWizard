// <copyright file="Transaction.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Container for a single transaction
	/// </summary>
	public class Transaction : ITransaction
	{
		/// <summary>
		/// Auto increment row id from database.
		/// </summary>
		public int RowID { get; set; }

		/// <summary>
		/// Date of Purchased (can be null),
		/// </summary>
		public DateTime? PurchasedDate { get; set; }

		/// <summary>					 .
		/// Ticker symbol of equity
		/// </summary>
		public string EquitySymbol { get; set; }

		/// <summary>
		/// Number of shares (can be partial shares).
		/// </summary>
		public double Quanity { get; set; }

		/// <summary>
		/// Price of equity (calculated).
		/// </summary>
		public decimal PurchasePrice
		{
			get
			{
				try
				{
					return Math.Round((decimal)((double)this.Cost / this.Quanity), 2);
				}
				catch (DivideByZeroException)
				{
					return 0.00m;
				}
			}
		}

		/// <summary>
		/// Total cost of purchase.
		/// </summary>
		public decimal Cost { get; set; }

		/// <summary>
		/// Date of the sale (can be null).
		/// </summary>
		public DateTime? SaleDate { get; set; }

		/// <summary>
		/// Price of stock when sold (can be null).
		/// </summary>
		public decimal? SalePrice 
		{
			get
			{
				try
				{
					return Math.Round((decimal)((double)this.SaleProceeds / this.Quanity), 2);
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Total proceeds from the sale of the stock (can be null).
		/// </summary>
		public decimal? SaleProceeds { get; set; }

		/// <summary>
		/// Total acculated dividends during ownership (can be null).
		/// </summary>
		public decimal? Dividends { get; set; }

		/// <summary>
		/// Converts itself to a list of strings for display purposes.
		/// </summary>
		/// <returns>A list of strings</returns>
		public IList<string> ToStringList()
		{
			List<string> list = new List<string>();
			list.Add(this.PurchasedDate.HasValue ? this.PurchasedDate.Value.ToShortDateString() : null);
			list.Add(this.EquitySymbol);
			list.Add(this.Quanity.ToString());
			list.Add(this.PurchasePrice.ToString());
			list.Add(this.Cost.ToString());
			list.Add(this.SaleDate.HasValue ? this.SaleDate.Value.ToShortDateString() : null);
			list.Add(this.SalePrice.ToString());
			list.Add(this.SaleProceeds.ToString());
			list.Add(this.Dividends.ToString());
			return list;
		}
	}
}
