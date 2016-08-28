namespace PetersInvestmentProgram
{
    partial class DlgSplit
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
            this.comboBoxSplit = new System.Windows.Forms.ComboBox();
            this.textBoxSplitTo = new System.Windows.Forms.TextBox();
            this.textBoxSplitFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxSplit
            // 
            this.comboBoxSplit.FormattingEnabled = true;
            this.comboBoxSplit.Location = new System.Drawing.Point(12, 12);
            this.comboBoxSplit.Name = "comboBoxSplit";
            this.comboBoxSplit.Size = new System.Drawing.Size(105, 21);
            this.comboBoxSplit.TabIndex = 1;
            this.comboBoxSplit.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSplit_SelectedIndexChanged);
            // 
            // textBoxSplitTo
            // 
            this.textBoxSplitTo.Location = new System.Drawing.Point(205, 13);
            this.textBoxSplitTo.Name = "textBoxSplitTo";
            this.textBoxSplitTo.Size = new System.Drawing.Size(36, 20);
            this.textBoxSplitTo.TabIndex = 2;
            // 
            // textBoxSplitFrom
            // 
            this.textBoxSplitFrom.Location = new System.Drawing.Point(261, 12);
            this.textBoxSplitFrom.Name = "textBoxSplitFrom";
            this.textBoxSplitFrom.Size = new System.Drawing.Size(36, 20);
            this.textBoxSplitFrom.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Split Ratio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "/";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(157, 49);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAccept.Location = new System.Drawing.Point(65, 49);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 6;
            this.buttonAccept.Text = "OK";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // DlgSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 84);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSplitFrom);
            this.Controls.Add(this.textBoxSplitTo);
            this.Controls.Add(this.comboBoxSplit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgSplit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Split";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSplit;
        private System.Windows.Forms.TextBox textBoxSplitTo;
        private System.Windows.Forms.TextBox textBoxSplitFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAccept;
    }
}