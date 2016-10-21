namespace InvestmentWizard
{
    using System.Collections.Generic;
    using System.Linq;

    public class OpenTransactionsListReadModel : TransactionsListReadModel
    {
        public OpenTransactionsListReadModel(IDatabase transactionsDatabase) : base(transactionsDatabase)
        {
        }

        public override void Update()
        {
			base.Update();
            IList<ITransaction> openTransactions = this.Transactions.Where(t => t.SaleDate == null).ToList();
            this.OnListChanged(this.ToListOfListOfStrings(openTransactions));
        }
    }
}
