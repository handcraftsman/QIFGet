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

using FluentAssert;

using NUnit.Framework;

using QIFGet.API.Domain;
using QIFGet.Domain;
using QIFGet.Domain.NamedConstants;

namespace QIFGet.Tests.Domain.NamedConstants
{
    public class QIFContentTypeTests
    {
        public class AccountName
        {
            [TestFixture]
            public class Given__PPersonal_Checking
            {
                private const string Input = "NPersonal_Checking";
                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);
                    _entry = new Entry
                        {
                            IsAccountHeader = true
                        };
                    _contentType = QIFContentType.AccountName;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.AccountName.ShouldBeEqualTo(Input.Substring(1));
                }
            }
        }

        public class Amount
        {
            [TestFixture]
            public class Given__T47_COMMA_111_COMMA_500_COMMA_000_DOT_00
            {
                private const string Input = "T47,111,500,000.00";

                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.Amount;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Amount.ShouldNotBeNull();
// ReSharper disable PossibleInvalidOperationException
                    _entry.Amount.Value.ShouldBeEqualTo(47111500000.00m);
// ReSharper restore PossibleInvalidOperationException
                }
            }

            [TestFixture]
            public class Given__T_DASH_47_DOT_00
            {
                private const string Input = "T-47.00";

                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.Amount;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Amount.ShouldNotBeNull();
// ReSharper disable PossibleInvalidOperationException
                    _entry.Amount.Value.ShouldBeEqualTo(-47.00m);
// ReSharper restore PossibleInvalidOperationException
                }
            }
        }

        public class Category
        {
            [TestFixture]
            public class Given__LAdjustment
            {
                private const string Input = "LAdjustment";

                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.Category;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Category.ShouldBeEqualTo(Input.Substring(1));
                }
            }
        }

        public class ClearedStatus
        {
            [TestFixture]
            public class Given__CX
            {
                private const string Input = "CX";
                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.ClearedStatus;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Status.ShouldBeEqualTo(QIFGet.API.Domain.NamedConstants.ClearedStatus.Reconciled);
                }
            }
        }

        public class Date
        {
            [TestFixture]
            public class Given__D6_SLASH_9_APOS_2006
            {
                private const string Input = "D6/9'2006";
                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.Date;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Date.ShouldBeEqualTo(new DateTime(2006, 6, 9));
                }
            }
        }

        public class Memo
        {
            [TestFixture]
            public class Given__MAmeritrade
            {
                private const string Input = "MAmeritrade";
                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.Memo;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Memo.ShouldBeEqualTo(Input.Substring(1));
                }
            }
        }

        public class Payee
        {
            [TestFixture]
            public class Given__PBank
            {
                private const string Input = "PBank";
                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);
                    _entry = new Entry();
                    _contentType = QIFContentType.Payee;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.Payee.ShouldBeEqualTo(Input.Substring(1));
                }
            }
        }

        public class TransferAmount
        {
            [TestFixture]
            public class Given__DOLLAR_47_DOT_00
            {
                private const string Input = "$47.00";

                private QIFContentType _contentType;
                private Entry _entry;
                private QIFRecord _record;

                [TestFixtureSetUp]
                public void Before_first_test()
                {
                    _record = new QIFRecord(QIFRecordType.Content, Input);

                    _entry = new Entry();
                    _contentType = QIFContentType.TransferAmount;
                }

                [Test]
                public void IsMatch_should_return_true()
                {
                    _contentType.IsMatch(_entry, _record).ShouldBeTrue();
                }

                [Test]
                public void Update_should_set_the_value_on_the_Entry()
                {
                    _contentType.Update(_entry, _record);
                    _entry.TransferAmount.ShouldNotBeNull();
// ReSharper disable PossibleInvalidOperationException
                    _entry.TransferAmount.Value.ShouldBeEqualTo(47.00m);
// ReSharper restore PossibleInvalidOperationException
                }
            }
        }
    }
}