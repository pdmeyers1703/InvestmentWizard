// <copyright file="TransactionTableSchema.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	/// <summary>
	/// Schema information about transaction table
	/// </summary>
	public static class TransactionTableSchema
	{
		public const string TransactionTableName = "Transactions";

		private static string[] columnNames =
		{
			"ID",
			"Purchased Date",
			"Equity",
			"Quantity",
			"Cost Basis",
			"Sold Date",
			"Market Value",
			"Dividends",
		};

		public enum ColumnIndex
		{
			ID = 0,
			PurchaseDate,
			EquityName,
			Quantity,
			CostBasis,
			SoldDate,
			MarketValue,
			Dividends,
		}

		/// <summary>
		/// Column name string lookup
		/// </summary>
		/// <param name="index"></param>
		/// <returns>column name as a string</returns>
		public static string ColumnLookUp(ColumnIndex index)
		{
			return columnNames[(int)index];
		}
	}
}
