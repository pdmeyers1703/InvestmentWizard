using InvestmentWizard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace InvestmentWizardTests
{
    [TestClass]
    public class AccessDatabaseTest
    {
        // [TestMethod]
        public void GetTableData()
        {
            //Arrange
            string tableName = "Transactions";
            IDatabase db = new AccessDB();
            DataTable dt = new DataTable();

            //Act
            dt = db.GetTableData(tableName);

            //Assert
        }
    }
}
