namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Controller for all transactions
    /// </summary>
    public class TransactionController : ITransactionController
    {
        /// <summary>
        /// private Members
        /// </summary>
        private ITransactionsModel transactionsModel;
        private ITransactionsView transactionsView;
        private IFinancialData server;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionsDB">transactions model</param>
        /// <param name="dataServer">Data server for real-time quotes</param>
        public TransactionController(ITransactionsModel transactionsModel, IFinancialData dataServer) 
        {
            if (transactionsModel == null)
            {
                throw new ArgumentNullException("Database refernce is Null");
            }
            else
            {
                this.transactionsModel = transactionsModel;
            }

            if (dataServer == null)
            {
                throw new ArgumentException("Data Server reference is NULL");
            }
            else
            {
                this.server = dataServer;
            }
        }

        public List<ITransaction> History
        {
            get
            {
                return this.transactionsModel.Transactions.OrderByDescending(a => a.PurchasedDate).Reverse().ToList();
            }

            private set
            {
                this.transactionsModel.Transactions = value;
            }
        }

        public List<ITransaction> OpenList
        {
            get
            {
                return (from t in this.transactionsModel.Transactions.AsEnumerable()
                        where t.SaleDate == null
                        select t).ToList();
            }
        }

        /// <summary>
        /// Sets the view for the controller
        /// </summary>
        public ITransactionsView View
        {
            get
            {
                return this.transactionsView;
            }

            set
            {
                this.transactionsView = value;
            }
        }

        /// <summary>
        /// Updates the model and sends the results to the view
        /// </summary>
        public void Update()
        {
            this.transactionsModel.Update();
            this.transactionsView.UpdateTransactionsDataGrid(
                this.transactionsModel.Transactions.OrderByDescending(a => a.PurchasedDate).Reverse().ToList());
        }

        public List<ITransaction> GetTransactionForThisYear(string symbol)
        {
            return this.transactionsModel.Transactions.Where(a => a.EquitySymbol == symbol)
                            .Where(b => b.SaleDate == null || 
                                b.SaleDate.Value.Year == DateTime.Now.Year).ToList();
        }

        public bool AddPosition(DateTime date, string stock, double quantity, decimal cost)
        {
            return this.transactionsModel.Add(date, stock, quantity, cost);
        }

        public bool SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds)
        {
            return this.transactionsModel.Sell(rowIndex, saleDate, quantity, saleProceeds);
        }

        public bool SplitPosition(string equitySymbol, double splitRatio)
        {
            return this.transactionsModel.Split(equitySymbol, splitRatio);
        }

        /// <summary>
        /// Returns all the dividend payment for an equity between a particular time range
        /// </summary>
        /// <param name="tickerSymbol">Equity symbol</param>
        /// <param name="startDate"> Begining of time range</param>
        /// <param name="endDate">End of time range</param>
        /// <returns>List of dividend payments</returns>
        private List<decimal> GetDividend(string tickerSymbol, DateTime? startDate, DateTime? endDate)
        {
            List<decimal> dividends = new List<decimal>();
            if (startDate == null)
            {
                throw new Exception();
            }

            if (endDate == null)
            {
                endDate = DateTime.Now;
            }

            this.server.GetDividendsOverTimeSpan(tickerSymbol, startDate.Value, endDate.Value, ref dividends);
            return dividends;
        }
    } 
}
