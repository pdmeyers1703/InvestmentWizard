namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;

    public class TransactionsListReadModel : IListObservable, IListReadModel
    {
        private IDatabase database;
        private IList<ITransaction> transactions;
        private bool refresh = true;

        public TransactionsListReadModel(IDatabase database)
        {
            this.database = database;
            Debug.Assert(this.database != null, "Database is null reference");
            this.transactions = new List<ITransaction>();
        }

        protected event ListChangedEventHandler ListChangedObserver;

        public IList<ITransaction> OpenTransactionsList
        {
            get
            {
                return this.transactions.Where(t => t.SaleDate == null).ToList();
            }
        }

        protected IList<ITransaction> Transactions
        {
            get
            {
                return this.transactions;
            }
        }

        /// <summary>
        /// Register observers for this model
        /// </summary>
        /// <param name="listChangedObserver">event handler</param>
        public void RegisterObserver(ListChangedEventHandler listChangedObserver)
        {
            this.ListChangedObserver += listChangedObserver;
        }

        /// <summary>
        /// Get entire transaction table in the database if refresh is needed
        /// </summary>
        public virtual void Update()
        {
            if (this.refresh)
            {
                this.DoUpdate();
                this.refresh = false;
            }
        }

        public bool Add(DateTime date, string stock, double quantity, decimal cost)
        {
            try
            {
                string[] columns = 
                                  {
									 TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.PurchaseDate),
									 TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.EquityName),
									 TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity),
									 TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.CostBasis) 
                                   };
                dynamic[] values = { date.ToShortDateString(), stock, quantity, cost };
                this.database.AddRecord(TransactionTableSchema.TransactionTableName, columns, values);
            }
            catch
            {
                return false;
            }

            return true;
        }

		public bool Sell(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds)
		{
			try
			{
				DataTable dt = this.database.GetRows(
					TransactionTableSchema.TransactionTableName,
					TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.ID),
					rowIndex);

				if (dt.Rows.Count != 1)
				{
					throw new Exception("Error: Transaction to sell was not found!");
				}
				else
				{
					DataRow dr = dt.Rows[0];
					////List<decimal> dividends = this.GetDividend(dr[3].ToString(), this.ConvertDate(dr[1].ToString()), this.ConvertDate(dr[5].ToString()));
					string[] columns = 
					{
						TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.SoldDate),
						TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.MarketValue),
					};

					dynamic[] values = { saleDate.ToShortDateString(), saleProceeds };
					this.database.UpdateRecord(TransactionTableSchema.TransactionTableName, rowIndex, columns, values);
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

        public bool Split(string equitySymbol, double splitRatio)
        {
            try
            {
                DataTable dt = this.database.GetRows(TransactionTableSchema.TransactionTableName, TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.EquityName), equitySymbol);

                string[] columns = { TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity) };
                foreach (var row in dt.AsEnumerable())
                {
                    string[] values = { (Convert.ToDouble(row[TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.Quantity)]) * splitRatio).ToString() };
                    this.database.UpdateRecord(TransactionTableSchema.TransactionTableName, Convert.ToInt32(row[TransactionTableSchema.ColumnLookUp(TransactionTableSchema.ColumnIndex.ID)]), columns, values);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Fires when list changes
        /// </summary>
        /// <param name="list">2 dimensional list of strings</param>
        protected void OnListChanged(IList<IList<string>> list)
        {
            this.ListChangedObserver(list);
        }

        /// <summary>
        /// Converts a list of transactions to string for displayablity 
        /// </summary>
        /// <param name="trasactions"> list of transactions</param>
        /// <returns> 2 dimensional list of strings </returns>
        protected IList<IList<string>> ToListOfListOfStrings(IList<ITransaction> trasactions)
        {
            IList<IList<string>> list = new List<IList<string>>();

            foreach (var row in this.transactions)
            {
                List<string> columns = new List<string>
                {
                    row.PurchasedDate == null ? string.Empty : row.PurchasedDate.Value.ToShortDateString(),
                    row.EquitySymbol,
                    row.Quanity.ToString(),
                    row.PurchasePrice.ToString(),
                    row.Cost.ToString(),
                    row.SaleDate == null ? string.Empty : row.SaleDate.Value.ToShortDateString(),
                    row.SalePrice.ToString(),
                    row.SaleProceeds.ToString(),
                    row.Dividends.ToString(),
                };

                list.Add(columns);
            }

            return list;
        }

        /// <summary>
        /// Get entire transaction table in the database
        /// </summary>
        private void DoUpdate()
        {
            DataTable dt = new DataTable();

            this.transactions.Clear();

            try
            {
                dt = this.database.GetTableData(TransactionTableSchema.TransactionTableName);

                foreach (DataRow row in dt.AsEnumerable())
                {
                    TransactionHistory t = new TransactionHistory();

                    t.RowID = Convert.ToInt32(row[(int)TransactionTableSchema.ColumnIndex.ID]);
                    t.PurchasedDate = DataConverter.Date(row[(int)TransactionTableSchema.ColumnIndex.PurchaseDate].ToString());
                    t.EquitySymbol = row[(int)TransactionTableSchema.ColumnIndex.EquityName].ToString();
                    t.Quanity = Convert.ToDouble(row[(int)TransactionTableSchema.ColumnIndex.Quantity]);
                    t.Cost = Convert.ToDecimal(row[(int)TransactionTableSchema.ColumnIndex.CostBasis]);
                    t.SaleDate = DataConverter.Date(row[(int)TransactionTableSchema.ColumnIndex.SoldDate].ToString());
                    t.SaleProceeds = DataConverter.NullableDecimal(row[(int)TransactionTableSchema.ColumnIndex.MarketValue].ToString());
                    t.Dividends = DataConverter.NullableDecimal(row[(int)TransactionTableSchema.ColumnIndex.Dividends].ToString());
                    if (t.Dividends == null)
                    {
                        ////tx.Dividends = this.GetDividend(tx.EquitySymbol, tx.PurchasedDate, tx.SaleDate).Sum(x => x) * (decimal)tx.Quanity;
                    }

                    this.transactions.Add(t);
                }

                this.SortByRowID((List<ITransaction>)this.transactions);

                this.OnListChanged(this.ToListOfListOfStrings((IList<ITransaction>)this.transactions) as IList<IList<string>>);
            }
            catch
            {
                // Make sure list is empty if an expection is caught
                this.transactions.Clear();
            }
        }

        /// <summary>
        /// Sort the list of transactions by their row id
        /// </summary>
        /// <param name="transactions">reference to list of transactions</param>
        private void SortByRowID(List<ITransaction> transactions)
        {
            transactions.Sort(delegate(ITransaction p1, ITransaction p2)
            {
                return p1.RowID.CompareTo(p2.RowID);
            });
        }
    }
}
