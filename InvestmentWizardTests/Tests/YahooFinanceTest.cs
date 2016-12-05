namespace InvestmentWizardTests
{
	using System;
	using System.Collections.Generic;
	using InvestmentWizard;
	using NUnit.Framework;

	[TestFixture]
    public class YahooFinanceTest
    {
        private List<string> symbols;
        private YahooFinancalDataClient client;
        private List<PriceQuote> priceQuotes;

        [SetUp]
        public void Setup()
        {
            this.symbols = new List<string>();
            this.client = new YahooFinancalDataClient();
            List<PriceQuote> p = new List<PriceQuote>();
        }

        [Test]
        public void TestGetPrices_GetOneValidStock()
        {
            // Arrange
            this.symbols.Add("GOOG");

            // Act
            bool result = this.client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("GOOG", this.priceQuotes[0].Symbol);
            Assert.AreEqual("Alphabet Inc.", this.priceQuotes[0].Name);
            Assert.IsTrue(this.priceQuotes[0].LastPrice > 0);
            Assert.IsTrue(this.priceQuotes[0].PreviousClose > 0);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangePercent);
        }

		[Test]
		public void TestGetPrices_GetMultipleValidStocks()
        {
            // Arrange
            this.symbols.Add("GOOG");
            this.symbols.Add("AAPL");
            this.symbols.Add("MSFT");

            // Act
            bool result = client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.AreEqual(true, result);

            Assert.AreEqual("GOOG", this.priceQuotes[0].Symbol);
            Assert.AreEqual("Alphabet Inc.", this.priceQuotes[0].Name);
            Assert.IsTrue(this.priceQuotes[0].LastPrice > 0);
            Assert.IsTrue(this.priceQuotes[0].PreviousClose > 0);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangePercent);

            Assert.AreEqual("AAPL", this.priceQuotes[1].Symbol);
            Assert.AreEqual("Apple Inc.", this.priceQuotes[1].Name);
            Assert.IsTrue(this.priceQuotes[1].LastPrice > 0);
            Assert.IsTrue(this.priceQuotes[1].PreviousClose > 0);
            Assert.IsNotNull(this.priceQuotes[1].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[1].PriceChangePercent);

            Assert.AreEqual("MSFT", this.priceQuotes[2].Symbol);
            Assert.AreEqual("Microsoft Corporation", this.priceQuotes[2].Name);
            Assert.IsTrue(this.priceQuotes[2].LastPrice > 0);
            Assert.IsTrue(this.priceQuotes[2].PreviousClose > 0);
            Assert.IsNotNull(this.priceQuotes[2].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[2].PriceChangePercent);
        }

		[Test]
		public void TestGetPrices_ReturnFalse_WhenSymbolIsNotFound()
        {
            // Arrange
            this.symbols.Add("XYZ");

            // Act
            bool result = client.GetPrices(this.symbols, out this.priceQuotes);

            Assert.IsFalse(result);
        }

		[Test]
		public void TestGetPrices_BadPriceReference()
        {
            // Arrange
            this.symbols.Add("GOOG");

            // Act
            
            client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.IsNotNull(this.priceQuotes);
        }

		[Test]
		public void GetPrices_EmptySymbolList()
        {
            // Arrange

            // Act
            client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.AreEqual(this.symbols.Count, this.priceQuotes.Count);
        }

		[Test]
		public void YahooFinance_GetHistoricalPrice()
        {
            // Arrange
            string price = string.Empty;
            string symbol = "AAPL";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, out price);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(price);
            Assert.IsTrue(Convert.ToDecimal(price) > 0m);
        }

		[Test]
		public void YahooFinance_GetHistoricalPrice_NonDividendStock()
        {
            // Arrange
            string price = null;
            string symbol = "AMZN";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, out price);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(price);
        }

		[Test]
		public void YahooFinance_GetHistoricalPrice_InvalidStockTicker()
        {
            // Arrange
            string price = null;
            string symbol = "XYZ";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, out price);

			// Assert
			Assert.IsFalse(result);
            Assert.IsNull(price);
        }
    }
}
