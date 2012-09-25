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

using QIFGet.API.Domain;
using QIFGet.Extensions;
using QIFGet.MvbaCore.NamedConstants;

namespace QIFGet.Domain.NamedConstants
{
    public class QIFContentType : NamedConstant<QIFContentType>
    {
        public static readonly QIFContentType AccountName = new QIFContentType("account name", (e, x) => e.IsAccountHeader && x.Data.StartsWith("N"), (e, x) => e.AccountName = x.Data.Substring(1));
        public static readonly QIFContentType AccountType = new QIFContentType("account type", (e, x) => e.IsAccountHeader && x.Data.StartsWith("T"), (e, x) => e.AccountType = x.Data.Substring(1));
        public static readonly QIFContentType Amount = new QIFContentType("amount", (e, x) => !e.IsAccountHeader && x.Data.StartsWith("T"), (e, x) => e.Amount = decimal.Parse(x.Data.Substring(1)));
        public static readonly QIFContentType Category = new QIFContentType("category", (e, x) => x.Data.StartsWith("L"), (e, x) => e.Category = x.Data.Substring(1));
        public static readonly QIFContentType CheckNumber = new QIFContentType("check number", (e, x) => !e.IsAccountHeader && x.Data.StartsWith("N"), (e, x) => e.CheckNumber = x.Data.Substring(1));
        public static readonly QIFContentType ClearedStatus = new QIFContentType("cleared status", (e, x) => x.Data.StartsWith("C"), (e, x) => e.Status = API.Domain.NamedConstants.ClearedStatus.GetAll().First(y => y.IsMatch(x.Data.Substring(1))));
        public static readonly QIFContentType Date = new QIFContentType("date", (e, x) => x.Data.StartsWith("D"), (e, x) => e.Date = x.Data.Substring(1).GetDate());
        public static readonly QIFContentType Memo = new QIFContentType("memo", (e, x) => x.Data.StartsWith("M"), (e, x) => e.Memo = x.Data.Substring(1));
        public static readonly QIFContentType Payee = new QIFContentType("payee", (e, x) => x.Data.StartsWith("P"), (e, x) => e.Payee = x.Data.Substring(1));
        public static readonly QIFContentType TransferAmount = new QIFContentType("transfer amount", (e, x) => x.Data.StartsWith("$"), (e, x) => e.TransferAmount = decimal.Parse(x.Data.Substring(1)));

        public QIFContentType(string key, Func<Entry, QIFRecord, bool> isMatch, Action<Entry, QIFRecord> update)
        {
            IsMatch = isMatch;
            Update = update;
            Add(key, this);
        }

        public Func<Entry, QIFRecord, bool> IsMatch { get; private set; }
        public Action<Entry, QIFRecord> Update { get; private set; }
    }
}