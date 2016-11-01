// <copyright file="ICurrentPositionsView.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	/// <summary>
	/// Actions the  view must implement
	/// </summary>
	public interface ICurrentPositionsView
	{
		/// <summary>
		/// Passing the view handler to the controller
		/// </summary>
		/// <param name="handler">list change handler</param>
		void RegisterCurrentPositionsList(out ListChangedEventHandler<ICurrentPosition> handler);
	}
}
