// <copyright file="CurrentPosition.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestmentWizard
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// A container for a single current position
	/// </summary>
	public class OpenPositions : ICurrentPosition
	{
		private string stockTicker;
		private double? quantity;
		private decimal cost;
		private decimal? currentPrice;
		private string priceChange;
		private string priceChangePercent;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stockTicker"></param>
		/// <param name="quantity"></param>
		/// <param name="cost"></param>
		/// <param name="currentPrice"></param>
		/// <param name="priceChange"></param>
		/// <param name="priceChangePercent"></param>
		public OpenPositions(
			string stockTicker, 
			double? quantity, 
			decimal cost, 
			decimal? currentPrice, 
			string priceChange, 
			string priceChangePercent)
		{
			this.stockTicker = stockTicker;
			this.quantity = quantity;
			this.cost = cost;
			this.currentPrice = currentPrice;
			this.priceChange = priceChange;
			this.priceChangePercent = priceChangePercent;

			if (this.quantity < 0)
			{
				throw new ArgumentException("Current positon quanitiy for " + this.stockTicker + stockTicker + " less than zero.");
			}

			if (this.cost < 0)
			{
				throw new ArgumentException("Current positon cost for " + this.stockTicker + stockTicker + " less than zero.");
			}
		}

		public string StockTicker
		{
			get
			{
				return this.stockTicker.ToUpper();
			}
		}

		public double? Quantity 
		{
			get
			{
				return this.quantity.HasValue ? (double?)Math.Round(this.quantity.Value, 3) : null;
			}

			set
			{
				this.quantity = value;
			}
		}

		public decimal Cost
		{
			get
			{
				return Math.Round(this.cost, 2);
			}

			set
			{
				this.cost = value;
			}
		}

		public decimal CurrentMarketValue 
		{
			get
			{
				return this.currentPrice.HasValue ? (decimal)(this.Quantity * (double)this.CurrentPrice) : 0.00m;
			}
		}

		public decimal GainLoss 
		{
			get
			{
				return this.CurrentMarketValue - this.Cost;
			}
		}

		public double PercentGainLoss 
		{
			get
			{
				return (double)Math.Round((this.CurrentMarketValue - this.Cost) / this.Cost, 3);
			}
		}

		public decimal? CurrentPrice
		{
			get
			{
				return this.currentPrice.HasValue ? (decimal?)Math.Round(this.currentPrice.Value, 2) : null;
			}
		}

		public string PriceChange
		{
			get
			{
				return this.priceChange;
			}
		}

		public string PriceChangePercent
		{
			get
			{
				return this.priceChangePercent;
			}
		}

		public double YtdPercentGainLoss { get; set; }

		public void UpdateRealTimeQuote(PriceQuote priceQuote)
		{
			this.currentPrice = priceQuote.LastPrice;
			this.priceChange = priceQuote.PriceChangeAbsolute;
			this.priceChangePercent = priceQuote.PriceChangePercent;
		}

		public List<string> ToStringList()
		{
			List<string> list = new List<string>();
			list.Add(this.StockTicker);
			list.Add(this.Quantity.ToString());
			list.Add(this.Cost.ToString());
			list.Add(this.CurrentMarketValue.ToString());
			list.Add(this.GainLoss.ToString());
			list.Add(this.PercentGainLoss.ToString());
			list.Add(this.CurrentPrice.ToString());
			list.Add(this.PriceChange);
			list.Add(this.PriceChangePercent);

			return list;
		}
	}
}
