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
using System.Collections.Generic;
using System.Linq;

namespace QIFGet.Extensions
{
    internal static class IEnumerableTExtensions
    {
        public static IEnumerable<IList<T>> Group<T>(this IEnumerable<T> items, Func<T, bool> endGroup)
        {
            var group = new List<T>();
            foreach (var item in items)
            {
                if (endGroup(item))
                {
                    if (group.Any())
                    {
                        yield return group;
                        group = new List<T>();
                    }
                }
                else
                {
                    group.Add(item);
                }
            }
            if (group.Any())
            {
                yield return group;
            }
        }
    }
}