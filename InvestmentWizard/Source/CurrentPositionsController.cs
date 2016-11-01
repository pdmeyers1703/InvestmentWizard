// <copyright file="CurrentPositionsController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Controller for the OpenPosition datagrid view.
	/// </summary>
	public class CurrentPositionsController : ICurrentPositionsController
	{
		private IListObservable<ICurrentPosition> currentPositionObserver;
		private ICurrentPositionsView currentPositionsView;

		/// <summary>
		/// Constuctor that takes an observer.
		/// </summary>
		/// <param name="currentPositionObserver">Observer of current positions model</param>
		public CurrentPositionsController(IListObservable<ICurrentPosition> currentPositionObserver)
		{
			this.currentPositionObserver = currentPositionObserver;

			if (this.currentPositionObserver == null)
			{
				throw new ArgumentNullException("Current Position Observer is null in Current Position Controller.");
			}
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
