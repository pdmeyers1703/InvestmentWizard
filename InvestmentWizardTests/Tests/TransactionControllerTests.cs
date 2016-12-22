// <copyright file="TransactionsControllerTests.cs" company="Peter Meyers">
//     Copyright (c) Peter Meyers. All rights reserved.
// </copyright>

namespace InvestopediaTests.Tests
{
	using System;
	using System.Collections.Generic;
	using InvestmentWizard;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class TransactionsControllerTests
	{
		ITransactionController transactionController;
		Mock<IListObservable<ITransaction>> mockTransactionsObserver;
		Mock<IListObservable<ITransaction>> mockOpenTransactionsObserver;
		Mock<ITransactionsListWriter> mockTransactionListWriter;

		[SetUp]
		public void Setup()
		{
			this.mockTransactionsObserver = new Mock<IListObservable<ITransaction>>();
			this.mockOpenTransactionsObserver = new Mock<IListObservable<ITransaction>>();
			this.mockTransactionListWriter = new Mock<ITransactionsListWriter>();

			this.transactionController = new TransactionController(
				this.mockTransactionsObserver.Object,
				this.mockOpenTransactionsObserver.Object,
				this.mockTransactionListWriter.Object);
		}

		[Test]
		public void SellPositionsCallsTransactionsListWriterSellAsManyTimeAsThereAreTransactions()
		{
			IList<ITransaction> transactionList = new List<ITransaction>() { CreateSomeTransaction(), CreateSomeOtherTransaction() };
			this.transactionController.SellPositions(transactionList, DateTime.Now, It.IsAny<decimal>());
			this.mockTransactionListWriter.Verify(w => w.Sell(
				It.IsAny<int>(), 
				It.IsAny<DateTime>(), 
				It.IsAny<double>(),
				It.IsAny<decimal>()), Times.Exactly(transactionList.Count));
		}

		[Test]
		public void SellPositionsCallsTransactionsListWriterPassingTheCorrectArguments()
		{
			IList<ITransaction> transactionList = new List<ITransaction>() { CreateSomeTransaction(), CreateSomeOtherTransaction() };
			this.transactionController.SellPositions(transactionList, Any.SomeSaleDate, Any.SomeProceeds);

			decimal expectedProceeds = 
				Math.Round(
					Convert.ToDecimal((double)Any.SomeProceeds * (Any.SomeQuantity / (Any.SomeQuantity + Any.SomeOtherQuantity))), 2);

			this.mockTransactionListWriter.Verify(w => w.Sell(
				Any.SomeRowid,
				Any.SomeSaleDate,
				Any.SomeQuantity,
				expectedProceeds), Times.Exactly(1));

		}

		[Test]
		public void SellPositionsUpdatesModeAfterPositionsAreWritten()
		{
			IList<ITransaction> transactionList = new List<ITransaction>() { CreateSomeTransaction(), CreateSomeOtherTransaction() };
			this.transactionController.SellPositions(transactionList, DateTime.Now, It.IsAny<decimal>());

			var sequence = new MockSequence();
			this.mockTransactionListWriter.InSequence(sequence).Setup(w => w.Sell(
				It.IsAny<int>(),
				It.IsAny<DateTime>(),
				It.IsAny<double>(),
				It.IsAny<decimal>()));
			this.mockTransactionsObserver.InSequence(sequence).Setup(o => o.Update());
			this.mockOpenTransactionsObserver.InSequence(sequence).Setup(o => o.Update());
		}

		private ITransaction CreateSomeTransaction()
		{
			Transaction transaction = new Transaction();
			transaction.RowID = Any.SomeRowid;
			transaction.Quanity = Any.SomeQuantity;
			transaction.SaleDate = Any.SomeDate;
			return transaction;
		}

		private ITransaction CreateSomeOtherTransaction()
		{
			Transaction transaction = new Transaction();
			transaction.RowID = Any.SomeOtherRowid;
			transaction.Quanity = Any.SomeOtherQuantity;
			transaction.SaleDate = Any.SomeOtherDate;
			return transaction;
		}

		private static class Any
		{
			public const int SomeRowid = 42;
			public const double SomeQuantity = 34.56f;
			public static readonly DateTime SomeDate = DateTime.Now;
			public const int SomeOtherRowid = 13;
			public const double SomeOtherQuantity = 18;
			public static readonly DateTime SomeOtherDate = new DateTime(15, 6, 12);
			public const decimal SomeProceeds = 3400.5m;
			public static readonly DateTime SomeSaleDate = new DateTime(16, 11, 22);
		}
	}
}
