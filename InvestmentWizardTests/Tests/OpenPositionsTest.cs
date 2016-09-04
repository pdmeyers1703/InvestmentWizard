namespace InvestmentWizardTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using InvestmentWizard;
    using System;

    [TestClass]
    public class OpenPositionsTest
    {
        [TestMethod]
        public void StockTicker_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.StockTicker = "XYZ";

            // Assert
            Assert.AreEqual("XYZ", pos.StockTicker, "Stock ticker is not \"XYZ\"");
        }

        [TestMethod]
        public void StockTicker_IsUpperCaseTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.StockTicker = "abc";

            // Assert
            Assert.AreEqual("ABC", pos.StockTicker, "Stock ticker is not \"ABC\"");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void StockTicker_IsNullTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.StockTicker = null;
        }

        [TestMethod]
        public void Quantity_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Quantity = 123.145;

            // Assert
            Assert.AreEqual(123.145, pos.Quantity, "Quantity is not 123.145");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Quantity_NegativeValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Quantity = -100;
        }

        [TestMethod]
        public void Quantity_HasThreeDecimalPlacesTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Quantity = 123.1237;

            // Assert
            Assert.AreEqual(123.124, pos.Quantity, "Quantity is not 123.124");
        }

        [TestMethod]
        public void Cost_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Cost = 1234.56m;

            // Assert
            Assert.AreEqual(1234.56m, pos.Cost, "Cost is not 1234.56");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Cost_NegativeValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Cost = -10000m;
        }

        [TestMethod]
        public void Cost_HasTwoDecimalPlacesTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Cost = 10000.98412312m;

            // Assert
            Assert.AreEqual(10000.98m, pos.Cost, "Cost is not 10000.98");
        }

        [TestMethod]
        public void CurrentPrice_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.CurrentPrice = 95.23m;

            // Assert
            Assert.AreEqual(95.23m, pos.CurrentPrice, "Current Price is not 95.23");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CurrentPrice_NegativeValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.CurrentPrice = -95.23m;
        }

        [TestMethod]
        public void CurrentPrice_HasTwoDecimalPlacesTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.CurrentPrice = 17.8999m;

            // Assert
            Assert.AreEqual(17.90m, pos.CurrentPrice, "Current price is not 17.90");
        }

        [TestMethod]
        public void CurrentMaketValue_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.CurrentPrice = 95.23m;
            pos.Quantity = 39;

            // Assert
            Assert.AreEqual(3713.97m, pos.CurrentMarketValue, "Current market value is not 3713.97");
        }

        [TestMethod]
        public void GainLoss_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Cost = 8123.145m;
            pos.CurrentPrice = 95.23m;
            pos.Quantity = 39;

            // Assert
            Assert.AreEqual(-4409.17m, pos.GainLoss, "Gain/Loss is not -4409.17");
        }

        [TestMethod]
        public void PercentGainLoss_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.Cost = 2123.145m;
            pos.CurrentPrice = 95.23m;
            pos.Quantity = 39;

            // Assert
            Assert.AreEqual(.749d, pos.PercentGainLoss, "Percentage Gain/Loss is not 74.9");
        }

        [TestMethod]
        public void PriceChange_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.PriceChange = "+ 1.41";

            // Assert
            Assert.AreEqual("+ 1.41", pos.PriceChange);
        }

        [TestMethod]
        public void PriceChangePercent_GetValueTest()
        {
            // Arrange
            OpenPositions pos = new OpenPositions();

            // Act
            pos.PriceChangePercent = "+ 2.50%";

            // Assert
            Assert.AreEqual("+ 2.50%", pos.PriceChangePercent);
        }
    }
}
