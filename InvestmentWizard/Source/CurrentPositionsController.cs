// <copyright file="CurrentPositionsController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	/// <summary>
	/// Controller for the OpenPosition datagrid view.
	/// </summary>
	public class CurrentPositionsController : ICurrentPositionsController
	{
		private IListObservable<ICurrentPosition> currentPositionObserver;
		private ICurrentPositionsView currentPositionsView;
		private IListObservable<ITransaction> openTransactionsObserver;
		private IObserver<ITransaction> currentpositionsObservableModel;

		/// <summary>
		/// Constuctor that takes an observer.
		/// </summary>
		/// <param name="currentPositionObserver">Observer of current positions model</param>
		public CurrentPositionsController(
			IListObservable<ICurrentPosition> currentPositionObserver, 
			IListObservable<ITransaction> openTransactionsObserver,
			IObserver<ITransaction> currentpositionsObservableModel)
		{
			this.currentPositionObserver = currentPositionObserver;
			this.openTransactionsObserver = openTransactionsObserver;
			this.currentpositionsObservableModel = currentpositionsObservableModel;
		}

		/// <summary>
		/// Property for the view to be set.
		/// </summary>
		public ICurrentPositionsView CurrentPositionsView
		{
			get
			{
				return this.currentPositionsView;
			}

			set
			{
				this.currentPositionsView = value;
			}
		}

		public void Initialize()
		{
			if (this.currentPositionsView != null)
			{
				ListChangedEventHandler<ICurrentPosition> listChangeEventHander;
				this.currentPositionsView.RegisterCurrentPositionsList(out listChangeEventHander);
				this.currentPositionObserver.RegisterObserver(listChangeEventHander);
			}

			ListChangedEventHandler<ITransaction> openTransactionsEventHandler = 
				this.currentpositionsObservableModel.GetObserverEventHandler();
			this.openTransactionsObserver.RegisterObserver(openTransactionsEventHandler);
		}

		/// <summary>
		/// Update all models
		/// </summary>
		public void Update()
		{
			this.currentPositionObserver.Update();
		}
	}
}
