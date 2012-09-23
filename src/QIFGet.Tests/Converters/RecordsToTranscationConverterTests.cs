// * **************************************************************************
// * Copyright (c) Clinton Sheppard <sheppard@cs.unm.edu>
// *
// * This source code is subject to terms and conditions of the MIT License.
// * A copy of the license can be found in the License.txt file
// * at the root of this distribution.
// * By using this source code in any fashion, you are agreeing to be bound by
// * the terms of the MIT License.
// * You must not remove this notice from this software.
// *
// * source repository: https://github.com/handcraftsman/QIFGet
// * **************************************************************************

using System.Linq;

using FluentAssert;

using NUnit.Framework;

using QIFGet.Converters;
using QIFGet.Domain;
using QIFGet.NamedConstants;

namespace QIFGet.Tests.Converters
{
    public class RecordsToTranscationConverterTests
    {
        [TestFixture]
        public class Given_QIFRecords_for_a_single_transaction
        {
            private RecordsToTransactionConverter _recordsToTransactionConverter;

            [SetUp]
            public void Before_each_test()
            {
                _recordsToTransactionConverter = new RecordsToTransactionConverter();
            }

            [Test]
            public void Should_return_a_QIFTransaction_containing_the_records()
            {
                var records = new[]
                    {
                        new QIFRecord(QIFRecordType.AccountHeader, ""),
                        new QIFRecord(QIFRecordType.Content, "NPersonal Checking"),
                        new QIFRecord(QIFRecordType.Content, "TBank"),
                        new QIFRecord(QIFRecordType.Content, "$0.44"),
                        new QIFRecord(QIFRecordType.TransactionEnd, ""),
                    };
                var transactions = _recordsToTransactionConverter.Combine(records).ToList();
                transactions.Count.ShouldBeEqualTo(1);
                var transaction = transactions.First();
                transaction.Records.Count.ShouldBeEqualTo(4);
                transaction.Records.ShouldContainAll(records.Take(4));
            }
        }

        [TestFixture]
        public class Given_QIFRecords_for_multiple_transactions
        {
            private RecordsToTransactionConverter _recordsToTransactionConverter;

            [SetUp]
            public void Before_each_test()
            {
                _recordsToTransactionConverter = new RecordsToTransactionConverter();
            }

            [Test]
            public void Should_return_QIFTransactions_containing_the_records()
            {
                var records = new[]
                    {
                        new QIFRecord(QIFRecordType.AccountHeader, ""),
                        new QIFRecord(QIFRecordType.Content, "NPersonal Checking"),
                        new QIFRecord(QIFRecordType.Content, "TBank"),
                        new QIFRecord(QIFRecordType.Content, "$0.44"),
                        new QIFRecord(QIFRecordType.TransactionEnd, ""),
                        new QIFRecord(QIFRecordType.Content, "D09/23/2012"),
                        new QIFRecord(QIFRecordType.Content, "N1701"),
                        new QIFRecord(QIFRecordType.Content, "PCHECK"),
                        new QIFRecord(QIFRecordType.Content, "MCHECK"),
                        new QIFRecord(QIFRecordType.Content, "CC"),
                        new QIFRecord(QIFRecordType.Content, "T-40"),
                        new QIFRecord(QIFRecordType.TransactionEnd, ""),
                    };
                var transactions = _recordsToTransactionConverter.Combine(records).ToList();
                transactions.Count.ShouldBeEqualTo(2);
                var transaction1 = transactions.First();
                transaction1.Records.Count.ShouldBeEqualTo(4);
                transaction1.Records.ShouldContainAll(records.Take(4));
                var transaction2 = transactions.Last();
                transaction2.Records.Count.ShouldBeEqualTo(6);
                transaction2.Records.ShouldContainAll(records.Skip(5).Take(6));
            }
        }
    }
}