namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using SimpleInjector;

    public static class Program
    {
        private static Container container;

        public static Container MyContainer
        { 
            get
            {
                return container;
            }
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootStrap();

            Application.Run(container.GetInstance<Main>());
        }

        /// <summary>
        /// Register all containers
        /// </summary>
        private static void BootStrap()
        {
            container = new Container();

            container.Register<IDatabase>(() => DatabaseFactory.Create(new AccessDB()), Lifestyle.Singleton);
            container.Register<IFinancialData, YahooFinancalDataClient>(Lifestyle.Singleton);
            container.Register<ITransactionsModel, TransactionsModel>(Lifestyle.Singleton);
            container.Register<ITransactionController, TransactionController>(Lifestyle.Singleton);
            container.Register<ICurrentPositionsModel, CurrentPositionModel>(Lifestyle.Singleton);
            container.Register<ITransactionsView, Main>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
