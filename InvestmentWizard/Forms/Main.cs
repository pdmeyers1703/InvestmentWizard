// <copyright file="Main.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright> System;

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	public partial class Main : Form, ITransactionsView, ICurrentPositionsView
	{
		private IFinancialData financialDataClient = new YahooFinancalDataClient();
		private ITransactionController transactionController;
		private ICurrentPositionsController currentPositionsController;
		private IList<ITransaction> openTransactionsList;
		private IViewFormatter<ICurrentPosition> currentpositionsFormatter;

		/// <summary>
		/// Constructor for main form.
		/// </summary>
		/// <param name="transactionController">The controller for all transactions</param>
		/// <param name="currentPositionsController">The controller for current positions.</param>
		/// <param name="currentpositionsFormatter">Formats data for current positions view</param>
		public Main(
			ITransactionController transactionController,
			ICurrentPositionsController currentPositionsController,
			IViewFormatter<ICurrentPosition> currentpositionsFormatter)
		{
			this.InitializeComponent();

			this.transactionController = transactionController;
			this.transactionController.TransactionView = this;
			this.currentPositionsController = currentPositionsController;
			this.currentPositionsController.CurrentPositionsView = this;

			this.currentpositionsFormatter = currentpositionsFormatter;
		}

		/// <summary>
		/// Registers the transactions handler (observer)
		/// </summary>
		/// <param name="handler">List change handler,</param>
		public void RegisterCompleteTransactionList(out ListChangedEventHandler<ITransaction> handler)
		{
			handler = new ListChangedEventHandler<ITransaction>(this.OnTransactionListChanged);
		}

		/// <summary>
		/// Registers the open transactions handler (observer)
		/// </summary>
		/// <param name="handler">List change handler.</param>
		public void RegisterOpenTransactionList(out ListChangedEventHandler<ITransaction> handler)
		{
			handler = new ListChangedEventHandler<ITransaction>(this.OpenTransactionListChanged);
		}

		/// <summary>
		/// Registers the current positions handler (observer)
		/// </summary>
		/// <param name="handler">List change handler.</param>
		public void RegisterCurrentPositionsList(out ListChangedEventHandler<ICurrentPosition> handler)
		{
			handler = new ListChangedEventHandler<ICurrentPosition>(this.CurrentPositionsListChanged);
		}

		/// <summary>
		/// Event handler to refresh quotes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClick_UpdateQuotes(object sender, EventArgs e)
		{
			this.UpdateCurrentPositions();
		}

		/// <summary>
		/// Event handler when main winform loads.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Load(object sender, EventArgs e)
		{
			this.transactionController.Initialize();
			this.currentPositionsController.Initialize();

			try
			{
				this.transactionController.Update();
			}
			catch
			{
				MessageBox.Show("Could not load transactions!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Get the latest real-time stock prices and then uses them to populate
		/// the open positions list and update the data grid view
		/// </summary>
		private void UpdateCurrentPositions()
		{
			string previousLastQuoteUpdate = this.lastQuoteUpdateStatusLabel.Text;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				this.lastQuoteUpdateStatusLabel.Text = "Updating Quotes...";

				PriceQuote sp500 = this.GetSP500Quote();
				decimal sp500LastYear = Convert.ToDecimal(this.GetSP500QuoteYTD());
				double sp500YTD = (double)Math.Round((Convert.ToDecimal(sp500.PreviousClose) - sp500LastYear) / sp500LastYear * 100, 2);
				this.sp00TodayTextStatusLabel.Text = sp500.Name + ": Today - ";
				this.sp500TodayValueStatusLabel.Text = sp500.PriceChangePercent;
				this.sp00YtdTextStatusLabel.Text = " YTD - ;";
				this.sp500YtdValueStatusLabel.Text = sp500YTD.ToString();

				this.currentPositionsController.Update();
			}
			catch
			{
				MessageBox.Show("Could update current positions!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.lastQuoteUpdateStatusLabel.Text = previousLastQuoteUpdate;
			}

			Cursor.Current = Cursors.Default;
			this.lastQuoteUpdateStatusLabel.Text = "Quotes Last Updated on:  " + DateTime.Now.ToString();
		}

        /// <summary>
        /// Retrieves the S&P 500 Index quote
        /// </summary>
        /// <returns>Price Quote</returns>
        private PriceQuote GetSP500Quote()
        {
            List<string> ticker = new List<string>();
            List<PriceQuote> standardAndPoors500Qoute = new List<PriceQuote>();
            ticker.Add("^gspc");
            this.financialDataClient.GetPrices(ticker, out standardAndPoors500Qoute);
            return standardAndPoors500Qoute[0];
        }

        /// <summary>
        /// Retrieves the S&P 500 Index quote from the last
        /// day of the previous year
        /// </summary>
        /// <returns>price</returns>
        private string GetSP500QuoteYTD()
        {
            string price = null;
            this.financialDataClient.GetHistoricalPrice("^gspc", DateTimeHelper.GetYTD(), ref price);
            return price;
        }

		/// <summary>
		/// Determines if certain columns should have green or red text
		/// </summary>
		/// <param name="sender">sender object.</param>
		/// <param name="e">Event argument.</param>
		private void DataGridViewCurPos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			string col = this.dataGridViewCurPos.Columns[e.ColumnIndex].Name;

			if (col == "gainLoss" ||
				col == "priceChange" |
				col == "PriceChangePercent" ||
				col == "YtdPercentGainLoss" ||
				col == "percentGainLoss")
			{
				e.CellStyle.ForeColor = this.currentpositionsFormatter.GetTextColor(e.Value as string);
			}
		}

		/// <summary>
		/// Event handler to apply correct text color when sp500 daily % changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SP00TodayValueStatusLabel_TextChanged(object sender, EventArgs e)
		{
			this.sp500TodayValueStatusLabel.ForeColor =
				this.currentpositionsFormatter.GetTextColor(this.sp500TodayValueStatusLabel.Text);
		}

		/// <summary>
		/// Event handler to apply correct text color when sp500 ytd % changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SP500YtdValueStatusLabel_TextChanged(object sender, EventArgs e)
		{
			this.sp500YtdValueStatusLabel.ForeColor =
				this.currentpositionsFormatter.GetTextColor(this.sp500YtdValueStatusLabel.Text);
		}
		
		/// <summary>
		/// Event handler when the current positions data grid view is entered.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPageCurrentOpenPositions_Enter(object sender, EventArgs e)
		{
			this.updateQuotes.Enabled = true;
			this.toolStripButtonAddTransaction.Enabled = false;
			this.toolStripButtonSellTransaction.Enabled = false;
			this.toolStripButtonSplit.Enabled = false;

			this.UpdateCurrentPositions();
		}

		/// <summary>
		/// Event handler when the transactions data grid view is entered.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPageTransactions_Enter(object sender, EventArgs e)
		{
			this.updateQuotes.Enabled = false;
			this.toolStripButtonAddTransaction.Enabled = true;
			this.toolStripButtonSellTransaction.Enabled = true;
			this.toolStripButtonSplit.Enabled = true;
		}

		/// <summary>
		/// Shows the Add transaction dialog box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonAddTransaction_Click(object sender, EventArgs e)
		{
			DlgBuyTransaction dlg = new DlgBuyTransaction(this.transactionController);
			dlg.ShowDialog();
		}

		/// <summary>
		/// Event handler for the sell transaction tool strip button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonSellTransaction_Click(object sender, EventArgs e)
		{
			DlgSell dlg = new DlgSell(this.openTransactionsList, this.transactionController);
			dlg.ShowDialog();
		}

		/// <summary>
		/// Event hanlder for the split transaction tool strip button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonSplit_Click(object sender, EventArgs e)
		{
			DlgSplit dlg = new DlgSplit(
				this.openTransactionsList.Select(a => a.EquitySymbol).Distinct().ToList(),
				this.transactionController);
			dlg.ShowDialog();
		}

		/// <summary>
		/// Event handler for the about diaglog box tool strip button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonAbout_Click(object sender, EventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			if (DialogResult.OK == aboutBox.ShowDialog())
			{
				aboutBox.Close();
			}
		}
		
		/// <summary>
		/// Observer registered to model
		/// </summary>
		/// <param name="transactionsList">transactions list</param>
		private void OnTransactionListChanged(IList<ITransaction> transactionsList)
		{
			IList<IList<string>> displayableList = new List<IList<string>>();

			foreach (var transaction in transactionsList)
			{
				displayableList.Add(transaction.ToStringList());
			}

			this.UpdateDataGrid(this.dataGridViewTransactions, displayableList);
		}

		/// <summary>
		/// Observer registered to model
		/// </summary>
		/// <param name="openTransactionslist">Open transactions list.</param>
		private void OpenTransactionListChanged(IList<ITransaction> openTransactionslist)
		{
			this.openTransactionsList = openTransactionslist;
		}

		/// <summary>
		/// Observer registered to model
		/// </summary>
		/// <param name="currentPositions">Current poistion list</param>
		private void CurrentPositionsListChanged(IList<ICurrentPosition> currentPositions)
		{
			IList<IList<string>> displayableList = new List<IList<string>>();

			foreach (var position in currentPositions)
			{
				displayableList.Add(this.currentpositionsFormatter.FormatDataToStringList(position));
			}

			this.UpdateDataGrid(this.dataGridViewCurPos, displayableList);
		}

		/// <summary>
		/// Updates a data grid view with a 2 deminsional list
		/// </summary>
		/// <param name="dataGridView">grid view to update</param>
		/// <param name="lists">2 deminsional list</param>
		private void UpdateDataGrid(DataGridView dataGridView, IList<IList<string>> lists)
		{
			dataGridView.Rows.Clear();

			foreach (var r in lists)
			{
				var row = new DataGridViewRow();
				dataGridView.ColumnCount = r.Count;
				for (int i = 0; i < r.Count; ++i)
				{
					row.Cells.Add(new DataGridViewTextBoxCell() { Value = r[i] });
				}

				dataGridView.Rows.Add(row);
			}

			dataGridView.Update();
		}
	}
}