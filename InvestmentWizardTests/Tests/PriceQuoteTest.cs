namespace InvestmentWizardTests
{
    using InvestmentWizard;
	using NUnit.Framework;

	[TestFixture]
    public class PriceQuoteTest
    {
        [Test]
        public void GetSymbolTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string symbol = "Test Symbol";
            
            // Act
            quote.Symbol = symbol;

            // Assert
            Assert.AreEqual(symbol, quote.Symbol);
        }

		[Test]
		public void GetNameTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string name = "Test Name";

            // Act
            quote.Name = name;

            // Assert
            Assert.AreEqual(name, quote.Name);
        }

		[Test]
		public void GetLastPriceTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            decimal lastPrice = 123.45m;

            // Act
            quote.LastPrice= lastPrice;

            // Assert
            Assert.AreEqual(lastPrice, quote.LastPrice);
        }

		[Test]
		public void GetLastPreviousCloseTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            decimal prevClose = 123.45m;

            // Act
            quote.PreviousClose = prevClose;

            // Assert
            Assert.AreEqual(prevClose, quote.PreviousClose);
        }

		[Test]
		public void GetPriceChangeAbsoluteTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string priceChanage = "+ 0.25";

            // Act
            quote.PriceChangeAbsolute = priceChanage;

            // Assert
            Assert.AreEqual(priceChanage, quote.PriceChangeAbsolute);
        }

		[Test]
		public void GetPriceChangePercentTest()
        {
            // Arrange
            PriceQuote quote = new PriceQuote();
            string priceChanage = "+1.20%";

            // Act
            quote.PriceChangePercent = priceChanage;

            // Assert
            Assert.AreEqual(priceChanage, quote.PriceChangePercent);
        }
    }
}
