namespace InvestmentWizard
{
	using System;

	/// <summary>
	/// Controller for transaction model
	/// </summary>
	public interface ITransactionController
	{
		/// <summary>
		/// Sets controllers view
		/// </summary>
		 ITransactionsView TransactionView { get; set; }

		/// <summary>
		/// Initialize controller features
		/// </summary>
		void Initialize();

		void Update();

		void AddPosition(DateTime date, string stock, double quantity, decimal cost);

		void SellPosition(int rowIndex, DateTime saleDate, double quantity, decimal saleProceeds);

		bool SplitPosition(string equitySymbol, double splitRatio);
	}
}
