// <copyright file="IViewFormatter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System.Drawing;

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

		/// <summary>
		/// Determines if a numeric value is positive or negative and assigns 
		/// a color based on the result
		/// </summary>
		/// <param name="cellString">Value in string format.</param>
		/// <returns>Color determined.</returns>
		Color GetCellColor(string cellString);
	}
}
