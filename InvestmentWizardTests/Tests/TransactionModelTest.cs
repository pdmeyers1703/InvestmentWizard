namespace InvestmentWizardTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using InvestmentWizard;
    using System;
    using System.Collections.Generic;
    using System.Data;

    [TestClass]
    public class TranscationModelTests
    {
        private Mock<IDatabase> db;
        private Mock<IFinancialData> server;
        private ITransactionsModel model;

        [TestInitialize]
        public void Setup()
        {
            db = new Mock<IDatabase>();
            server = new Mock<IFinancialData>();

            this.model = new TransactionsModel(db.Object);
        }

        [TestMethod]
        public void Update_Test()
        {
            // Arrange
            DataTable dt = new DataTable();
            this.TwoDataRows(ref dt);
            this.db.Setup(x => x.GetTableData("Transactions")).Returns(dt);

            // Act
            this.model.Update();

            // Assert
            Assert.IsNotNull(model.Transactions);
            Assert.IsInstanceOfType(model.Transactions, typeof(IList<ITransaction>));
            Assert.AreEqual(2, model.Transactions.Count);
            Assert.AreEqual("1/1/2000", ((DateTime)model.Transactions[0].PurchasedDate).ToShortDateString());
            Assert.AreEqual("X", model.Transactions[0].EquitySymbol);
            Assert.AreEqual((uint)49, model.Transactions[0].Quanity);
            Assert.AreEqual(10000m, model.Transactions[0].Cost);
            Assert.AreEqual("1/1/2010", ((DateTime)model.Transactions[0].SaleDate).ToShortDateString());
            Assert.AreEqual(8888.12m, model.Transactions[0].SaleProceeds);
            Assert.AreEqual(0m, model.Transactions[0].Dividends);
            Assert.AreEqual("4/12/2014", ((DateTime)model.Transactions[1].PurchasedDate).ToShortDateString());
            Assert.AreEqual("XYZ", model.Transactions[1].EquitySymbol);
            Assert.AreEqual((uint)3000, model.Transactions[1].Quanity);
            Assert.AreEqual(18000.00m, model.Transactions[1].Cost);
            Assert.AreEqual("6/9/2014", ((DateTime)model.Transactions[1].SaleDate).ToShortDateString());
            Assert.AreEqual(23675.46m, model.Transactions[1].SaleProceeds);
            Assert.AreEqual(140.98m, model.Transactions[1].Dividends);
        }

        [TestMethod]
        public void Update_NullabeTest()
        {
            // Arrange
            DataTable dt = new DataTable();
            this.FillDataTablewithNullData(ref dt);
            this.db.Setup(x => x.GetTableData("Transactions")).Returns(dt);

            // Act
            this.model.Update();

            // Assert
            Assert.IsNotNull(this.model.Transactions);
            Assert.IsInstanceOfType(this.model.Transactions, typeof(IList<ITransaction>));
            Assert.AreEqual(1, this.model.Transactions.Count);
            Assert.IsNull(this.model.Transactions[0].PurchasedDate);
            Assert.IsNull(this.model.Transactions[0].SaleDate);
        }

        [TestMethod]
        public void Update_ExceptionThrownTest()
        {
            // Arrange
            List<TransactionHistory> list = new List<TransactionHistory>();
            var mockDB = new Mock<IDatabase>();
            var mockServer = new Mock<IFinancialData>();
            mockDB.Setup(x => x.GetTableData("Transactions")).Throws(new Exception());

            // Act
            this.model.Update();

            // Assert
            Assert.IsNotNull(this.model.Transactions);
            Assert.AreEqual(0, this.model.Transactions.Count, "List is not empty");
        }

        [TestMethod]
        public void AddPurchase_ExceptionThrownTest()
        {
            // Arrange
            this.db.Setup(x => x.AddRecord(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Throws(new Exception());
         
            // Act
            bool result = this.model.Add(new DateTime(2014, 8, 23), "GOOG", 100, 10000.0m);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddPurchase_Test()
        {
            // Arrange
            this.db.Setup(x => x.AddRecord(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Verifiable();
            
            // Act
            bool result = this.model.Add(new DateTime(2014, 8, 23), "GOOG", 100, 10000.0m);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SellPosition_ReturnsFalse_WhenUpdatingRecordFailsTest()
        {
            // Arrange
            this.db.Setup(x => x.UpdateRecord(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Throws(new Exception());

            // Act
            bool result = this.model.Sell(33, new DateTime(2014, 8, 23), 10, 10000.0m);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SellPosition_ReturnsFalse_WhenmoreThanOneRowMatchesTest()
        {
            // Arrange
            DataTable dt = new DataTable();
            this.TwoDataRows(ref dt);
            this.db.Setup(x => x.GetRows(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(dt).Verifiable();
            this.db.Setup(x => x.UpdateRecord(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Verifiable();

            // Act
            bool result = this.model.Sell(33, new DateTime(2014, 8, 23), 15, 10000.0m);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SellPosition_Test()
        {
            // Arrange
            DataTable dt = new DataTable();
            this.OneDataRow(ref dt);
            this.db.Setup(x => x.GetRows(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(dt).Verifiable();
            this.db.Setup(x => x.UpdateRecord(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Verifiable();

            // Act
            bool result = this.model.Sell(33, new DateTime(2014, 8, 23), 15, 10000.0m);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SplitPosition_ReturnFalse_WhenUpdateRecordThrowsTest()
        {
            // Arrange
            this.db.Setup(x => x.UpdateRecord(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Throws(new Exception());
            
            // Act
            bool result = this.model.Split("AAPL", 2.0);

            // Assert
            Assert.IsFalse(result);
        }

         [TestMethod]
        public void SplitPosition_Test()
        {
            // Arrange
            DataTable dt = new DataTable();
            this.TwoDataRows(ref dt);
            this.db.Setup(x => x.GetRows(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(dt);
            this.db.Setup(x => x.UpdateRecord(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>(), It.IsAny<dynamic[]>())).Verifiable();
     
            // Act
            bool result = this.model.Split("AAPL", 2.0);

            // Assert
            Assert.IsTrue(result);
        }

        readonly string tableName = "Transactions";
        readonly string[] AnyRow =  {  "1", "1/1/2000", "X", "49", "10000.00",
                                       "1/1/2010", "8888.12", "0.00" };
        readonly string[] OtherRow =  {  "2", "4/12/2014", "XYZ", "3000", "18000.00",
                                         "6/9/2014", "23675.46", "140.98" };

        private void OneDataRow(ref DataTable dt)
        {
            // Add table name
            dt.TableName = this.tableName;

            //Add the excepted columns
            this.AddCorrectTableNames(ref dt);
            dt.Rows.Add(this.AnyRow);
        }

        private void TwoDataRows(ref DataTable dt)
        {
            this.OneDataRow(ref dt);
            dt.Rows.Add(this.OtherRow);
        }

        private void FillDataTablewithNullData(ref DataTable dt)
        {
            // Add table name
            dt.TableName = "Transactions";

            //Add the excepted columns
            this.AddCorrectTableNames(ref dt);

            dt.Rows.Add(new string[] {  "1",
                                        null,
                                        "X",
                                        "49",
                                        "10,000",
                                        null,
                                        "8888.12",
                                        "0"});
        }

        private void AddCorrectTableNames(ref DataTable dt)
        {
            dt.Columns.Add("ID");
            dt.Columns.Add("Date Purchase");
            dt.Columns.Add("Equity");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Cost Basis");
            dt.Columns.Add("Sold Date");
            dt.Columns.Add("Market Value");
            dt.Columns.Add("Dividends");
        }
    }
}
