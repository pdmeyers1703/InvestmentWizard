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

			container.Register<IViewFormatter<ICurrentPosition, List<string>>, CurrentPositionsViewFormatter>(Lifestyle.Singleton);

			var registration = Lifestyle.Singleton.CreateRegistration<TransactionsListReadModel>(container);
			container.RegisterConditional(typeof(IListObservable<ITransaction>), registration, o => o.Consumer.Target.Parameter.Name.Equals("transactionsObserver"));
			container.RegisterConditional<IListObservable<ITransaction>, OpenTransactionsListReadModel>(Lifestyle.Singleton, o => o.Consumer.Target.Parameter.Name.Equals("openTransactionsObserver"));
			container.Register<ITransactionsListWriter, TransactionsListWriteModel>(Lifestyle.Singleton);

			var currentpositionsModelRegistration = Lifestyle.Singleton.CreateRegistration<CurrentPositionModel>(container);
			container.AddRegistration(typeof(IListObservable<ICurrentPosition>), currentpositionsModelRegistration);
			container.AddRegistration(typeof(IObserver<ITransaction>), currentpositionsModelRegistration);

			container.Register<ITransactionController, TransactionController>(Lifestyle.Singleton);

			var currentPositionControllerRegistration = Lifestyle.Singleton.CreateRegistration<CurrentPositionsController>(container);
			container.AddRegistration(typeof(ICurrentPositionsController), currentPositionControllerRegistration);

			var mainRegistration = Lifestyle.Singleton.CreateRegistration<Main>(container);
			container.AddRegistration(typeof(ITransactionsView), mainRegistration);
			container.AddRegistration(typeof(ICurrentPositionsView), mainRegistration);

			container.Verify();
        }
    }
}
