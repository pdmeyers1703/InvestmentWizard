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
		public DlgSell(IList<ITransaction> openTransactions, ITransactionController transactionController) : this()
		{
			this.transactionController = transactionController;
			this.openTransactions = openTransactions;

			IEnumerable<ITransaction> orderOpenTransactions = this.openTransactions
				.OrderBy(o => o.EquitySymbol)
				.ThenBy(o => o.PurchasedDate);

			this.openPositionsTreeView.Nodes.Clear();

			foreach (var equityName in orderOpenTransactions.Select(o => o.EquitySymbol).Distinct().ToList())
			{
				TreeNode newNode = new TreeNode();
				foreach (var transaction in orderOpenTransactions.Where(o => o.EquitySymbol == equityName).ToList())
				{
					TreeNode newChildNode = 
						new TreeNode(transaction.PurchasedDate.Value.ToShortDateString() + " - " +
						transaction.Quanity + " Shares @ $" + 
						((double)transaction.Cost / transaction.Quanity).ToString("0.00"));
					newChildNode.Tag = transaction;
					newNode.Nodes.Add(newChildNode);
				}

				this.SetParentNodeText(newNode); 
				this.openPositionsTreeView.Nodes.Add(newNode);
			}
		}

		/// <summary>
		/// Event Handler for OK button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonAccept_Click(object sender, EventArgs e)
		{
			List<ITransaction> sellTransactions = new List<ITransaction>();

			foreach (TreeNode parentNode in this.openPositionsTreeView.Nodes)
			{
				foreach (TreeNode childNode in parentNode.Nodes)
				{
					if (childNode.Checked)
					{
						sellTransactions.Add((ITransaction)childNode.Tag);
					}
				}
			}

			this.DialogResult = DialogResult.None;

			if ((this.textBoxSalesProceeds.Text == string.Empty) ||
				((this.textBoxSalesProceeds.Text != string.Empty) && 
				Convert.ToDecimal(this.textBoxSalesProceeds.Text) < 0.01m))
			{
				MessageBox.Show("Please enter a sales price greater than $0.00");
			}
			else if (sellTransactions.Count() == 0)
			{
				MessageBox.Show("Please select at least 1 transaction to sell");
			}
			else
			{
				if (DialogResult.Yes == MessageBox.Show(
					"Are you sure you would like to sell this position?",
					"Confirmation",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question))
				{
					this.DialogResult = DialogResult.OK;
					try
					{
						this.transactionController.SellPositions(sellTransactions, this.datePicker.Value, Convert.ToDecimal(this.textBoxSalesProceeds.Text));
					}
					catch
					{
						MessageBox.Show("Failure to sell one or more transactions");
					}
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

		/// <summary>
		/// On expanding a node, collapse all other nodes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenPositionsTreeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			foreach (TreeNode node in this.openPositionsTreeView.Nodes)
			{
				if (node != e.Node)
				{
					node.Collapse();
					node.Checked = false;

					foreach (TreeNode childNode in node.Nodes)
					{
						childNode.Checked = false;
					}
				}
			}
		}

		/// <summary>
		/// If Top level node is checked or unchecked apply that state to its children
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenPositionsTreeView_AfterChecked(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Parent == null)
			{
				foreach (TreeNode childNode in e.Node.Nodes)
				{
					childNode.Checked = e.Node.Checked;
				}
			}
			else
			{
				this.SetParentNodeText(e.Node.Parent);
			}
		}	 

		private void SetParentNodeText(TreeNode parentNode)
		{
			double quantitySelected = 0.0f;
			double totalQuantity = 0.0f;
			foreach (TreeNode childNode in parentNode.Nodes)
			{
				ITransaction transaction = (ITransaction)childNode.Tag;

				if (childNode.Checked)
				{
					quantitySelected += transaction.Quanity;
				}

				totalQuantity += transaction.Quanity;
			}

			parentNode.Text = ((ITransaction)parentNode.FirstNode.Tag).EquitySymbol + " -  (" + quantitySelected + "/"
				+ totalQuantity + ")";
		}
	}
}