namespace InvestmentWizard
{
	using System;
	using System.Linq;
	using System.Windows.Forms;

	/// <summary>
	/// Dialog box to enter an stock buy
	/// </summary>
	public partial class DlgBuyTransaction : Form
	{
		private ITransactionController transactionController;

		public DlgBuyTransaction(ITransactionController controller)
		{
			this.InitializeComponent();
			this.transactionController = controller;
		}

		private void BtnBuyTransactionAccept_Click(object sender, EventArgs e)
		{
			if ((this.textBoxTickerSymbol.Text == string.Empty) ||
				this.textBoxTickerSymbol.Text.Any(x => !char.IsLetter(x)) ||
				(this.textBoxTickerSymbol.Text.Length > 4))
			{
				this.ReportDataValidationError("Please enter stock ticker symbol that 1 to 4 or letters");
			}
			else if ((this.textBoxQuantity.Text == string.Empty) ||
						(Convert.ToDouble(this.textBoxQuantity.Text) <= 0))
			{
				this.ReportDataValidationError("Please enter a number of shares greater than 0");
			}
			else if ((this.textBoxCost.Text == string.Empty) ||
				(Convert.ToDecimal(this.textBoxCost.Text) <= 0))
			{
				this.ReportDataValidationError("Please enter a total cost greater than $0.00");
			}
			else
			{
				if (DialogResult.Yes == MessageBox.Show("Are you sure you would like to Add this purchase?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					this.transactionController.AddPosition(
						this.dateTimePicker.Value,
						this.textBoxTickerSymbol.Text,
						Convert.ToDouble(this.textBoxQuantity.Text),
						Convert.ToDecimal(this.textBoxCost.Text));
				}
				else
				{
					// Keep dialog box open
					this.DialogResult = DialogResult.None;
				}
			}
		}

		private void ReportDataValidationError(string errorString)
		{
			MessageBox.Show(errorString, "Data Entry Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			this.DialogResult = DialogResult.None;
		}
	}
}
