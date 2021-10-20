// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal static class ExpressionUtils
    {
        public static Expression ChangeType(Expression baseExpr, Expression changeExpr)
        {
            var methodInfo = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });
            return Expression.Convert(
                Expression.Call(methodInfo, 
                    Expression.Convert(changeExpr, typeof(object)), 
                    Expression.Constant(baseExpr.Type)), 
                baseExpr.Type);
        }
    }
}
