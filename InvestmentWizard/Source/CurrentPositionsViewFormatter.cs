// <copyright file="CurrentPositionsViewFormatter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;

	/// <summary>
	/// Formats current positions data for data grid view
	/// </summary>
	public class CurrentPositionsViewFormatter : IViewFormatter<ICurrentPosition>
	{
		/// <summary>
		/// Basic formatting of data types, decimals places, special characters (%,$)
		/// </summary>
		/// <param name="currentPosition">A single position</param>
		/// <returns>A list of strings representing a poistions formatted for data grid view</returns>
		public List<string> FormatDataToStringList(ICurrentPosition currentPosition)
		{
			List<string> formattedData = new List<string>();

			formattedData.Add(currentPosition.StockTicker.ToString());
			formattedData.Add("$" + string.Format("{0:N2}", currentPosition.CurrentPrice));
			formattedData.Add("$" + currentPosition.PriceChange);
			formattedData.Add(currentPosition.PriceChangePercent);
			formattedData.Add(string.Format("{0:N1}%", currentPosition.YtdPercentGainLoss * 100));
			formattedData.Add(string.Format("{0:N2}", currentPosition.Quantity));
			formattedData.Add(string.Format("${0:N2}", currentPosition.Cost));
			formattedData.Add(string.Format("${0:N2}", currentPosition.CurrentMarketValue));
			formattedData.Add(string.Format("${0:N2}", currentPosition.GainLoss));
			formattedData.Add(string.Format("{0:N1}%", currentPosition.PercentGainLoss * 100));
			return formattedData;
		}

		/// <summary>
		/// Determines if a numeric value is positive or negative and assigns 
		/// a color based on the result
		/// </summary>
		/// <param name="cellString">Value in string format.</param>
		/// <returns>Color determined.</returns>
		public Color GetTextColor(string cellString)
		{
			Color color = Color.Black;

			try
			{
				if (Convert.ToDouble(cellString.Replace("$", string.Empty).Replace("%", string.Empty)) >= 0)
				{
					color = Color.Green;
				}
				else
				{
					color = Color.Red;
				}
			}
			catch (FormatException)
			{
			}
			
			return color;
		}
	}
}
