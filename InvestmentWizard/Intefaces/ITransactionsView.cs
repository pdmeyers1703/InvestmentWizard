namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Actions the view must implement
    /// </summary>
    public interface ITransactionsView
    {
        /// <summary>
        /// Updates the transactions data grid view
        /// </summary>
        /// <param name="transactions">List of transactions</param>
        void UpdateTransactionsDataGrid(IList<ITransaction> transactions);
    }
}
