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

using QIFGet.MvbaCore.NamedConstants;

namespace QIFGet.API.Domain.NamedConstants
{
    public class EntryType : NamedConstant<EntryType>
    {
        public static readonly EntryType Credit = new EntryType("credit", x => x.DollarAmount != null && x.DollarAmount >= 0m);
        public static readonly EntryType Debit = new EntryType("debit", x => x.DollarAmount != null && x.DollarAmount < 0m);

        private EntryType(string key, Func<Entry, bool> isMatch)
        {
            IsMatch = isMatch;
            Add(key, this);
        }

        public Func<Entry, bool> IsMatch { get; private set; }
    }
}