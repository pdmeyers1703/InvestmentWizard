namespace InvestmentWizard
{
    using System.Collections.Generic;

    public interface IListReadModel
    {
        IList<ITransaction> OpenTransactionsList { get; }
    }
}
