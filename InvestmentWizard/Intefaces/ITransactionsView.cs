namespace InvestmentWizard
{
	using System.Collections.Generic;

	/// <summary>
	/// Actions the view must implement
	/// </summary>
	public interface ITransactionsView
	{
		/// <summary>
		/// Passing the view handler to the controller
		/// </summary>
		/// <param name="handler">list change handler</param>
		void RegisterCompleteTransactionList(out ListChangedEventHandler<IList<string>> handler);

		/// <summary>
		/// Passing the view handlers to the controller
		/// </summary>
		/// <param name="handler"></param>
		void RegisterOpenTransactionList(out ListChangedEventHandler<IList<string>> handler);
	}
}
