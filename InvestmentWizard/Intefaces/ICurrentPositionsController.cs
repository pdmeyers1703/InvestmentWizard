// <copyright file="ICurrentpositionsController.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{ 
	public interface ICurrentPositionsController
	{
		/// <summary>
		/// Sets controllers view
		/// </summary>
		ICurrentPositionsView CurrentPositionsView { get; set; }

		/// <summary>
		/// Initialize controller features
		/// </summary>
		void Initialize();

		/// <summary>
		/// Updates the models
		/// </summary>
		void Update();
	}
}
