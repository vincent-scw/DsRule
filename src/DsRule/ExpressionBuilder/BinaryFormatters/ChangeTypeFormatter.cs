// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DsRule.ExpressionBuilder.BinaryFormatters
{
    public class ChangeTypeFormatter : IBinaryFormatter
    {
        private static readonly MethodInfo _changeType = 
            typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });

        public bool CanFormat(Operators op, Expression lExpr, Expression rExpr)
        {
            // When type does not match
            return lExpr.Type != rExpr.Type;
        }

        public void Format(Operators op, ref Expression lExpr, ref Expression rExpr)
        {
            rExpr = Expression.Convert(
                Expression.Call(_changeType,
                    Expression.Convert(rExpr, typeof(object)),
                    Expression.Constant(lExpr.Type)),
                lExpr.Type);
        }
    }
}
