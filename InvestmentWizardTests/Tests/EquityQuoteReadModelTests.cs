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
		private EquityQuoteReadModel model;

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
			this.model.RegisterObserver(this.mockEventHandler.Object);
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

		[Test]
		public void UpdateCallsEventHandlerCalledOnce()
		{
			this.model.AddRealTimeQuote(new string[] { Any.EquitySymbol, Any.OtherEquitySymbol });

			this.model.Update();

			this.mockEventHandler.Verify(e => e(It.IsAny<IList<PriceQuote>>()), Times.Once);
		}

		[Test, TestCaseSource("TestCaseEquitySymbols")]
		public void UpdateCallsEventHandlerWithCorrectPriceQuote(string[] equitySymbols)
		{
			this.model.AddRealTimeQuote(equitySymbols);

			this.model.Update();

			if (equitySymbols.Length == 0)
			{
				this.mockEventHandler.Verify(e => e(new List<PriceQuote>()), Times.Never);
			}
			else if (equitySymbols[0] == Any.EquitySymbol && equitySymbols.Length == 1)
			{
				this.mockEventHandler.Verify(e => e(new List<PriceQuote>() { Any.Price }), Times.Once);
			}
			else if (equitySymbols[0] == Any.EquitySymbol && equitySymbols[1] == Any.OtherEquitySymbol)
			{
				this.mockEventHandler.Verify(e => e(new List<PriceQuote>() { Any.Price, Any.OtherPrice }), Times.Once);
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
