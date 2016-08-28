namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITransactionsModel
    {
        IList<ITransaction> Transactions { get; set; }

        void Update();

        bool Add(DateTime date, string stock, double quantity, decimal cost);

        bool Sell(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

        bool Split(string equitySymbol, double splitRatio);
    }
}
