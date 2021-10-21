// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;
using System.Reflection;

namespace DsRule.ExpressionBuilder.BinaryBuilders
{
    public class StringConcatBuilder : IBinaryBuilder
    {
        private static readonly MethodInfo _concat =
            typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

        public bool CanBuild(Operators op, Expression lExpr, Expression rExpr)
        {
            if (op != Operators.Add)
                return false;

            return lExpr.Type == typeof(string);
        }

        public Expression Build(Operators op, Expression lExpr, Expression rExpr)
        {
            return Expression.Add(lExpr, rExpr, _concat);
        }
    }
}
