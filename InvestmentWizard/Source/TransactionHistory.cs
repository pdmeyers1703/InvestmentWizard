// <copyright file="TransactionHistory.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;

	public class Transaction : ITransaction
	{
		public int RowID { get; set; }

		public DateTime? PurchasedDate { get; set; }

		public string EquitySymbol { get; set; }

		public double Quanity { get; set; }

		public decimal PurchasePrice
		{
			get
			{
				try
				{
					if (this.Quanity == 0)
					{
						return 0;
					}
					else
					{
						return Math.Round((decimal)((double)this.Cost / this.Quanity), 2);
					}
				}
				catch (DivideByZeroException ex)
				{
					throw ex;
				}
			}
		}

		public decimal Cost { get; set; }

		public DateTime? SaleDate { get; set; }

		public decimal? SalePrice 
		{
			get
			{
				try
				{
					if (this.SaleProceeds == null || this.Quanity == 0)
					{
						return null;
					}
					else
					{
						return Math.Round((decimal)((double)this.SaleProceeds / this.Quanity), 2);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public decimal? SaleProceeds { get; set; }

		public decimal? Dividends { get; set; }

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
