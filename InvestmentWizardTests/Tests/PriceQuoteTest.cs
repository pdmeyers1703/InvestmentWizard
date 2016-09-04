namespace InvestmentWizardTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using InvestmentWizard;

    [TestClass]
    public class PriceQuoteTest
    {
        [TestMethod]
        public void GetSymbolTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string symbol = "Test Symbol";
            
            // Act
            quote.Symbol = symbol;

            // Assert
            Assert.AreEqual(symbol, quote.Symbol, "Quote symbol is not equal to \"" + symbol + "\"");
        }

        [TestMethod]
        public void GetNameTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string name = "Test Name";

            // Act
            quote.Name = name;

            // Assert
            Assert.AreEqual(name, quote.Name, "Quote name is not equal to \"" + name + "\"");
        }

        [TestMethod]
        public void GetLastPriceTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            decimal lastPrice = 123.45m;

            // Act
            quote.LastPrice= lastPrice;

            // Assert
            Assert.AreEqual(lastPrice, quote.LastPrice, "Quote last price is not equal to \"" + lastPrice + "\"");
        }

        [TestMethod]
        public void GetLastPreviousCloseTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            decimal prevClose = 123.45m;

            // Act
            quote.PreviousClose = prevClose;

            // Assert
            Assert.AreEqual(prevClose, quote.PreviousClose, "Quote previous close is not equal to \"" + prevClose + "\"");
        }

        [TestMethod]
        public void GetPriceChangeAbsoluteTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string priceChanage = "+ 0.25";

            // Act
            quote.PriceChangeAbsolute = priceChanage;

            // Assert
            Assert.AreEqual(priceChanage, quote.PriceChangeAbsolute, "Price change is not equal to \"" + priceChanage + "\"");
        }

        [TestMethod]
        public void GetPriceChangePercentTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string priceChanage = "+1.20%";

            // Act
            quote.PriceChangePercent = priceChanage;

            // Assert
            Assert.AreEqual(priceChanage, quote.PriceChangePercent, "Price change percent is not equal to \"" + priceChanage + "\"");
        }
    }
}
