// <copyright file="IViewFormatter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	/// <summary>
	/// Basic formatter of data to be viewed.
	/// </summary>
	/// <typeparam name="InputType">Data type of data to be formatted.</typeparam>
	/// <typeparam name="OutputType">Data type of the formatted data.</typeparam>
	public interface IViewFormatter<InputType, OutputType>
	{
		/// <summary>
		/// Formats data types, decial places, scaling factors and adds special characters
		/// </summary>
		/// <param name="viewData">.Data to be formatted</param>
		/// <returns>Formatted Data</returns>
		OutputType FormatData(InputType viewData);
	}
}
