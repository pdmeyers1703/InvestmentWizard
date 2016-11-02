// <copyright file="Sell.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright> System;

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Forms;

	public partial class DlgSell : Form
	{				   
		private ITransactionController transactionController;
		private IEnumerable<ITransaction> openTransactions;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public DlgSell()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Constructor that initializes a combo box.
		/// </summary>
		/// <param name="currentHoldings">items to fill combo box</param>
		public DlgSell(IEnumerable<ITransaction> openTransactions, ITransactionController transactionController) : this()
		{
			this.transactionController = transactionController;
			this.openTransactions = openTransactions;

			foreach (var t in this.openTransactions)
			{
				this.comboBoxSell.Items.Add(t.Quanity + " shares of " + t.EquitySymbol + " (" + t.PurchasedDate + ")");
			}
		}

		/// <summary>
		/// Event Handler for OK button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonAccept_Click(object sender, EventArgs e)
		{
			if (this.comboBoxSell.SelectedItem == null)
			{
				MessageBox.Show("Select an open open position from the combo box");
			}
			else if ((this.textBoxQuantity.Text == string.Empty) &&
						(Convert.ToDouble(this.textBoxQuantity) > 0.0f))
			{
			}
			else if ((this.textBoxSalesProceeds.Text == string.Empty) &&
						(Convert.ToDecimal(this.textBoxSalesProceeds) > 0.00m))
			{
				MessageBox.Show("Please enter a sales price greater than $0.00");
			}
			else
			{
				try
				{
					if (DialogResult.Yes == MessageBox.Show("Are you sure you would like to sell this position?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						ITransaction selectedItem = this.openTransactions.ElementAt(this.comboBoxSell.SelectedIndex);
						this.transactionController.SellPosition(
							Convert.ToInt32(selectedItem.RowID), 
							this.datePicker.Value, 
							Convert.ToDouble(this.textBoxQuantity.Text), 
							Convert.ToDecimal(this.textBoxSalesProceeds.Text));
						this.Close();
					}
				}
				catch
				{
					MessageBox.Show("Error entering sell data");
				}
			}
		}

		/// <summary>
		/// Closes dialog box without taking combo box selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonCancel_MouseClick(object sender, MouseEventArgs e)
		{
			this.Close();
		}
	}
}
