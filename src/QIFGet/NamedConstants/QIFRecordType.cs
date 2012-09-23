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

using QIFGet.MvbaCore.NamedConstants;

namespace QIFGet.NamedConstants
{
    public class QIFRecordType : NamedConstant<QIFRecordType>
    {
        public static readonly QIFRecordType AccountHeader = new QIFRecordType("account header", x => x.StartsWith("!Account"), x => "");
        public static readonly QIFRecordType Content = new QIFRecordType("content", x => !GetAll().Where(y => y.Key != "content").Any(y => y.IsMatch(x)), x => x);
        public static readonly QIFRecordType OptionHeader = new QIFRecordType("option header", x => x.StartsWith("!Option:"), x => x.Substring("!Option:".Length));
        public static readonly QIFRecordType TransactionEnd = new QIFRecordType("transaction end", x => x == "^", x => "");
        public static readonly QIFRecordType TypeHeader = new QIFRecordType("type header", x => x.StartsWith("!Type:"), x => x.Substring("!Type:".Length));

        private QIFRecordType(string key, Func<string, bool> isMatch, Func<string, string> getData)
        {
            IsMatch = isMatch;
            GetData = getData;
            Add(key, this);
        }

        public Func<string, string> GetData { get; private set; }
        public Func<string, bool> IsMatch { get; private set; }
    }
}