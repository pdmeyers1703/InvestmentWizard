namespace InvestmentWizardTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using InvestmentWizard;
    using System;

    [TestClass]
    public class TransactionHistoryTest
    {
        [TestMethod]
        public void GetRowIdTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();

            // Act
            t.RowID = 17;

            // Assert
            Assert.AreEqual<int>(17, t.RowID, "The row id is not " + t.RowID.ToString());
        }

        [TestMethod]
        public void GetPurchasedDateTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory(); 
            DateTime date = new DateTime(2014, 06, 05);

            // Act
            t.PurchasedDate = date;

            // Assert
            Assert.AreEqual<DateTime?>(date, t.PurchasedDate, "Purchased Date is not " + date.ToShortDateString());
        }

        [TestMethod]
        public void GetPurchasedDateNullabeTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            DateTime? date = null;

            // Act
            t.PurchasedDate = date;

            // Assert
            Assert.IsNull(t.PurchasedDate);
        }

        [TestMethod]
        public void GetEquitySymbolTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            string symbol = "XYZ";

            // Act
            t.EquitySymbol = symbol;

            // Assert
            Assert.AreSame(symbol, t.EquitySymbol, "Equity Symbol is not " + symbol);
        }

        [TestMethod]
        public void GetEquitySymbolNullableTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            string symbol = null;

            // Act
            t.EquitySymbol = symbol;

            // Assert
            Assert.IsNull(t.EquitySymbol);
        }

        [TestMethod]
        public void GetQuantityTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            uint quanity = 100;

            // Act
            t.Quanity = quanity;

            // Assert
            Assert.AreEqual<double>(quanity, t.Quanity, "Quantity is not " + quanity.ToString());
        }
        
        [TestMethod]
        public void GetPurchasePrice()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            uint quanity = 100;
            decimal cost = 4444.59m;

            // Act
            t.Quanity = quanity;
            t.Cost = cost;

            // Assert
            Assert.AreEqual<decimal>(44.45m, t.PurchasePrice, "Purchase price is not 44.45");
        }

        [TestMethod]
        public void GetPurchasePrice_DividebyZero()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            double quanity = 0;
            decimal cost = 4444.59m;

            // Act
            t.Quanity = quanity;
            t.Cost = cost;
           
            // Assert
            Assert.AreEqual<decimal>(0, t.PurchasePrice);
        }

        [TestMethod]
        public void GetCost()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            decimal cost = 4444.59m;

            // Act
            t.Cost = cost;

            // Assert
            Assert.AreEqual<decimal>(4444.59m, t.Cost, "Cost is not 4444.59");
        }

        [TestMethod]
        public void GetSaleDateTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            DateTime date = new DateTime(2014, 06, 05);

            // Act
            t.SaleDate = date;

            // Assert
            Assert.AreEqual<DateTime?>(date, t.SaleDate, "Sale Date is not " + date.ToShortDateString());
        }

        [TestMethod]
        public void GetSaleDateNullabeTest()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            DateTime? date = null;

            // Act
            t.SaleDate = date;

            // Assert
            Assert.IsNull(t.SaleDate);
        }

        [TestMethod]
        public void GetSalePrice()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            uint quanity = 59;
            decimal proceeds = 7893.67m;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;

            // Assert
            Assert.AreEqual<decimal?>(133.79m, t.SalePrice, "Sale price is not 133.79");
        }

        [TestMethod]
        public void GetSalePrice_DividebyZero()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            double quanity = 0;
            decimal proceeds = 7893.67m;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;
            
            // Assert
            Assert.IsNull(t.SalePrice);
        }

        [TestMethod]
        public void GetSalePrice_SalesProceedsNull()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            uint quanity = 59;
            decimal? proceeds = null;

            // Act
            t.Quanity = quanity;
            t.SaleProceeds = proceeds;

            // Assert
            Assert.IsNull(t.SalePrice);
        }

        [TestMethod]
        public void GetSaleProceeds()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            decimal proceeds = 1234.56m;

            // Act
            t.SaleProceeds = proceeds;

            // Assert
            Assert.AreEqual<decimal?>(proceeds, t.SaleProceeds, "Sale proceeds is not " + proceeds);
        }

        [TestMethod]
        public void GetSaleProceeds_NullValue()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            decimal? proceeds = null;

            // Act
            t.SaleProceeds = proceeds;

            // Assert
            Assert.IsNull(t.SaleProceeds);
        }

        [TestMethod]
        public void GetDividends()
        {
            // Arrange
            TransactionHistory t = new TransactionHistory();
            decimal div = 222.56m;

            // Act
            t.Dividends = div;

            // Assert
            Assert.AreEqual<decimal?>(div, t.Dividends, "Dividends is not " + div);
        }
    }
}
