﻿namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Main : Form
    {
        private IFinancialData financialDataClient = new YahooFinancalDataClient();
        private List<PriceQuote> prices = new List<PriceQuote>();
        private TransactionController transactionController;
        private ICurrentPositionsModel currentPositionsModel;

        public Main(ICurrentPositionsModel currentPositionsModel)
        {
            this.InitializeComponent();

            ITransactionsModel transactionModel = TransactionModelFactory.Create();
            this.transactionController = new TransactionController(transactionModel, this.financialDataClient);
            this.currentPositionsModel = currentPositionsModel;
        }

        private void OnClick_UpdateQuotes(object sender, EventArgs e)
        {
            this.UpdateCurrentPositionsDataGridViewQuotes();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            IDatabase database = Program.MyContainer.GetInstance<IDatabase>();
            database.DatabasePath = "..\\..\\DataBases\\DataStore.accdb";
            try
            {
                this.transactionController.Update();
                var bindinglist = new BindingList<ITransaction>(this.transactionController.History);
                var source = new BindingSource(bindinglist, null);
                this.dataGridViewTransactions.DataSource = source;

                this.currentPositionsModel.Update(this.transactionController.History);
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
        /// Get the latest updated transaction history list and 
        /// display it in a data grid view
        /// </summary>
        private void UpdateTransactionHistoryDataDridView()
        {
            Cursor.Current = Cursors.WaitCursor;

            // Update transactions history data grid view
            try
            {
                this.dataGridViewTransactions.Rows.Clear();
                this.transactionController.Update();
                var bindinglist = new BindingList<ITransaction>(this.transactionController.History);
                var source = new BindingSource(bindinglist, null);
                this.dataGridViewTransactions.DataSource = source;
                this.dataGridViewTransactions.Refresh();
            }
            catch
            {
                MessageBox.Show("Failed to update transaction history list.");
                throw;
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
            
            this.currentPositionsModel.Update(this.transactionController.History);
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
                if (!this.transactionController.AddPurchase(dlg.Date, dlg.Stock, dlg.Quantity, dlg.Cost))
                {
                    MessageBox.Show("Could not add stock \"" + dlg.Stock + "\" to  transactions list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.UpdateTransactionHistoryDataDridView();
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
            List<ITransaction> openList = this.transactionController.OpenList;
            List<string> comboBoxString = new List<string>();
            int i = 0;

            foreach (var o in openList.AsEnumerable())
            {
                comboBoxString.Add(o.Quanity + " shares of " + o.EquitySymbol + " (" + o.PurchasedDate.Value.ToShortDateString() + ")");
                ++i;
            }

            DlgSell dlg = new DlgSell(comboBoxString);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ITransaction transaction = openList[dlg.SelectedSellTransaction];

                if (dlg.Quantity <= transaction.Quanity)
                {
                    if (this.transactionController.SellPosition(transaction.RowID, dlg.SaleDate, dlg.Quantity, dlg.SaleProceeds))
                    {
                        this.UpdateTransactionHistoryDataDridView();
                    }
                    else
                    {
                        MessageBox.Show("Could not sell stock \"" + openList[dlg.SelectedSellTransaction].EquitySymbol + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Could not sell stock \"" + openList[dlg.SelectedSellTransaction].EquitySymbol + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            List<ITransaction> openList = this.transactionController.OpenList;

            List<string> comboBoxString = openList.Select(a => a.EquitySymbol).Distinct().ToList();

            DlgSplit dlg = new DlgSplit(comboBoxString);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (this.transactionController.SplitPosition(dlg.SplitEquity, dlg.SplitRatio))
                {
                    this.UpdateTransactionHistoryDataDridView();
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
    }
}