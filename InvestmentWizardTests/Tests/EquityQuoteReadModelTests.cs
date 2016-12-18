// <copyright file="EquityQuoteReadModelTests.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestopediaTests.Tests
{
	using System;
	using System.Collections.Generic;
	using InvestmentWizard;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class EquityQuoteReadModelTests
	{
		private Mock<IFinancialData> mockFinancialData;
		private Mock<ListChangedEventHandler<PriceQuote>> mockEventHandler;
		private IEquityQuoteReadModel model;

		[SetUp]
		public void SetUp()
		{
			this.mockFinancialData = new Mock<IFinancialData>();
			this.mockEventHandler = new Mock<ListChangedEventHandler<PriceQuote>>();

			List<PriceQuote> onePrice = new List<PriceQuote>() { Any.PriceQuote };
			this.mockFinancialData.Setup(f => f.GetPrices(new List<string>() { Any.EquitySymbol }, out onePrice));

			List<PriceQuote> twoPrices = new List<PriceQuote>() { Any.PriceQuote, Any.OtherPriceQuote };
			this.mockFinancialData.Setup(f => f.GetPrices(new List<string>() { Any.EquitySymbol, Any.OtherEquitySymbol }, out twoPrices));

			this.model = new EquityQuoteReadModel(this.mockFinancialData.Object);
		}

		[Test, TestCaseSource("TestCaseEquitySymbols")]
		public void UpdateCallsFinancialDataGetRealtimePricesWithCorrectEquitySymbols(string[] equitySymbols)
		{
			this.model.AddRealTimeQuote(equitySymbols);

			this.model.Update();

			List<PriceQuote> priceQuotes;
			if (equitySymbols.Length == 0)
			{
				this.mockFinancialData.Verify(f => f.GetPrices(new List<string>(equitySymbols), out priceQuotes), Times.Never);
			}
			else
			{
				this.mockFinancialData.Verify(f => f.GetPrices(new List<string>(equitySymbols), out priceQuotes), Times.Once);
			}
		}

		[Test, TestCaseSource("TestCaseEquitySymbolsAndDate")]
		public void UpdateCallsFinancialDataGetHistoricalPricesWithCorrectEquitySymbols(Tuple<string, DateTime>[] equitySymbolsAndDates)
		{
			this.model.AddHistoricalQuote(equitySymbolsAndDates);

			this.model.Update();

			string historicalQuote = string.Empty;
			if (equitySymbolsAndDates.Length == 0)
			{
				this.mockFinancialData.Verify(f => f.GetHistoricalPrice(string.Empty, It.IsAny<DateTime>(), out historicalQuote), Times.Never);
			}
			else if (equitySymbolsAndDates.Length > 0)
			{
				this.mockFinancialData.Verify(f => f.GetHistoricalPrice(equitySymbolsAndDates[0].Item1, equitySymbolsAndDates[0].Item2, out historicalQuote), Times.Once);

				if (equitySymbolsAndDates.Length > 1)
				{
					this.mockFinancialData.Verify(f => f.GetHistoricalPrice(equitySymbolsAndDates[1].Item1, equitySymbolsAndDates[1].Item2, out historicalQuote), Times.Once);
				}
			}
		}

		[TestCase(Any.EquitySymbol)]
		public void GetRealTimeQuoteReturnsCorrectPriceQuote(string symbol)
		{
			this.model.AddRealTimeQuote(new string[] { symbol });
			this.model.Update();

			PriceQuote quote = this.model.GetRealTimeQuote(symbol);

			Assert.AreEqual(symbol, quote.Symbol);
		}

		[Test]
		public void GetRealTimeQuoteReturnsNullWhenQuoteIsNotRegistered()
		{
			this.model.Update();

			PriceQuote quote = this.model.GetRealTimeQuote(Any.EquitySymbol);

			Assert.IsNull(quote);
		}

		[TestCase(Any.EquitySymbol)]
		public void GetHistoricalQuoteReturnsCorrectPrice(string symbol)
		{
			string expectedPrice = Any.Price.ToString();
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(symbol, Any.Date);
			this.mockFinancialData.Setup(f => f.GetHistoricalPrice(symbol, Any.Date, out expectedPrice));
			this.model.AddHistoricalQuote(new Tuple<string, DateTime>[] { symbolAndDate });
			this.model.Update();

			string price = this.model.GetHistoricalQuote(symbolAndDate);

			Assert.AreEqual(expectedPrice, price);
		}

		[Test]
		public void GetHistoricalQuoteReturnsAnEmptyStringWhenQuoteIsNotRegistered()
		{
			string expectedPrice = Any.Price.ToString();
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.EquitySymbol, Any.Date);
			this.mockFinancialData.Setup(f => f.GetHistoricalPrice(Any.EquitySymbol, Any.Date, out expectedPrice));
			this.model.Update();

			string price = this.model.GetHistoricalQuote(symbolAndDate);

			Assert.AreEqual(string.Empty, price);
		}

		[TestCase(34.21, 18.73)]
		[TestCase(34.21, 34.21)]
		[TestCase(18.73, 34.21)]
		public void GetYtdChangeReturnsThePriceDifference(decimal expectedCurrentPrice, decimal expectedPreviousPrice)
		{
			string expectedHistricalPrice = expectedPreviousPrice.ToString();
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.Date);
			this.mockFinancialData.Setup(f => 
				f.GetHistoricalPrice(Any.OtherEquitySymbol, Any.Date, out expectedHistricalPrice));

			List<PriceQuote> expectedCurrentQuote = new List<PriceQuote>()
			{ new PriceQuote(Any.OtherEquitySymbol, "", expectedCurrentPrice, 0.0m, "", "") };
			this.mockFinancialData.Setup(
				f => f.GetPrices(new List<string>() { Any.OtherEquitySymbol }, out expectedCurrentQuote));

			this.model.AddYtdChange(new Tuple<string, DateTime>[] { symbolAndDate });
			this.model.Update();

			string priceChange = this.model.GetYtdPriceChanged(symbolAndDate);

			Assert.AreEqual((expectedCurrentPrice - expectedPreviousPrice).ToString(), priceChange);
		}

		[Test]
		public void GetYtdChangeReturnsAnEmptyStringWhenCurrentPriceIsNotFound()
		{
			string expectedHistricalPrice = Any.Price.ToString();
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.Date);
			this.mockFinancialData.Setup(f =>
				f.GetHistoricalPrice(Any.OtherEquitySymbol, Any.Date, out expectedHistricalPrice));

			this.model.AddHistoricalQuote(new Tuple<string, DateTime>[] { symbolAndDate });
			this.model.Update();

			string priceChange = this.model.GetYtdPriceChanged(symbolAndDate);

			Assert.AreEqual(string.Empty, priceChange);
		}

		[Test]
		public void GetYtdChangeReturnsAnEmptyStringWhenHistoricalPriceIsNotFound()
		{
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.Date);
			List<PriceQuote> expectedCurrentQuote = new List<PriceQuote>()
			{ new PriceQuote(Any.OtherEquitySymbol, "", Any.Price, 0.0m, "", "") };
			this.mockFinancialData.Setup(
				f => f.GetPrices(new List<string>() { Any.OtherEquitySymbol }, out expectedCurrentQuote));

			this.model.AddRealTimeQuote(new string[] { Any.OtherEquitySymbol });
			this.model.Update();

			string priceChange = this.model.GetYtdPriceChanged(symbolAndDate);

			Assert.AreEqual(string.Empty, priceChange);
		}

		[Test]
		public void GetYtdChangeReturnsAnEmptyStringWhenCurrentPriceAndHistoricalPriceIsNotFound()
		{
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.Date);
			this.model.Update();

			string priceChange = this.model.GetYtdPriceChanged(symbolAndDate);

			Assert.AreEqual(string.Empty, priceChange);
		}

		[TestCase(34.21, 18.73, "82.65")]
		[TestCase(34.21, 34.21, "0.00")]
		[TestCase(18.73, 34.21, "-45.25")]
		[TestCase(34.72, 0.00, "0.00")]
		public void GetYtdChangePercentageReturnsThePriceDifference(decimal expectedCurrentPrice, decimal expectedPreviousPrice, string expectedChange)
		{
			string expectedHistricalPrice = expectedPreviousPrice.ToString();
			Tuple<string, DateTime> symbolAndDate = new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.Date);
			this.mockFinancialData.Setup(f =>
				f.GetHistoricalPrice(Any.OtherEquitySymbol, Any.Date, out expectedHistricalPrice));

			List<PriceQuote> expectedCurrentQuote = new List<PriceQuote>()
			{ new PriceQuote(Any.OtherEquitySymbol, "", expectedCurrentPrice, 0.0m, "", "") };
			this.mockFinancialData.Setup(
				f => f.GetPrices(new List<string>() { Any.OtherEquitySymbol }, out expectedCurrentQuote));

			this.model.AddYtdChange(new Tuple<string, DateTime>[] { symbolAndDate });
			this.model.Update();

			string priceChange = this.model.GetYtdPriceChangedPercent(symbolAndDate);

			Assert.AreEqual(expectedChange, priceChange);
		}

		private static class Any
		{
			public const string EquitySymbol = "ABC";
			public const string OtherEquitySymbol = "DEF";
			public const decimal Price = 34.21m;
			public static readonly PriceQuote PriceQuote = new PriceQuote(EquitySymbol, "", 0.0m, 0.0m, "", "");
			public static readonly PriceQuote OtherPriceQuote = new PriceQuote(OtherEquitySymbol, "", 0.0m, 0.0m, "", "");
			public static readonly DateTime Date = Convert.ToDateTime("5/5/1975");
			public static readonly DateTime OtherDate = Convert.ToDateTime("11/16/1974");
		}
		
		private static object[] TestCaseEquitySymbols =
		{
			new string[] { },
			new string[] { Any.EquitySymbol },
			new string[] { Any.EquitySymbol, Any.OtherEquitySymbol},
		};

		private static object[] TestCaseEquitySymbolsAndDate =
		{
			new Tuple<string, DateTime>[] { },
			new Tuple<string, DateTime>[] { new Tuple<string, DateTime>(Any.EquitySymbol, Any.Date) },
			new Tuple<string, DateTime>[] { new Tuple<string, DateTime>(Any.EquitySymbol, Any.Date), new Tuple<string, DateTime>(Any.OtherEquitySymbol, Any.OtherDate) },
		};
	}
}
