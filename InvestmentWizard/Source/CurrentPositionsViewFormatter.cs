// <copyright file="CurrentPositionsViewFormatter.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System.Collections.Generic;

	/// <summary>
	/// Formats current positions data for data grid view
	/// </summary>
	public class CurrentPositionsViewFormatter : IViewFormatter<ICurrentPosition, List<string>>
	{
		/// <summary>
		/// Basic formatting of data types, decimals places, special characters (%,$)
		/// </summary>
		/// <param name="currentPosition">A single position</param>
		/// <returns>A list of strings representing a poistions formatted for data grid view</returns>
		public List<string> FormatData(ICurrentPosition currentPosition)
		{
			List<string> formattedData = new List<string>();

			formattedData.Add(currentPosition.StockTicker.ToString());
			formattedData.Add("$" + string.Format("{0:N2}", currentPosition.CurrentPrice));
			formattedData.Add(currentPosition.PriceChange);
			formattedData.Add(currentPosition.PriceChangePercent);
			formattedData.Add(string.Format("{0:N1}%", currentPosition.YtdPercentGainLoss * 100));
			formattedData.Add(string.Format("{0:N2}", currentPosition.Quantity));
			formattedData.Add(string.Format("${0:N2}", currentPosition.Cost));
			formattedData.Add(string.Format("${0:N2}", currentPosition.CurrentMarketValue));
			formattedData.Add(string.Format("${0:N2}", currentPosition.GainLoss));
			formattedData.Add(string.Format("{0:N1}%", currentPosition.PercentGainLoss * 100));
			return formattedData;
		}
	}
}
