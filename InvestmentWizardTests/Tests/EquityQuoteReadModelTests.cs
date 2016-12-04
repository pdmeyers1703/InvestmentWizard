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

			List<PriceQuote> onePrice = new List<PriceQuote>() { Any.Price };
			this.mockFinancialData.Setup(f => f.GetPrices(new List<string>() { Any.EquitySymbol }, out onePrice));

			List<PriceQuote> twoPrices = new List<PriceQuote>() { Any.Price, Any.OtherPrice };
			this.mockFinancialData.Setup(f => f.GetPrices(new List<string>() { Any.EquitySymbol, Any.OtherEquitySymbol }, out twoPrices));

			this.model = new EquityQuoteReadModel(this.mockFinancialData.Object);
		}

		[Test, TestCaseSource("TestCaseEquitySymbols")]
		public void UpdateCallsFinancialDataGetPricesWithCorrectEquitySymbols(string[] equitySymbols)
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

		private static class Any
		{
			public const string EquitySymbol = "ABC";
			public const string OtherEquitySymbol = "DEF";
			public static readonly PriceQuote Price = new PriceQuote(EquitySymbol, "", 0.0m, 0.0m, "", "");
			public static readonly PriceQuote OtherPrice = new PriceQuote(OtherEquitySymbol, "", 0.0m, 0.0m, "", "");
		}
		
		private static object[] TestCaseEquitySymbols =
		{
			new string[] { },
			new string[] { Any.EquitySymbol },
			new string[] { Any.EquitySymbol, Any.OtherEquitySymbol},
		};
	}
}
