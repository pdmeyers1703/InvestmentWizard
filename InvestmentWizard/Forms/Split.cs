namespace InvestmentWizard
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

    public partial class DlgSplit : Form
    {
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
        /// <param name="currentHoldings"></param>
        public DlgSplit(List<string> currentHoldings)
            : this()
        {
            foreach (var c in currentHoldings)
            {
                this.comboBoxSplit.Items.Add(c.ToString());
            }
        }

        /// <summary>
        /// The ratio of the stock split (i.e 2/1 = 2)
        /// </summary>
        public double SplitRatio { get; private set; }

        /// <summary>
        /// The stock that is split
        /// </summary>
        public string SplitEquity { get; private set; }

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
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you would like to split " + this.SplitEquity + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        this.SplitRatio = Convert.ToDouble(this.textBoxSplitTo.Text) / Convert.ToDouble(this.textBoxSplitFrom.Text);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Error entering split");
                }
            }
        }

        /// <summary>
        /// Event hanlder for combo box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            this.SplitEquity = comboBox.SelectedItem.ToString();
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
