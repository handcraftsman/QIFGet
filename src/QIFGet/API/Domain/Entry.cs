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

using QIFGet.API.Domain.NamedConstants;

namespace QIFGet.API.Domain
{
    public class Entry
    {
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public decimal? Amount { get; set; }
        public string Category { get; set; }
        public string CheckNumber { get; set; }
        public DateTime? Date { get; set; }
        public EntryType EntryType { get; set; }
        public bool IsAccountHeader { get; set; }
        public bool IsHeader { get; set; }
        public string Memo { get; set; }
        public string Payee { get; set; }
        public ClearedStatus Status { get; set; }
        public decimal? TransferAmount { get; set; }
    }
}