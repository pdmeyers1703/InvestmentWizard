namespace InvestmentWizardTests
{
	using System;
	using InvestmentWizard;
	using NUnit.Framework;

	[TestFixture]
    public class TransactionTests
    {
        [Test]
        public void GetRowIdTest()
        {
            // Arrange
            ITransaction t = new Transaction();

            // Act
            t.RowID = 17;

            // Assert
            Assert.AreEqual(17, t.RowID, "The row id is not " + t.RowID.ToString());
        }

		[Test]
		public void GetPurchasedDateTest()
        {
            // Arrange
            ITransaction t = new Transaction(); 
            DateTime date = new DateTime(2014, 06, 05);

            // Act
            t.PurchasedDate = date;

            // Assert
            Assert.AreEqual(date, t.PurchasedDate, "Purchased Date is not " + date.ToShortDateString());
        }

		[TestCase]
		public void GetPurchasedDateNullabeTest()
        {
            // Arrange
            ITransaction t = new Transaction();
            DateTime? date = null;

            // Act
            t.PurchasedDate = date;

            // Assert
            Assert.IsNull(t.PurchasedDate);
        }

		[TestCase]
		public void GetEquitySymbolTest()
        {
            // Arrange
            ITransaction t = new Transaction();
            string symbol = "XYZ";

            // Act
            t.EquitySymbol = symbol;

            // Assert
            Assert.AreSame(symbol, t.EquitySymbol, "Equity Symbol is not " + symbol);
        }

		[TestCase]
		public void GetEquitySymbolNullableTest()
        {
            // Arrange
            ITransaction t = new Transaction();
            string symbol = null;

            // Act
            t.EquitySymbol = symbol;

            // Assert
            Assert.IsNull(t.EquitySymbol);
        }

		[TestCase]
		public void GetQuantityTest()
        {
            // Arrange
            ITransaction t = new Transaction();
            uint quanity = 100;

            // Act
            t.Quanity = quanity;

            // Assert
            Assert.AreEqual(quanity, t.Quanity, "Quantity is not " + quanity.ToString());
        }

		[TestCase]
		public void GetPurchasePrice()
        {
            // Arrange
            ITransaction t = new Transaction();
            uint quanity = 100;
            decimal cost = 4444.59m;

            // Act
            t.Quanity = quanity;
            t.Cost = cost;

            // Assert
            Assert.AreEqual(44.45m, t.PurchasePrice, "Purchase price is not 44.45");
        }

		[TestCase]
		public void GetPurchasePrice_DividebyZero()
		{
			// Arrange
			ITransaction t = new Transaction();
			double quanity = 0;
			decimal cost = 4444.59m;

			// Act
			t.Quanity = quanity;
			t.Cost = cost;

			// Assert
			Assert.AreEqual(0.00m, t.PurchasePrice);
        }

		[TestCase]
		public void GetCost()
        {
            // Arrange
            ITransaction t = new Transaction();
            decimal cost = 4444.59m;

            // Act
            t.Cost = cost;

            // Assert
            Assert.AreEqual(4444.59m, t.Cost, "Cost is not 4444.59");
        }

		[TestCase]
		public void GetSaleDateTest()
        {
			// Arrange
			ITransaction t = new Transaction();
            DateTime date = new DateTime(2014, 06, 05);

            // Act
            t.SaleDate = date;

            // Assert
            Assert.AreEqual(date, t.SaleDate, "Sale Date is not " + date.ToShortDateString());
        }

		[TestCase]
		public void GetSaleDateNullabeTest()
        {
			// Arrange
			ITransaction t = new Transaction();
            DateTime? date = null;

            // Act
            t.SaleDate = date;

            // Assert
            Assert.IsNull(t.SaleDate);
        }

		[TestCase]
		public void GetSalePrice()
        {
			// Arrange
			ITransaction t = new Transaction();
            uint quanity = 59;
            decimal proceeds = 7893.67m;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;

            // Assert
            Assert.AreEqual(133.79m, t.SalePrice, "Sale price is not 133.79");
        }

		[TestCase]
		public void GetSalePrice_DividebyZero()
        {
			// Arrange
			ITransaction t = new Transaction();
            double quanity = 0;
            decimal proceeds = 7893.67m;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;
            
            // Assert
            Assert.IsNull(t.SalePrice);
        }

		[TestCase]
		public void GetSalePrice_SalesProceedsNull()
        {
			// Arrange
			ITransaction t = new Transaction();
            uint quanity = 59;
            decimal? proceeds = null;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;

            // Assert
            Assert.IsNull(t.SalePrice);
        }

		[TestCase]
		public void GetSaleProceeds()
        {
			// Arrange
			ITransaction t = new Transaction();
            decimal proceeds = 1234.56m;

            // Act
            t.SaleProceeds = proceeds;

            // Assert
            Assert.AreEqual(proceeds, t.SaleProceeds, "Sale proceeds is not " + proceeds);
        }

		[TestCase]
		public void GetSaleProceeds_NullValue()
        {
			// Arrange
			ITransaction t = new Transaction();
            decimal? proceeds = null;

            // Act
            t.SaleProceeds = proceeds;

            // Assert
            Assert.IsNull(t.SaleProceeds);
        }

		[TestCase]
		public void GetDividends()
        {
			// Arrange
			ITransaction t = new Transaction();
            decimal div = 222.56m;

            // Act
            t.Dividends = div;

            // Assert
            Assert.AreEqual(div, t.Dividends, "Dividends is not " + div);
        }
    }
}
