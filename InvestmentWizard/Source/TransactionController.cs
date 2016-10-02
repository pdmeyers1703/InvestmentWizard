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
        private IListObservable transactionsObserver;
        private IListObservable openTransactionsObserver;
        private IFinancialData server;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionsObserver">observer for entire transactions list </param>
        /// <param name="openTransactionsObserver">observer for all open transactions</param>
        /// <param name="dataServer">Data server for real-time quotes</param>
        public TransactionController(
            IListObservable transactionsObserver, 
            IListObservable openTransactionsObserver,
            IFinancialData dataServer) 
        {
            this.transactionsObserver = transactionsObserver;
            this.openTransactionsObserver = openTransactionsObserver;
            this.server = dataServer;
        }

        /// <summary>
        /// Sets the list models observers
        /// </summary>
        /// <param name="listChangedObserver">observer event handler</param>
        public void RegisterTransactionsListObserver(ListChangedEventHandler listChangedObserver)
        {
            this.transactionsObserver.RegisterObserver(listChangedObserver);
        }

        /// <summary>
        /// Set observer for the open transactions list
        /// </summary>
        /// <param name="listChangedObserver">observer event handler</param>
        public void RegisterOpenTransactionsListObserver(ListChangedEventHandler listChangedObserver)
        {
            this.openTransactionsObserver.RegisterObserver(listChangedObserver);
        }

        /// <summary>
        /// Updates the model
        /// </summary>
        public void Update()
        {
            this.transactionsObserver.Update();
            this.openTransactionsObserver.Update();
        }

        public List<ITransaction> GetTransactionForThisYear(string symbol)
        {
            return new List<ITransaction>(); ///this.transactionsObserver.Transactions.Where(a => a.EquitySymbol == symbol)
                                            ///         .Where(b => b.SaleDate == null || 
                                            ///             b.SaleDate.Value.Year == DateTime.Now.Year).ToList();
        }

        public bool AddPosition(DateTime date, string stock, double quantity, decimal cost)
        {
            return true; //// this.transactionsObserver.Add(date, stock, quantity, cost);
        }

        public bool SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds)
        {
            return true; //// this.transactionsObserver.Sell(rowIndex, saleDate, quantity, saleProceeds);
        }

        public bool SplitPosition(string equitySymbol, double splitRatio)
        {
            return true;  ////this.transactionsObserver.Split(equitySymbol, splitRatio);
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
