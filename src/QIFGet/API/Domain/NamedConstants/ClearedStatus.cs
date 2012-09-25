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

using QIFGet.MvbaCore;
using QIFGet.MvbaCore.NamedConstants;

namespace QIFGet.API.Domain.NamedConstants
{
    public class ClearedStatus : NamedConstant<ClearedStatus>
    {
        public static readonly ClearedStatus Cleared = new ClearedStatus("cleared", x => x == "*" || x == "c");
        [DefaultKey]
        public static readonly ClearedStatus NotCleared = new ClearedStatus("not cleared", x => x == "");
        public static readonly ClearedStatus Reconciled = new ClearedStatus("reconciled", x => x == "X" || x == "R");

        private ClearedStatus(string key, Func<string, bool> isMatch)
        {
            IsMatch = isMatch;
            Add(key, this);
        }

        public Func<string, bool> IsMatch { get; private set; }
    }
}