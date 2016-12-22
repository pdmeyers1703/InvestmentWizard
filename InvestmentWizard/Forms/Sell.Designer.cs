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
			this.buttonAccept = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.saleDateLabel = new System.Windows.Forms.Label();
			this.textBoxSalesProceeds = new System.Windows.Forms.TextBox();
			this.saleProceedLabel = new System.Windows.Forms.Label();
			this.openPositionsTreeView = new System.Windows.Forms.TreeView();
			this.openPositionsLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonAccept
			// 
			this.buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonAccept.Location = new System.Drawing.Point(36, 285);
			this.buttonAccept.Name = "buttonAccept";
			this.buttonAccept.Size = new System.Drawing.Size(75, 28);
			this.buttonAccept.TabIndex = 4;
			this.buttonAccept.Text = "OK";
			this.buttonAccept.UseVisualStyleBackColor = true;
			this.buttonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(128, 285);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 28);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonCancel_MouseClick);
			// 
			// datePicker
			// 
			this.datePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.datePicker.Location = new System.Drawing.Point(39, 54);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(129, 26);
			this.datePicker.TabIndex = 1;
			// 
			// saleDateLabel
			// 
			this.saleDateLabel.AutoSize = true;
			this.saleDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saleDateLabel.Location = new System.Drawing.Point(36, 24);
			this.saleDateLabel.Name = "saleDateLabel";
			this.saleDateLabel.Size = new System.Drawing.Size(80, 20);
			this.saleDateLabel.TabIndex = 4;
			this.saleDateLabel.Text = "Sale Date";
			// 
			// textBoxSalesProceeds
			// 
			this.textBoxSalesProceeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxSalesProceeds.Location = new System.Drawing.Point(39, 119);
			this.textBoxSalesProceeds.Name = "textBoxSalesProceeds";
			this.textBoxSalesProceeds.Size = new System.Drawing.Size(129, 26);
			this.textBoxSalesProceeds.TabIndex = 2;
			// 
			// saleProceedLabel
			// 
			this.saleProceedLabel.AutoSize = true;
			this.saleProceedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saleProceedLabel.Location = new System.Drawing.Point(37, 87);
			this.saleProceedLabel.Name = "saleProceedLabel";
			this.saleProceedLabel.Size = new System.Drawing.Size(135, 20);
			this.saleProceedLabel.TabIndex = 6;
			this.saleProceedLabel.Text = "Sale Proceeds ($)";
			// 
			// openPositionsTreeView
			// 
			this.openPositionsTreeView.CheckBoxes = true;
			this.openPositionsTreeView.LineColor = System.Drawing.Color.DodgerBlue;
			this.openPositionsTreeView.Location = new System.Drawing.Point(242, 48);
			this.openPositionsTreeView.Name = "openPositionsTreeView";
			this.openPositionsTreeView.Size = new System.Drawing.Size(267, 265);
			this.openPositionsTreeView.TabIndex = 9;
			this.openPositionsTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.OpenPositionsTreeView_AfterChecked);
			this.openPositionsTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OpenPositionsTreeView_AfterExpand);
			// 
			// openPositionsLabel
			// 
			this.openPositionsLabel.AutoSize = true;
			this.openPositionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.openPositionsLabel.Location = new System.Drawing.Point(239, 19);
			this.openPositionsLabel.Name = "openPositionsLabel";
			this.openPositionsLabel.Size = new System.Drawing.Size(116, 20);
			this.openPositionsLabel.TabIndex = 10;
			this.openPositionsLabel.Text = "Open Positions";
			// 
			// DlgSell
			// 
			this.AcceptButton = this.buttonAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 325);
			this.Controls.Add(this.openPositionsLabel);
			this.Controls.Add(this.openPositionsTreeView);
			this.Controls.Add(this.saleProceedLabel);
			this.Controls.Add(this.textBoxSalesProceeds);
			this.Controls.Add(this.saleDateLabel);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonAccept);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label saleDateLabel;
        private System.Windows.Forms.TextBox textBoxSalesProceeds;
        private System.Windows.Forms.Label saleProceedLabel;
		private System.Windows.Forms.TreeView openPositionsTreeView;
		private System.Windows.Forms.Label openPositionsLabel;
	}
}