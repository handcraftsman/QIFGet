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

using QIFGet.API;
using QIFGet.API.Domain.NamedConstants;

namespace QIFGet.Tests.API
{
    public class QIFReaderTests
    {
        [TestFixture]
        public class Given_one_transaction_with_an_account_header
        {
            [Test]
            public void Should_return_an_Account()
            {
                const string input = @"!Account
NPersonal Checking
TBank
$0.44
^
D09/24'2012
N1701
MCHECK
Cc
T-40
^
";
                var reader = new QIFReader();
                var accounts = reader.ReadFrom(input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).ToList();
                accounts.Count.ShouldBeEqualTo(1);
                var account = accounts.First();
                account.Entries.Count.ShouldBeEqualTo(2);

                var entry1 = account.Entries.First();
                entry1.IsAccountHeader.ShouldBeTrue();
                entry1.AccountName.ShouldBeEqualTo("Personal Checking");
                entry1.AccountType.ShouldBeEqualTo("Bank");
                entry1.TransferAmount.ShouldBeEqualTo(0.44m);

                var entry2 = account.Entries.Last();
                entry2.Date.ShouldBeEqualTo(new DateTime(2012, 9, 24));
                entry2.CheckNumber.ShouldBeEqualTo("1701");
                entry2.Memo.ShouldBeEqualTo("CHECK");
                entry2.Status.ShouldBeEqualTo(ClearedStatus.Cleared);
                entry2.DollarAmount.ShouldBeEqualTo(-40m);
            }
        }
    }
}