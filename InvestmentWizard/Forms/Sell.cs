// <copyright file="Sell.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright> System;

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	public partial class DlgSell : Form
	{
		private ITransactionController transactionController;
		private IList<IList<string>> openTransactions;

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
		public DlgSell(IList<IList<string>> openTransactions, ITransactionController transactionController) : this()
		{
			this.transactionController = transactionController;
			this.openTransactions = openTransactions;

			foreach (var t in this.openTransactions)
			{
				this.comboBoxSell.Items.Add(t[3] + " shares of " + t[2] + " (" + t[1] + ")");
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
						IList<string> selectedItem = this.openTransactions[this.comboBoxSell.SelectedIndex];
						this.transactionController.SellPosition(
							Convert.ToInt32(selectedItem[0]), 
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
