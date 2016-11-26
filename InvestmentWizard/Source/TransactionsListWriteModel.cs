// <copyright file="TransactionsListWriteModel.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Data;
	using System.Linq;

	/// <summary>
	/// Implements write support to transactions model
	/// </summary>
	public class TransactionsListWriteModel : ITransactionsListWriter
	{
		private IDatabase database;

		public TransactionsListWriteModel(IDatabase database)
		{
			this.database = database;
		}

		/// <summary>
		/// Addes a new transaction that was purchased
		/// </summary>
		/// <param name="date">date of transaction</param>
		/// <param name="stock">Stock purchasedd</param>
		/// <param name="quantity">number of shares purchased</param>
		/// <param name="cost">total cost of the transaction</param>
		public void Add(DateTime date, string stock, double quantity, decimal cost)
		{
				string[] columns =
				{
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.PurchaseDate),
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.EquityName),
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity),
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.CostBasis)
				};
				dynamic[] values = { date.ToShortDateString(), stock, quantity, cost };
				this.database.AddRecord(TransactionTableSchema.TransactionTableName, columns, values);
		}

		/// <summary>
		/// Update transaction infomation when the stock is sold
		/// </summary>
		/// <param name="rowIndex">index of the sold stock in the transaction table</param>
		/// <param name="saleDate">date of sale</param>
		/// <param name="quantity">quantity sold</param>
		/// <param name="saleProceeds">total proceeds  of sale</param>
		/// <returns></returns>
		public void Sell(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds)
		{
			DataTable dt = this.database.GetRows(
				TransactionTableSchema.TransactionTableName,
				TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.ID),
				rowIndex);

			if (dt.Rows.Count != 1)
			{
				throw new Exception("Error: Transaction to sell was not found!");
			}
			else
			{
				DataRow dr = dt.Rows[0];
				////List<decimal> dividends = this.GetDividend(dr[3].ToString(), this.ConvertDate(dr[1].ToString()), this.ConvertDate(dr[5].ToString()));
				string[] columns =
				{
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.SoldDate),
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.MarketValue),
				};

				dynamic[] values = { saleDate.ToShortDateString(), saleProceeds };
				this.database.UpdateRecord(TransactionTableSchema.TransactionTableName, rowIndex, columns, values);
			}
		}

		/// <summary>
		/// Ajusts a stock share count for a stock split
		/// /// </summary>
		/// <param name="equitySymbol">stock to be split</param>
		/// <param name="splitRatio">ration of of the split</param>
		/// <returns></returns>
		public void Split(string equitySymbol, double splitRatio)
		{
			DataTable dt = this.database.GetRows(
				TransactionTableSchema.TransactionTableName, 
				TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.EquityName), 
				equitySymbol);

			string[] columns = { TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity) };
			foreach (var row in dt.AsEnumerable())
			{
				string[] values = 
				{
					(Convert.ToDouble(row[TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity)]) * splitRatio).ToString()
				};

				this.database.UpdateRecord(
					TransactionTableSchema.TransactionTableName, 
					Convert.ToInt32(row[TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.ID)]), 
					columns, 
					values);
			}
		}
}
}
