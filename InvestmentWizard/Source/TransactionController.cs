// <copyright file="TransactionController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Controller for all transactions
	/// </summary>
	public class TransactionController : ITransactionController
	{
		/// <summary>
		/// private Members
		/// </summary>
        private IListObservable<ITransaction> transactionsObserver;
        private IListObservable<ITransaction> openTransactionsObserver;
		private ITransactionsListWriter transactionWriter;
		private ITransactionsView transactionView;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="transactionsObserver">observer for entire transactions list </param>
		/// <param name="openTransactionsObserver">observer for all open transactions</param>
		public TransactionController(
		IListObservable<ITransaction> transactionsObserver, 
		IListObservable<ITransaction> openTransactionsObserver,
		ITransactionsListWriter transactionWriter) 
	{
			this.transactionsObserver = transactionsObserver;
			this.openTransactionsObserver = openTransactionsObserver;
			this.transactionWriter = transactionWriter;
		}

		/// <summary>
		/// Sets the view for the controller
		/// </summary>
		public ITransactionsView TransactionView
		{
			get
			{
				return this.transactionView;
			}

			set
			{
				this.transactionView = value;
			}
		}

		/// <summary>
		/// Initialize controller features
		/// </summary>
		public void Initialize()
		{
			this.RegisterModelsWithView();
		}

		/// <summary>
		/// Updates the models
		/// </summary>
		public void Update()
		{
			this.transactionsObserver.Update();
			this.openTransactionsObserver.Update();
		}

        public List<ITransaction> GetTransactionForThisYear(string symbol)
        {
            return new List<ITransaction>(); ///this.transactionsObserver.Transactions.Where(a => a.EquitySymbol == symbol)
                                            ///         .Where(b => b.SaleDate == null || 
                                            ///             b.SaleDate.Value.Year == DateTime.Now.Year).ToList();
        }

		/// <summary>
		/// Add new transaction (buy)
		/// </summary>
		/// <param name="date">Date of purchase.</param>
		/// <param name="stock">Stock purchased.</param>
		/// <param name="quantity">Number of shares purchased.</param>
		/// <param name="cost">Total cost basis.</param>
		public void AddPosition(DateTime date, string stock, double quantity, decimal cost)
		{
			this.transactionWriter.Add(date, stock, quantity, cost);
			this.Update();
		}

		/// <summary>
		/// Sell open positions
		/// </summary>
		/// <param name="transactions">List of transactions to be sold.</param>
		/// <param name="saleDate">Date of sale.</param>
		/// <param name="saleProceeds">Total proceeds of sale.</param>
		public void SellPositions(IList<ITransaction> transactions, DateTime saleDate, decimal saleProceeds)
		{
			foreach (var transaction in transactions)
			{
				this.transactionWriter.Sell(
					transaction.RowID, 
					saleDate, 
					transaction.Quanity, 
					Math.Round(saleProceeds * (decimal)(transaction.Quanity / transactions.Sum(t => t.Quanity)), 2));
			}

			this.Update();
		}

		/// <summary>
		/// Current holding is split
		/// </summary>
		/// <param name="equitySymbol">Stock to be split.</param>
		/// <param name="splitRatio">The share split ratio</param>
		public void SplitPosition(string equitySymbol, double splitRatio)
		{
			this.transactionWriter.Split(equitySymbol, splitRatio);
			this.Update();
		}

		/// <summary>
		/// Register all view observers to the model									 
		/// </summary>
		private void RegisterModelsWithView()
		{
			ListChangedEventHandler<ITransaction> completeTransactionHandler;
			this.transactionView.RegisterCompleteTransactionList(out completeTransactionHandler);
			this.transactionsObserver.RegisterObserver(completeTransactionHandler);

			ListChangedEventHandler<ITransaction> openTransactionHandler;
			this.transactionView.RegisterOpenTransactionList(out openTransactionHandler);
			this.openTransactionsObserver.RegisterObserver(openTransactionHandler);
		}
													
		/// <summary>
		/// Returns all the dividend payment for an equity between a particular time range
		/// </summary>
		/// <param name="tickerSymbol">Equity symbol</param>
		/// <param name="startDate"> Begining of time range</param>
		/// <param name="endDate">End of time range</param>
		/// <returns>List of dividend payments</returns>
		////private List<decimal> GetDividend(string tickerSymbol, DateTime? startDate, DateTime? endDate)
		////{
		////	List<decimal> dividends = new List<decimal>();
		////	if (startDate == null)
		////	{
		////		throw new Exception();
		////	}

		////	if (endDate == null)
		////	{
		////		endDate = DateTime.Now;
		////	}

		////	this.server.GetDividendsOverTimeSpan(tickerSymbol, startDate.Value, endDate.Value, ref dividends);
		////	return dividends;
		////}
	} 
}
