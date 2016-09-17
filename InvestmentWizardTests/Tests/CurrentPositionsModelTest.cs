namespace InvestmentWizardTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using InvestmentWizard;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class CurrentPositionsModelTest
    {
        readonly private string AnyEquitySymbol = "ABC";
        readonly private double AnyQuanity = 99;
        readonly private double OtherQuanity = 20;
        readonly private decimal AnyCost = 4567.87m;
        readonly private decimal OtherCost = 3000.45m;
        readonly private DateTime? AnyPurchaseDate = DataConverter.Date("08/14/2016");
        readonly private decimal AnyLastPrice = 55.23m;

        private Mock<IFinancialData> financeClient;
        private Mock<ITransactionsModel> transactionModel;
        private ICurrentPositionsModel model;
        
        [TestInitialize]
        public void Setup()
        {
            this.financeClient = new Mock<IFinancialData>();
            this.transactionModel = new Mock<ITransactionsModel>();
            this.model = new CurrentPositionModel(financeClient.Object, transactionModel.Object);
        }

        private Mock<ITransaction> GetAnyTransaction()
        {
            Mock<ITransaction> transaction = new Mock<ITransaction>().SetupAllProperties();
            transaction.Object.EquitySymbol = this.AnyEquitySymbol;
            transaction.Object.Quanity = this.AnyQuanity;
            transaction.Object.Cost = this.AnyCost;
            transaction.Object.PurchasedDate = this.AnyPurchaseDate;
            return transaction;
        }

        private Mock<ITransaction> GetOtherTransaction()
        {
            Mock<ITransaction> transaction = new Mock<ITransaction>().SetupAllProperties();
            transaction.Object.EquitySymbol = this.AnyEquitySymbol;
            transaction.Object.Quanity = this.OtherQuanity;
            transaction.Object.Cost = this.OtherCost;
            transaction.Object.PurchasedDate = this.AnyPurchaseDate;
            return transaction;
        }

        private Mock<IFinancialData> SetupAnyFinancialData()
        {
            List<PriceQuote> expectedPrices = new List<PriceQuote>();
            PriceQuote expectedPrice = new PriceQuote();
            expectedPrice.Symbol = this.AnyEquitySymbol;
            expectedPrice.LastPrice = this.AnyLastPrice;
            expectedPrices.Add(expectedPrice);
            this.financeClient.Setup(f => f.GetPrices(It.IsAny<List<string>>(), out expectedPrices)).Returns(true);
            return this.financeClient;
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void Update_AddsNewTransactionToOpenList_IfOpenListDoesNotContainStockTest()
        {
            //Arrange
            List<ITransaction> transactionList = new List<ITransaction>();
            transactionList.Add(this.GetAnyTransaction().Object);
            this.transactionModel.SetupGet(m => m.Transactions).Returns(transactionList);
                ;

            //Act
            this.model.UpdateList();

            //Assert
            Assert.AreEqual("ABC", model.CurrentPositions[0].StockTicker);
            Assert.AreEqual(99, model.CurrentPositions[0].Quantity);
            Assert.AreEqual(4567.87m, model.CurrentPositions[0].Cost);
        }

        [TestMethod]
        public void Update_UpdatesOpenList_IfOpenListDoesContainStockTest()
        {
            //Arrange
            List<ITransaction> transactionList = new List<ITransaction>();
            transactionList.Add(this.GetAnyTransaction().Object);
            transactionList.Add(this.GetOtherTransaction().Object);
            this.transactionModel.SetupGet(m => m.Transactions).Returns(transactionList);

            //Act
            this.model.UpdateList();

            //Assert
            Assert.AreEqual(this.AnyEquitySymbol, model.CurrentPositions[0].StockTicker);
            Assert.AreEqual(this.AnyQuanity + this.OtherQuanity, model.CurrentPositions[0].Quantity);
            Assert.AreEqual(this.AnyCost + this.OtherCost, model.CurrentPositions[0].Cost);
        }

        [TestMethod]
        public void Update_DoesNotAddNewTransaction_IfTransactionHasSaleDateTest()
        {
            //Arrange
            List<ITransaction> transactionList = new List<ITransaction>();
            ITransaction transaction = this.GetAnyTransaction().Object;
            transaction.SaleDate = DateTime.Now;
            transactionList.Add(transaction);
            this.transactionModel.SetupGet(m => m.Transactions).Returns(transactionList);

            //Act
            this.model.UpdateList();

            //Assert
            Assert.AreEqual(0, this.model.CurrentPositions.Count);
        }


        [TestMethod]
        public void Update_OnePositionTest()
        {
            //Arrange
            this.SetupAnyFinancialData();

            List<ITransaction> transactionList = new List<ITransaction>();
            ITransaction transaction = this.GetAnyTransaction().Object;
            transactionList.Add(transaction);
            this.transactionModel.SetupGet(m => m.Transactions).Returns(transactionList);

            //Act
            this.model.UpdateList();

            // Assert
            Assert.AreEqual(55.23m, model.CurrentPositions[0].CurrentPrice);
        }
    }
}
