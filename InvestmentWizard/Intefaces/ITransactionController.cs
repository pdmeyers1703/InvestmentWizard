namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Controller for transaction model
    /// </summary>
    public interface ITransactionController
    {
        /// <summary>
        /// Set observer for complete transaction list
        /// </summary>
        /// <param name="listChangedObserver">observer event handler</param>
        void RegisterTransactionsListObserver(ListChangedEventHandler listChangedObserver);

        /// <summary>
        /// Set observer for the open transactions list
        /// </summary>
        /// <param name="listChangedObserver">observer event handler</param>
        void RegisterOpenTransactionsListObserver(ListChangedEventHandler listChangedObserver);

        void Update();

        bool AddPosition(DateTime date, string stock, double quantity, decimal cost);

        bool SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

        bool SplitPosition(string equitySymbol, double splitRatio);
    }
}
