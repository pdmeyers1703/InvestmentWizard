namespace PetersInvestmentProgram
{
    using System;
    using System.Collections.Generic;

    public class TransactionModelFactory
    {
        public static ITransactionsModel Create()
        {
            return Program.MyContainer.GetInstance<ITransactionsModel>();
        }
    }
}
