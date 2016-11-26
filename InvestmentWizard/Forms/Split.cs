// <copyright file="Split.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	public partial class DlgSplit : Form
	{
		private ITransactionController transactionController;

		/// <summary>
		/// Initializes the default constructor
		/// </summary>
		public DlgSplit()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Initializes a constructor that sets the combo box
		/// </summary>
		/// <param name="currentHoldings">List of stock names that are currently held.</param>
		public DlgSplit(List<string> currentHoldings, ITransactionController transactionController)
			: this()
		{
			this.transactionController = transactionController;

			foreach (var c in currentHoldings)
			{
				this.comboBoxSplit.Items.Add(c.ToString());
			}
		}

		/// <summary>
		/// Event handler for OK button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonAccept_Click(object sender, EventArgs e)
		{
			if (this.comboBoxSplit.SelectedItem == null)
			{
				MessageBox.Show("Select a stock from the combo box");
			}
			else if (this.textBoxSplitFrom.Text == string.Empty ||
						this.textBoxSplitTo.Text == string.Empty)
			{
				MessageBox.Show("Please enter a stock split ration");
			}
			else
			{
				try
				{
					if (DialogResult.Yes == MessageBox.Show("Are you sure you would like to split " + this.comboBoxSplit.SelectedItem.ToString() + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						this.transactionController.SplitPosition(
							this.comboBoxSplit.SelectedItem.ToString(),
							Convert.ToDouble(this.textBoxSplitTo.Text) / Convert.ToDouble(this.textBoxSplitFrom.Text));
						this.Close();
					}
				}
				catch
				{
					MessageBox.Show("Could not split stock \"" + this.comboBoxSplit.SelectedItem.ToString() + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// Event handler for Cancel button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
