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

using System;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

using QIFGet.Domain;
using QIFGet.Domain.NamedConstants;
using QIFGet.Extensions;

namespace QIFGet.Tests.Extensions
{
    public class ConversionExtensionsTests
    {
        public class When_asked_to_combine_QIFRecords_into_QIFTransactions
        {
            [TestFixture]
            public class Given_QIFRecords_for_a_single_transaction
            {
                [Test]
                public void Should_return_a_QIFTransaction_containing_the_records()
                {
                    var records = new[]
                        {
                            new QIFRecord(QIFRecordType.Content, "N1701"),
                            new QIFRecord(QIFRecordType.Content, "PCHECK"),
                            new QIFRecord(QIFRecordType.Content, "$12.44"),
                            new QIFRecord(QIFRecordType.TransactionEnd, ""),
                        };
                    var transactions = records.CombineIntoTransactions().ToList();
                    transactions.Count.ShouldBeEqualTo(1);
                    var transaction = transactions.First();
                    transaction.Records.Count.ShouldBeEqualTo(4);
                    transaction.Records.ShouldContainAll(records);
                }
            }

            [TestFixture]
            public class Given_QIFRecords_for_multiple_transactions
            {
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
                    var transactions = records.CombineIntoTransactions().ToList();
                    transactions.Count.ShouldBeEqualTo(2);

                    var transaction1 = transactions.First();
                    transaction1.Records.Count.ShouldBeEqualTo(5);
                    transaction1.Records.ShouldContainAll(records.Take(5));

                    var transaction2 = transactions.Last();
                    transaction2.Records.Count.ShouldBeEqualTo(7);
                    transaction2.Records.ShouldContainAll(records.Skip(5));
                }
            }
        }

        public class When_asked_to_convert_a_string_to_a_QIFRecord
        {
            [TestFixture]
            public class Given_text_containing_only_a_QIF_end_of_transaction_code
            {
                [Test]
                public void Should_return_a_TransactionEnd_record()
                {
                    const string input = "^";
                    var record = input.ConvertToRecord();
                    record.RecordType.ShouldBeEqualTo(QIFRecordType.TransactionEnd);
                    record.Data.ShouldBeEqualTo("");
                }
            }

            [TestFixture]
            public class Given_text_starting_with_a_QIF_header_account_code
            {
                [Test]
                public void Should_return_an_AccountHeader_record_with_the_contents_of_the_text()
                {
                    const string input = "!Account";
                    var record = input.ConvertToRecord();
                    record.RecordType.ShouldBeEqualTo(QIFRecordType.AccountHeader);
                    record.Data.ShouldBeEqualTo("");
                }
            }

            [TestFixture]
            public class Given_text_starting_with_a_QIF_header_option_code
            {
                [Test]
                public void Should_return_a_OptionHeader_record_with_the_contents_of_the_text()
                {
                    const string input = "!Option:AutoSwitch";
                    var record = input.ConvertToRecord();
                    record.RecordType.ShouldBeEqualTo(QIFRecordType.OptionHeader);
                    record.Data.ShouldBeEqualTo("AutoSwitch");
                }
            }

            [TestFixture]
            public class Given_text_starting_with_a_QIF_header_type_code
            {
                [Test]
                public void Should_return_a_TypeHeader_record_with_the_contents_of_the_text()
                {
                    const string input = "!Type:Cash";
                    var record = input.ConvertToRecord();
                    record.RecordType.ShouldBeEqualTo(QIFRecordType.TypeHeader);
                    record.Data.ShouldBeEqualTo("Cash");
                }
            }

            [TestFixture]
            public class Given_text_that_does_not_contain_a_QIF_header_or_transaction_end_code
            {
                [Test]
                public void Should_return_a_Content_record_with_the_contents_of_the_text()
                {
                    const string input = "NChecking";
                    var record = input.ConvertToRecord();
                    record.RecordType.ShouldBeEqualTo(QIFRecordType.Content);
                    record.Data.ShouldBeEqualTo("NChecking");
                }
            }
        }

        [TestFixture]
        public class When_asked_to_get_a_date_from_a_string
        {
            [Test]
            public void Given__10_SLASH_14_APOS_2006__should_get_2006_10_14()
            {
                var result = "10/14'2006".GetDate();
                result.ShouldBeEqualTo(new DateTime(2006, 10, 14));
            }
        }
    }
}