namespace PetersInvestmentProgram
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

    public partial class DlgSell : Form
    {
        private int rowIndex = -1;

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
        public DlgSell(IEnumerable<string> currentHoldings) : this()
        {
            foreach (var c in currentHoldings)
            {
                this.comboBoxSell.Items.Add(c.ToString());
            }
        }

        /// <summary>
        /// The selected index in the combo box.
        /// </summary>
        public int SelectedSellTransaction { get; private set; }

        /// <summary>
        /// The users entered sales date
        /// </summary>
        public DateTime SaleDate { get; private set; }

        public double Quantity { get; private set; }

        /// <summary>
        /// The users entered sales price
        /// </summary>
        public decimal SaleProceeds { get; private set; }

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
                        this.SelectedSellTransaction = this.rowIndex;
                        this.SaleDate = this.datePicker.Value;
                        this.Quantity = Convert.ToDouble(this.textBoxQuantity.Text);
                        this.SaleProceeds = Convert.ToDecimal(this.textBoxSalesProceeds.Text);
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
        /// Event handler when selection in combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            this.rowIndex = (int)comboBox.SelectedIndex;
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
