// <copyright file="PriceQuote.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{ 
    public class PriceQuote
    {
		public PriceQuote()
		{
		}

		public PriceQuote(
			string symbol, 
			string name, 
			decimal lastPrice, 
			decimal prevClose,
			string priceChangeAbsolute,
			string priceChangePercent)
		{
			this.Symbol = symbol;
			this.Name = name;
			this.LastPrice = lastPrice;
			this.PreviousClose = prevClose;
			this.PriceChangeAbsolute = priceChangeAbsolute;
			this.PriceChangePercent = priceChangePercent;
		}

		public string Symbol { get; set; }

		public string Name { get; set; }

		public decimal LastPrice { get; set; }

		public decimal PreviousClose { get; set; }

		public string PriceChangeAbsolute { get; set; }

		public string PriceChangePercent { get; set; }
	}
}
