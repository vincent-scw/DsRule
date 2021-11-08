// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;

namespace DsRule
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> list, string expression)
        {
            var expr = LinqParser.Parse<T, bool>(expression);
            var d = expr.Compile();
            return list.Where(x => (bool) d.DynamicInvoke(x));
        }
    }
}
