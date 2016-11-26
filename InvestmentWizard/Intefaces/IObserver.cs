// <copyright file="IObserver.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	public interface IObserver<T>
	{
		ListChangedEventHandler<T> GetObserverEventHandler();
	}
}
