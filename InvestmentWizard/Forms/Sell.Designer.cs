namespace InvestmentWizard
{
    partial class DlgSell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.comboBoxSell = new System.Windows.Forms.ComboBox();
			this.buttonAccept = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.saleDateLabel = new System.Windows.Forms.Label();
			this.textBoxSalesProceeds = new System.Windows.Forms.TextBox();
			this.saleProceedLabel = new System.Windows.Forms.Label();
			this.textBoxQuantity = new System.Windows.Forms.TextBox();
			this.quantityLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBoxSell
			// 
			this.comboBoxSell.FormattingEnabled = true;
			this.comboBoxSell.Location = new System.Drawing.Point(12, 12);
			this.comboBoxSell.Name = "comboBoxSell";
			this.comboBoxSell.Size = new System.Drawing.Size(239, 21);
			this.comboBoxSell.TabIndex = 0;
			// 
			// buttonAccept
			// 
			this.buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonAccept.Location = new System.Drawing.Point(54, 129);
			this.buttonAccept.Name = "buttonAccept";
			this.buttonAccept.Size = new System.Drawing.Size(75, 23);
			this.buttonAccept.TabIndex = 4;
			this.buttonAccept.Text = "OK";
			this.buttonAccept.UseVisualStyleBackColor = true;
			this.buttonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(146, 129);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonCancel_MouseClick);
			// 
			// datePicker
			// 
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.datePicker.Location = new System.Drawing.Point(92, 38);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(129, 20);
			this.datePicker.TabIndex = 1;
			// 
			// saleDateLabel
			// 
			this.saleDateLabel.AutoSize = true;
			this.saleDateLabel.Location = new System.Drawing.Point(16, 44);
			this.saleDateLabel.Name = "saleDateLabel";
			this.saleDateLabel.Size = new System.Drawing.Size(54, 13);
			this.saleDateLabel.TabIndex = 4;
			this.saleDateLabel.Text = "Sale Date";
			// 
			// textBoxSalesProceeds
			// 
			this.textBoxSalesProceeds.Location = new System.Drawing.Point(92, 94);
			this.textBoxSalesProceeds.Name = "textBoxSalesProceeds";
			this.textBoxSalesProceeds.Size = new System.Drawing.Size(129, 20);
			this.textBoxSalesProceeds.TabIndex = 3;
			// 
			// saleProceedLabel
			// 
			this.saleProceedLabel.AutoSize = true;
			this.saleProceedLabel.Location = new System.Drawing.Point(16, 97);
			this.saleProceedLabel.Name = "saleProceedLabel";
			this.saleProceedLabel.Size = new System.Drawing.Size(76, 13);
			this.saleProceedLabel.TabIndex = 6;
			this.saleProceedLabel.Text = "Sale Proceeds";
			// 
			// textBoxQuantity
			// 
			this.textBoxQuantity.Location = new System.Drawing.Point(92, 64);
			this.textBoxQuantity.Name = "textBoxQuantity";
			this.textBoxQuantity.Size = new System.Drawing.Size(129, 20);
			this.textBoxQuantity.TabIndex = 2;
			// 
			// quantityLabel
			// 
			this.quantityLabel.AutoSize = true;
			this.quantityLabel.Location = new System.Drawing.Point(16, 67);
			this.quantityLabel.Name = "quantityLabel";
			this.quantityLabel.Size = new System.Drawing.Size(62, 13);
			this.quantityLabel.TabIndex = 8;
			this.quantityLabel.Text = "# of Shares";
			// 
			// DlgSell
			// 
			this.AcceptButton = this.buttonAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 164);
			this.Controls.Add(this.quantityLabel);
			this.Controls.Add(this.textBoxQuantity);
			this.Controls.Add(this.saleProceedLabel);
			this.Controls.Add(this.textBoxSalesProceeds);
			this.Controls.Add(this.saleDateLabel);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonAccept);
			this.Controls.Add(this.comboBoxSell);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgSell";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Sell";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSell;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label saleDateLabel;
        private System.Windows.Forms.TextBox textBoxSalesProceeds;
        private System.Windows.Forms.Label saleProceedLabel;
        private System.Windows.Forms.TextBox textBoxQuantity;
        private System.Windows.Forms.Label quantityLabel;
    }
}