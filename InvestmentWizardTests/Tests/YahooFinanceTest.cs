using InvestmentWizard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace InvestmentWizardTests
{
    [TestClass]
    public class YahooFinanceTest
    {
        private List<string> symbols;
        private YahooFinancalDataClient client;
        private List<PriceQuote> priceQuotes;

        [TestInitialize]
        public void Setup()
        {
            this.symbols = new List<string>();
            this.client = new YahooFinancalDataClient();
            List<PriceQuote> p = new List<PriceQuote>();
        }

        [TestMethod]
        public void TestGetPrices_GetOneValidStock()
        {
            // Arrange
            this.symbols.Add("GOOG");

            // Act
            bool result = this.client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.AreEqual(true, result, "Method return code is not successful");
            Assert.AreEqual("GOOG", this.priceQuotes[0].Symbol, "Ticker symbol is not correct");
            Assert.AreEqual("Alphabet Inc.", this.priceQuotes[0].Name, "Company name is not correct");
            Assert.IsTrue(this.priceQuotes[0].LastPrice > 0, "Last Price is 0");
            Assert.IsTrue(this.priceQuotes[0].PreviousClose > 0, "Previous close is 0");
            Assert.IsNotNull(this.priceQuotes[0].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangePercent);
        }

        [TestMethod]
        public void TestGetPrices_GetMultipleValidStocks()
        {
            // Arrange
            this.symbols.Add("GOOG");
            this.symbols.Add("AAPL");
            this.symbols.Add("MSFT");

            // Act
            bool result = client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.AreEqual(true, result, "Method return code is not successful");

            Assert.AreEqual("GOOG", this.priceQuotes[0].Symbol, "Ticker symbol is not correct");
            Assert.AreEqual("Alphabet Inc.", this.priceQuotes[0].Name, "Company name is not correct");
            Assert.IsTrue(this.priceQuotes[0].LastPrice > 0, "Last Price is 0");
            Assert.IsTrue(this.priceQuotes[0].PreviousClose > 0, "Previous close is 0");
            Assert.IsNotNull(this.priceQuotes[0].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[0].PriceChangePercent);

            Assert.AreEqual("AAPL", this.priceQuotes[1].Symbol, "Ticker symbol is not correct");
            Assert.AreEqual("Apple Inc.", this.priceQuotes[1].Name, "Company name is not correct");
            Assert.IsTrue(this.priceQuotes[1].LastPrice > 0, "Last Price is 0");
            Assert.IsTrue(this.priceQuotes[1].PreviousClose > 0, "Previous close is 0");
            Assert.IsNotNull(this.priceQuotes[1].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[1].PriceChangePercent);

            Assert.AreEqual("MSFT", this.priceQuotes[2].Symbol, "Ticker symbol is not correct");
            Assert.AreEqual("Microsoft Corporation", this.priceQuotes[2].Name, "Company name is not correct");
            Assert.IsTrue(this.priceQuotes[2].LastPrice > 0, "Last Price is 0");
            Assert.IsTrue(this.priceQuotes[2].PreviousClose > 0, "Previous close is 0");
            Assert.IsNotNull(this.priceQuotes[2].PriceChangeAbsolute);
            Assert.IsNotNull(this.priceQuotes[2].PriceChangePercent);
        }

        [TestMethod]
        public void TestGetPrices_ReturnFalse_WhenSymbolIsNotFound()
        {
            // Arrange
            this.symbols.Add("XYZ");

            // Act
            bool result = client.GetPrices(this.symbols, out this.priceQuotes);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetPrices_BadPriceReference()
        {
            // Arrange
            this.symbols.Add("GOOG");

            // Act
            
            client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.IsNotNull(this.priceQuotes, "Price reference is null");
        }

        [TestMethod]
        public void GetPrices_EmptySymbolList()
        {
            // Arrange

            // Act
            client.GetPrices(this.symbols, out this.priceQuotes);

            // Assert
            Assert.AreEqual(this.symbols.Count, this.priceQuotes.Count);
        }

        [TestMethod]
        public void YahooFinance_GetHistoricalPrice()
        {
            // Arrange
            string price = string.Empty;
            string symbol = "AAPL";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, ref price);

            // Assert
            Assert.AreEqual(true, result, "Method return code is not successful");
            Assert.IsNotNull(price);
            Assert.IsTrue(Convert.ToDecimal(price) > 0m, "Price is not valid");
        }

        [TestMethod]
        public void YahooFinance_GetHistoricalPrice_NonDividendStock()
        {
            // Arrange
            string price = null;
            string symbol = "AMZN";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, ref price);

            // Assert
            Assert.AreEqual(true, result, "Method return code is not successful");
            Assert.IsNotNull(price);
        }

        [TestMethod]
        public void YahooFinance_GetHistoricalPrice_InvalidStockTicker()
        {
            // Arrange
            string price = null;
            string symbol = "XYZ";
            DateTime date = new DateTime(2014, 10, 1);

            // Act
            bool result = client.GetHistoricalPrice(symbol, date, ref price);

            // Assert
            Assert.AreEqual(false, result, "Method return code is not successful");
            Assert.IsNull(price);
        }
    }
}
