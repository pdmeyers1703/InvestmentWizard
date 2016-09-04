﻿namespace InvestmentWizardTests
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
        readonly private decimal AnyCost = 4567.87m;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_ThrowsArgumentNullException_WhenNullTest()
        {
            //Act
            this.model.Update(null);
        }

        [TestMethod]
        public void Update_AddsNewTransactionToOpenLust_IfOpenListDoesNotContainStockTest()
        {
            //Arrange
            List<ITransaction> transactionList = new List<ITransaction>();
            transactionList.Add(this.GetAnyTransaction().Object);

            //Act
            this.model.Update(transactionList);

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

            Mock<IOpenPositions> openPositions = new Mock<IOpenPositions>().SetupAllProperties();
            openPositions.Object.StockTicker = this.AnyEquitySymbol;
            openPositions.Object.Quantity = 20;
            openPositions.Object.Cost = 3001.45m;
            this.model.CurrentPositions.Add(openPositions.Object);

            //Act
            this.model.Update(transactionList);

            //Assert
            Assert.AreEqual(this.AnyEquitySymbol, model.CurrentPositions[0].StockTicker);
            Assert.AreEqual(this.AnyQuanity + 20, model.CurrentPositions[0].Quantity);
            Assert.AreEqual(this.AnyCost + 3001.45m, model.CurrentPositions[0].Cost);
        }

        [TestMethod]
        public void Update_DoesNotAddNewTransaction_IfTransactionHasSaleDateTest()
        {
            //Arrange
            this.transactionModel.SetupAllProperties();
            this.transactionModel.Object.Transactions = new List<ITransaction>();
            ITransaction transaction = this.GetAnyTransaction().Object;
            transaction.SaleDate = DateTime.Now;
            this.transactionModel.Object.Transactions.Add(transaction);

            //Act
            this.model.Update(this.transactionModel.Object.Transactions);

            //Assert
            Assert.AreEqual(0, this.model.CurrentPositions.Count);
        }


        [TestMethod]
        public void Update_OnePositionTest()
        {
            //Arrange
            this.SetupAnyFinancialData();

            this.transactionModel.SetupAllProperties();
            this.transactionModel.Object.Transactions = new List<ITransaction>();
            this.transactionModel.Object.Transactions.Add(this.GetAnyTransaction().Object);

            //Act
            this.model.Update(this.transactionModel.Object.Transactions);

            // Assert
            Assert.AreEqual(55.23m, model.CurrentPositions[0].CurrentPrice);
        }
    }
}