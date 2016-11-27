namespace InvestmentWizard
{
    public partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton updateQuotes;
        private System.Windows.Forms.DataGridView dataGridViewCurPos;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lastQuoteUpdateStatusLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageTransactions;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.BindingSource transactionsBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddTransaction;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.updateQuotes = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonAddTransaction = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSellTransaction = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSplit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
			this.dataGridViewCurPos = new System.Windows.Forms.DataGridView();
			this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.currentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PriceChangePercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.YtdPercentGainLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.currentMarketValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gainLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.percentGainLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lastQuoteUpdateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.sp00TodayTextStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.sp500TodayValueStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.sp00YtdTextStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.sp500YtdValueStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageMain = new System.Windows.Forms.TabPage();
			this.tabPageTransactions = new System.Windows.Forms.TabPage();
			this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
			this.PurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EquitySymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Quantity2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PurchasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TotalCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SaleProceeds = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Dividends = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.transactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurPos)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageMain.SuspendLayout();
			this.tabPageTransactions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateQuotes,
            this.toolStripSeparator1,
            this.toolStripButtonAddTransaction,
            this.toolStripButtonSellTransaction,
            this.toolStripButtonSplit,
            this.toolStripSeparator2,
            this.toolStripButtonAbout});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(881, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip";
			// 
			// updateQuotes
			// 
			this.updateQuotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.updateQuotes.Image = ((System.Drawing.Image)(resources.GetObject("updateQuotes.Image")));
			this.updateQuotes.ImageTransparentColor = System.Drawing.Color.White;
			this.updateQuotes.Name = "updateQuotes";
			this.updateQuotes.Size = new System.Drawing.Size(23, 22);
			this.updateQuotes.Text = "Refresh";
			this.updateQuotes.Click += new System.EventHandler(this.OnClick_UpdateQuotes);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonAddTransaction
			// 
			this.toolStripButtonAddTransaction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAddTransaction.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddTransaction.Image")));
			this.toolStripButtonAddTransaction.ImageTransparentColor = System.Drawing.Color.White;
			this.toolStripButtonAddTransaction.Name = "toolStripButtonAddTransaction";
			this.toolStripButtonAddTransaction.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAddTransaction.Text = "Add";
			this.toolStripButtonAddTransaction.Click += new System.EventHandler(this.ToolStripButtonAddTransaction_Click);
			// 
			// toolStripButtonSellTransaction
			// 
			this.toolStripButtonSellTransaction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSellTransaction.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSellTransaction.Image")));
			this.toolStripButtonSellTransaction.ImageTransparentColor = System.Drawing.Color.White;
			this.toolStripButtonSellTransaction.Name = "toolStripButtonSellTransaction";
			this.toolStripButtonSellTransaction.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSellTransaction.Text = "Sell";
			this.toolStripButtonSellTransaction.Click += new System.EventHandler(this.ToolStripButtonSellTransaction_Click);
			// 
			// toolStripButtonSplit
			// 
			this.toolStripButtonSplit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSplit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSplit.Image")));
			this.toolStripButtonSplit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSplit.Name = "toolStripButtonSplit";
			this.toolStripButtonSplit.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSplit.Text = "Split";
			this.toolStripButtonSplit.Click += new System.EventHandler(this.ToolStripButtonSplit_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonAbout
			// 
			this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
			this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAbout.Name = "toolStripButtonAbout";
			this.toolStripButtonAbout.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAbout.Text = "About";
			this.toolStripButtonAbout.Click += new System.EventHandler(this.ToolStripButtonAbout_Click);
			// 
			// dataGridViewCurPos
			// 
			this.dataGridViewCurPos.AllowUserToAddRows = false;
			this.dataGridViewCurPos.AllowUserToDeleteRows = false;
			this.dataGridViewCurPos.AllowUserToResizeColumns = false;
			this.dataGridViewCurPos.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCurPos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewCurPos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewCurPos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCurPos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewCurPos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCurPos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stock,
            this.currentPrice,
            this.priceChange,
            this.PriceChangePercent,
            this.YtdPercentGainLoss,
            this.quantity,
            this.cost,
            this.currentMarketValue,
            this.gainLoss,
            this.percentGainLoss});
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.LightBlue;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCurPos.DefaultCellStyle = dataGridViewCellStyle13;
			this.dataGridViewCurPos.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewCurPos.MultiSelect = false;
			this.dataGridViewCurPos.Name = "dataGridViewCurPos";
			this.dataGridViewCurPos.ReadOnly = true;
			this.dataGridViewCurPos.RowHeadersWidth = 10;
			this.dataGridViewCurPos.Size = new System.Drawing.Size(873, 392);
			this.dataGridViewCurPos.TabIndex = 1;
			this.dataGridViewCurPos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewCurPos_CellFormatting);
			// 
			// stock
			// 
			this.stock.DataPropertyName = "StockTicker";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.stock.DefaultCellStyle = dataGridViewCellStyle3;
			this.stock.FillWeight = 75.21733F;
			this.stock.HeaderText = "Stock";
			this.stock.Name = "stock";
			this.stock.ReadOnly = true;
			this.stock.Width = 60;
			// 
			// currentPrice
			// 
			this.currentPrice.DataPropertyName = "CurrentPrice";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Format = "C2";
			dataGridViewCellStyle4.NullValue = null;
			this.currentPrice.DefaultCellStyle = dataGridViewCellStyle4;
			this.currentPrice.HeaderText = "Price";
			this.currentPrice.Name = "currentPrice";
			this.currentPrice.ReadOnly = true;
			this.currentPrice.Width = 75;
			// 
			// priceChange
			// 
			this.priceChange.DataPropertyName = "PriceChange";
			dataGridViewCellStyle5.Format = "C2";
			dataGridViewCellStyle5.NullValue = "0";
			this.priceChange.DefaultCellStyle = dataGridViewCellStyle5;
			this.priceChange.HeaderText = "Price +/-";
			this.priceChange.Name = "priceChange";
			this.priceChange.ReadOnly = true;
			this.priceChange.Width = 75;
			// 
			// PriceChangePercent
			// 
			this.PriceChangePercent.DataPropertyName = "PriceChangePercent";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.Format = "0.0%";
			this.PriceChangePercent.DefaultCellStyle = dataGridViewCellStyle6;
			this.PriceChangePercent.HeaderText = "Price +/- %";
			this.PriceChangePercent.Name = "PriceChangePercent";
			this.PriceChangePercent.ReadOnly = true;
			this.PriceChangePercent.Width = 75;
			// 
			// YtdPercentGainLoss
			// 
			this.YtdPercentGainLoss.DataPropertyName = "YtdPercentGainLoss";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.Format = "0.0 %";
			dataGridViewCellStyle7.NullValue = "NA";
			this.YtdPercentGainLoss.DefaultCellStyle = dataGridViewCellStyle7;
			this.YtdPercentGainLoss.HeaderText = "YTD Price +/- %";
			this.YtdPercentGainLoss.Name = "YtdPercentGainLoss";
			this.YtdPercentGainLoss.ReadOnly = true;
			this.YtdPercentGainLoss.Width = 75;
			// 
			// quantity
			// 
			this.quantity.DataPropertyName = "Quantity";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.quantity.DefaultCellStyle = dataGridViewCellStyle8;
			this.quantity.FillWeight = 114.2132F;
			this.quantity.HeaderText = "Quantity";
			this.quantity.Name = "quantity";
			this.quantity.ReadOnly = true;
			this.quantity.Width = 75;
			// 
			// cost
			// 
			this.cost.DataPropertyName = "Cost";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.Format = "C2";
			dataGridViewCellStyle9.NullValue = null;
			this.cost.DefaultCellStyle = dataGridViewCellStyle9;
			this.cost.FillWeight = 110.5695F;
			this.cost.HeaderText = "Cost";
			this.cost.Name = "cost";
			this.cost.ReadOnly = true;
			// 
			// currentMarketValue
			// 
			this.currentMarketValue.DataPropertyName = "CurrentMarketValue";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.Format = "C2";
			dataGridViewCellStyle10.NullValue = null;
			this.currentMarketValue.DefaultCellStyle = dataGridViewCellStyle10;
			this.currentMarketValue.HeaderText = "Market Value";
			this.currentMarketValue.Name = "currentMarketValue";
			this.currentMarketValue.ReadOnly = true;
			// 
			// gainLoss
			// 
			this.gainLoss.DataPropertyName = "GainLoss";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.Format = "C2";
			dataGridViewCellStyle11.NullValue = null;
			this.gainLoss.DefaultCellStyle = dataGridViewCellStyle11;
			this.gainLoss.HeaderText = "+/-";
			this.gainLoss.Name = "gainLoss";
			this.gainLoss.ReadOnly = true;
			// 
			// percentGainLoss
			// 
			this.percentGainLoss.DataPropertyName = "PercentGainLoss";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.Format = "0.0 %";
			dataGridViewCellStyle12.NullValue = null;
			this.percentGainLoss.DefaultCellStyle = dataGridViewCellStyle12;
			this.percentGainLoss.HeaderText = "+/- %";
			this.percentGainLoss.Name = "percentGainLoss";
			this.percentGainLoss.ReadOnly = true;
			this.percentGainLoss.Width = 75;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastQuoteUpdateStatusLabel,
            this.toolStripStatusLabel1,
            this.sp00TodayTextStatusLabel,
            this.sp500TodayValueStatusLabel,
            this.sp00YtdTextStatusLabel,
            this.sp500YtdValueStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 453);
			this.statusStrip.Name = "statusStrip1";
			this.statusStrip.Size = new System.Drawing.Size(881, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "Ready to make some Money!!!";
			// 
			// lastQuoteUpdateStatusLabel
			// 
			this.lastQuoteUpdateStatusLabel.Name = "lastQuoteUpdateStatusLabel";
			this.lastQuoteUpdateStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
			this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripStatusLabel1.RightToLeftAutoMirrorImage = true;
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(835, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sp00TodayTextStatusLabel
			// 
			this.sp00TodayTextStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.sp00TodayTextStatusLabel.Name = "sp00TodayTextStatusLabel";
			this.sp00TodayTextStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// sp500TodayValueStatusLabel
			// 
			this.sp500TodayValueStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.sp500TodayValueStatusLabel.Name = "sp500TodayValueStatusLabel";
			this.sp500TodayValueStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.sp500TodayValueStatusLabel.TextChanged += new System.EventHandler(this.SP00TodayValueStatusLabel_TextChanged);
			// 
			// sp00YtdTetStatusLabel
			// 
			this.sp00YtdTextStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sp00YtdTextStatusLabel.Name = "sp00YtdTetStatusLabel";
			this.sp00YtdTextStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// sp500YtdValueStatusLabel
			// 
			this.sp500YtdValueStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sp500YtdValueStatusLabel.Name = "sp500YtdValueStatusLabel";
			this.sp500YtdValueStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.sp500YtdValueStatusLabel.TextChanged += new System.EventHandler(this.SP500YtdValueStatusLabel_TextChanged);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControl.Controls.Add(this.tabPageMain);
			this.tabControl.Controls.Add(this.tabPageTransactions);
			this.tabControl.Location = new System.Drawing.Point(0, 29);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(881, 421);
			this.tabControl.TabIndex = 3;
			// 
			// tabPageMain
			// 
			this.tabPageMain.Controls.Add(this.dataGridViewCurPos);
			this.tabPageMain.Location = new System.Drawing.Point(4, 25);
			this.tabPageMain.Name = "tabPageMain";
			this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMain.Size = new System.Drawing.Size(873, 392);
			this.tabPageMain.TabIndex = 0;
			this.tabPageMain.Text = "Current Positions";
			this.tabPageMain.UseVisualStyleBackColor = true;
			this.tabPageMain.Enter += new System.EventHandler(this.TabPageCurrentOpenPositions_Enter);
			// 
			// tabPageTransactions
			// 
			this.tabPageTransactions.Controls.Add(this.dataGridViewTransactions);
			this.tabPageTransactions.Location = new System.Drawing.Point(4, 25);
			this.tabPageTransactions.Name = "tabPageTransactions";
			this.tabPageTransactions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTransactions.Size = new System.Drawing.Size(873, 392);
			this.tabPageTransactions.TabIndex = 1;
			this.tabPageTransactions.Text = "TransactionHistory";
			this.tabPageTransactions.UseVisualStyleBackColor = true;
			this.tabPageTransactions.Enter += new System.EventHandler(this.TabPageTransactions_Enter);
			// 
			// dataGridViewTransactions
			// 
			this.dataGridViewTransactions.AllowUserToAddRows = false;
			this.dataGridViewTransactions.AllowUserToDeleteRows = false;
			this.dataGridViewTransactions.AllowUserToResizeColumns = false;
			this.dataGridViewTransactions.AllowUserToResizeRows = false;
			this.dataGridViewTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
			this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PurchaseDate,
            this.EquitySymbol,
            this.Quantity2,
            this.PurchasePrice,
            this.TotalCost,
            this.SaleDate,
            this.SalePrice,
            this.SaleProceeds,
            this.Dividends});
			this.dataGridViewTransactions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewTransactions.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewTransactions.Name = "dataGridViewTransactions";
			this.dataGridViewTransactions.Size = new System.Drawing.Size(823, 365);
			this.dataGridViewTransactions.TabIndex = 0;
			// 
			// PurchaseDate
			// 
			this.PurchaseDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.PurchaseDate.DataPropertyName = "PurchaseDate";
			dataGridViewCellStyle15.Format = "d";
			dataGridViewCellStyle15.NullValue = null;
			this.PurchaseDate.DefaultCellStyle = dataGridViewCellStyle15;
			this.PurchaseDate.HeaderText = "Purchased Date";
			this.PurchaseDate.MinimumWidth = 75;
			this.PurchaseDate.Name = "PurchaseDate";
			this.PurchaseDate.ReadOnly = true;
			this.PurchaseDate.Width = 75;
			// 
			// EquitySymbol
			// 
			this.EquitySymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.EquitySymbol.DataPropertyName = "EquitySymbol";
			this.EquitySymbol.HeaderText = "Ticker";
			this.EquitySymbol.Name = "EquitySymbol";
			this.EquitySymbol.ReadOnly = true;
			this.EquitySymbol.Width = 68;
			// 
			// Quantity2
			// 
			this.Quantity2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.Quantity2.DataPropertyName = "Quanity";
			this.Quantity2.HeaderText = "Quantity";
			this.Quantity2.Name = "Quantity2";
			this.Quantity2.ReadOnly = true;
			this.Quantity2.Width = 79;
			// 
			// PurchasePrice
			// 
			this.PurchasePrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.PurchasePrice.DataPropertyName = "PurchasePrice";
			dataGridViewCellStyle16.Format = "C2";
			dataGridViewCellStyle16.NullValue = null;
			this.PurchasePrice.DefaultCellStyle = dataGridViewCellStyle16;
			this.PurchasePrice.HeaderText = "Purchase Price";
			this.PurchasePrice.MinimumWidth = 75;
			this.PurchasePrice.Name = "PurchasePrice";
			this.PurchasePrice.ReadOnly = true;
			this.PurchasePrice.Width = 75;
			// 
			// TotalCost
			// 
			this.TotalCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.TotalCost.DataPropertyName = "Cost";
			dataGridViewCellStyle17.Format = "C2";
			dataGridViewCellStyle17.NullValue = null;
			this.TotalCost.DefaultCellStyle = dataGridViewCellStyle17;
			this.TotalCost.HeaderText = "Cost";
			this.TotalCost.Name = "TotalCost";
			this.TotalCost.ReadOnly = true;
			this.TotalCost.Width = 57;
			// 
			// SaleDate
			// 
			this.SaleDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.SaleDate.DataPropertyName = "SaleDate";
			dataGridViewCellStyle18.Format = "d";
			dataGridViewCellStyle18.NullValue = null;
			this.SaleDate.DefaultCellStyle = dataGridViewCellStyle18;
			this.SaleDate.HeaderText = "Sale Date";
			this.SaleDate.MinimumWidth = 100;
			this.SaleDate.Name = "SaleDate";
			this.SaleDate.ReadOnly = true;
			// 
			// SalePrice
			// 
			this.SalePrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.SalePrice.DataPropertyName = "SalePrice";
			dataGridViewCellStyle19.Format = "C2";
			dataGridViewCellStyle19.NullValue = null;
			this.SalePrice.DefaultCellStyle = dataGridViewCellStyle19;
			this.SalePrice.HeaderText = "Sale Price";
			this.SalePrice.MinimumWidth = 100;
			this.SalePrice.Name = "SalePrice";
			this.SalePrice.ReadOnly = true;
			// 
			// SaleProceeds
			// 
			this.SaleProceeds.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.SaleProceeds.DataPropertyName = "SaleProceeds";
			dataGridViewCellStyle20.Format = "C2";
			dataGridViewCellStyle20.NullValue = null;
			this.SaleProceeds.DefaultCellStyle = dataGridViewCellStyle20;
			this.SaleProceeds.HeaderText = "Proceeds";
			this.SaleProceeds.MinimumWidth = 100;
			this.SaleProceeds.Name = "SaleProceeds";
			this.SaleProceeds.ReadOnly = true;
			// 
			// Dividends
			// 
			this.Dividends.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Dividends.DataPropertyName = "Dividends";
			dataGridViewCellStyle21.Format = "C2";
			dataGridViewCellStyle21.NullValue = null;
			this.Dividends.DefaultCellStyle = dataGridViewCellStyle21;
			this.Dividends.HeaderText = "Dividends";
			this.Dividends.MinimumWidth = 100;
			this.Dividends.Name = "Dividends";
			this.Dividends.ReadOnly = true;
			this.Dividends.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Dividends.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// transactionsBindingSource
			// 
			this.transactionsBindingSource.DataMember = "Transactions";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(881, 475);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Main";
			this.Text = "Investment Wizard";
			this.Load += new System.EventHandler(this.Main_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurPos)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPageMain.ResumeLayout(false);
			this.tabPageTransactions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion        

        private System.Windows.Forms.ToolStripButton toolStripButtonSellTransaction;
        private System.Windows.Forms.ToolStripButton toolStripButtonSplit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel sp00TodayTextStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel sp500TodayValueStatusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceChangePercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn YtdPercentGainLoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentMarketValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn gainLoss;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentGainLoss;
        private System.Windows.Forms.ToolStripStatusLabel sp00YtdTextStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel sp500YtdValueStatusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquitySymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchasePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleProceeds;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dividends;
    }
}
