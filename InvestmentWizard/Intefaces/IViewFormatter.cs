// <copyright file="IViewFormatter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System.Collections.Generic;
	using System.Drawing;

	/// <summary>
	/// Basic formatter of data to be viewed.
	/// </summary>
	/// <typeparam name="InputType">Data type of data to be formatted.</typeparam>
	public interface IViewFormatter<T>
	{
		/// <summary>
		/// Formats data types, decial places, scaling factors and adds special characters
		/// </summary>
		/// <param name="viewData">.Data to be formatted</param>
		/// <returns>Formatted Data</returns>
		List<string> FormatDataToStringList(T viewData);

		/// <summary>
		/// Determines if a numeric value is positive or negative and assigns 
		/// a color based on the result
		/// </summary>
		/// <param name="cellString">Value in string format.</param>
		/// <returns>Color determined.</returns>
		Color GetTextColor(string cellString);
	}
}
