namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Controller for transaction data store
    /// </summary>
    public interface ITransactionController
    {
       /// <summary>
       /// List of transactions
       /// </summary>
        List<ITransaction> History { get; }

        /// <summary>
        /// Gets list of currently open transactions
        /// </summary>
        List<ITransaction> OpenList { get; }

        /// <summary>
        /// Sets the view for the controller
        /// </summary>
        ITransactionsView View { get; set; }

        void Update();

        bool AddPosition(DateTime date, string stock, double quantity, decimal cost);

        bool SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

        bool SplitPosition(string equitySymbol, double splitRatio);
    }
}
