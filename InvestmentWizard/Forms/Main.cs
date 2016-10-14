namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Main : Form, ITransactionsView
	{
        private IFinancialData financialDataClient = new YahooFinancalDataClient();
        private List<PriceQuote> prices = new List<PriceQuote>();
        private ITransactionController transactionController;
        private ICurrentPositionsModel currentPositionsModel;
        private IList<IList<string>> openTransactionsList;
        
        public Main(
            ICurrentPositionsModel currentPositionsModel,
            ITransactionController transactionController)
        {
            this.InitializeComponent();

            this.transactionController = transactionController;
			this.transactionController.TransactionView = this;
            this.currentPositionsModel = currentPositionsModel;
        }

		/// <summary>
		/// Passing the view handler to the controller
		/// </summary>
		/// <param name="handler">list change handler</param>
		public void RegisterCompleteTransactionList(out ListChangedEventHandler handler)
		{
			handler = new ListChangedEventHandler(this.OnTransactionListChanged);
		}

		/// <summary>
		/// Passing the view handler to the controller
		/// </summary>
		/// <param name="handler">list change handler</param>
		public void RegisterOpenTransactionList(out ListChangedEventHandler handler)
		{
			handler = new ListChangedEventHandler(this.OpenTransactionListChanged);
		}

		private void OnClick_UpdateQuotes(object sender, EventArgs e)
        {
            this.UpdateCurrentPositionsDataGridViewQuotes();
        }

        private void Main_Load(object sender, EventArgs e)
        {
			this.transactionController.Initialize();

            try
            {
                this.transactionController.Update();
                this.currentPositionsModel.UpdateList();
                var bindinglist2 = new BindingList<IOpenPositions>(this.currentPositionsModel.CurrentPositions);
                var source2 = new BindingSource(bindinglist2, null);
                this.dataGridViewCurPos.DataSource = source2;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Get the latest real-time stock prices and then uses them to populate
        /// the open positions list and update the data grid view
        /// </summary>
        private void UpdateCurrentPositionsDataGridViewQuotes()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.currentPositionsModel.Update();
                ////this.openPositions.BuildTotals();
               
                PriceQuote sp500 = this.GetSP500Quote();
                decimal sp500LastYear = Convert.ToDecimal(this.GetSP500QuoteYTD());
                double sp500YTD = (double)Math.Round((Convert.ToDecimal(sp500.PreviousClose) - sp500LastYear) / sp500LastYear * 100, 2);
                this.toolStripStatusLabel.Text = "Quotes Last Updated on:  "  + DateTime.Now.ToString();
                this.toolStripStatusLabel2.Text = sp500.Name + ": Today;";
                this.toolStripStatusLabel3.Text = sp500.PriceChangePercent;
                this.toolStripStatusLabel4.Text = " YTD;";
                this.toolStripStatusLabel5.Text = sp500YTD.ToString();
            }
            catch
            {
                MessageBox.Show("Failed to update current positions list.");
            }
            
            Cursor.Current = Cursors.Default;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewCurPos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string col = this.dataGridViewCurPos.Columns[e.ColumnIndex].Name;
            double value = 0;

            if ((col == "gainLoss") ||
                (col == "percentGainLoss") ||
                (col == "priceChange") ||
                (col == "YtdPercentGainLoss"))
            {
                try
                {
                    value = Convert.ToDouble(e.Value);
                }
                catch
                {
                    value = 0;
                }
            }
            else if (col == "PriceChangePercent")
            {
                try
                {
                    value = Convert.ToDouble((e.Value as string).Replace("%", string.Empty));
                }
                catch
                {
                    value = 0;
                }
            }
            else
            {
                return;
            }

            if (value >= 0)
            {
                e.CellStyle.ForeColor = Color.Green;
            }
            else
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }

        private void TabPageCurrentOpenPositions_Enter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.updateQuotes.Enabled = true;
            this.toolStripButtonAddTransaction.Enabled = false;
            this.toolStripButtonSellTransaction.Enabled = false;
            this.toolStripButtonSplit.Enabled = false;
            
            this.currentPositionsModel.UpdateList();
            this.UpdateCurrentPositionsDataGridViewQuotes();
            var bindinglist = new BindingList<IOpenPositions>(this.currentPositionsModel.CurrentPositions);
            var source = new BindingSource(bindinglist, null);
            this.dataGridViewCurPos.DataSource = source;
            
            Cursor.Current = Cursors.Default;
        }

        private void TabPageTransactions_Enter(object sender, EventArgs e)
        {
            this.updateQuotes.Enabled = false;
            this.toolStripButtonAddTransaction.Enabled = true;
            this.toolStripButtonSellTransaction.Enabled = true;
            this.toolStripButtonSplit.Enabled = true;
        }

        private void ToolStripButtonAddTransaction_Click(object sender, EventArgs e)
        {
            DlgBuyTransaction dlg = new DlgBuyTransaction();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!this.transactionController.AddPosition(dlg.Date, dlg.Stock, dlg.Quantity, dlg.Cost))
                {
                    MessageBox.Show("Could not add stock \"" + dlg.Stock + "\" to  transactions list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.transactionController.Update();
                }

                dlg.Close();
            }
        }

        /// <summary>
        /// Event handler for the sell transaction tool strip button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonSellTransaction_Click(object sender, EventArgs e)
        {
            List<string> sellComboBoxStrings = new List<string>();
            foreach (var o in this.openTransactionsList.AsEnumerable())
            {
                sellComboBoxStrings.Add(o[3] + " shares of " + o[1] + " (" + o[0] + ")");
            }

            DlgSell dlg = new DlgSell(sellComboBoxStrings);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IList<string> transaction = this.openTransactionsList[dlg.SelectedSellTransaction];

                if (dlg.Quantity <= Convert.ToDouble(transaction[2]))
                {
                    if (this.transactionController.SellPosition(dlg.SelectedSellTransaction, dlg.SaleDate, dlg.Quantity, dlg.SaleProceeds))
                    {
                        this.transactionController.Update();
                    }
                    else
                    {
                        MessageBox.Show("Could not sell stock \"" + transaction[1] + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Could not sell stock \"" + transaction[1] + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToolStripButtonAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            if (DialogResult.OK == aboutBox.ShowDialog())
            {
                aboutBox.Close();
            }
        }

        private void ToolStripButtonSplit_Click(object sender, EventArgs e)
        {
            DlgSplit dlg = new DlgSplit(this.openTransactionsList.Select(a => a[1]).Distinct().ToList());

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (this.transactionController.SplitPosition(dlg.SplitEquity, dlg.SplitRatio))
                {
                    this.transactionController.Update();
                }
                else
                {
                    MessageBox.Show("Could not split stock \"" + dlg.SplitEquity + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToolStripStatusLabel3_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.toolStripStatusLabel3.Text.TrimEnd('%')) < 0)
            {
                this.toolStripStatusLabel3.ForeColor = Color.Red;
            }
            else
            {
                this.toolStripStatusLabel3.ForeColor = Color.Green;
            }
        }

        private void ToolStripStatusLabel5_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.toolStripStatusLabel5.Text.TrimEnd('%')) < 0)
            {
                this.toolStripStatusLabel5.ForeColor = Color.Red;
            }
            else
            {
                this.toolStripStatusLabel5.ForeColor = Color.Green;
            }
        }

        /// <summary>
        /// Observer registered to model
        /// </summary>
        /// <param name="transactionsList">transactions list</param>
        private void OnTransactionListChanged(IList<IList<string>> transactionsList)
        {
            this.UpdateDataGrid(this.dataGridViewTransactions, transactionsList);
        }

        /// <summary>
        /// Observer registed to model
        /// </summary>
        /// <param name="openTransactionslist">open transactions list</param>
        private void OpenTransactionListChanged(IList<IList<string>> openTransactionslist)
        {
            this.openTransactionsList = openTransactionslist;
        }

        /// <summary>
        /// Updates a data grid view with a 2 deminsional list
        /// </summary>
        /// <param name="dataGridView">grid view to update</param>
        /// <param name="lists">2 deminsional list</param>
        private void UpdateDataGrid(DataGridView dataGridView, IList<IList<string>> lists)
        {
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