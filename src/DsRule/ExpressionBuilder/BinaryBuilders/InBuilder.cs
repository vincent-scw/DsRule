// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DsRule.ExpressionBuilder.BinaryBuilders
{
    public class InBuilder : IBinaryBuilder
    {
        private static readonly MethodInfo _indexOf =
            typeof(Array).GetMethod("IndexOf", new[] {typeof(Array), typeof(object)});

        public bool CanBuild(Operators op, Expression lExpr, Expression rExpr)
        {
            if (op != Operators.In)
            {
                return false;
            }

            return rExpr is NewArrayExpression;
        }

        public Expression Build(Operators op, Expression lExpr, Expression rExpr)
        {
            var arrayExpr = rExpr as NewArrayExpression;

            return Expression.MakeBinary(ExpressionType.GreaterThan,
                Expression.Call(_indexOf, arrayExpr, lExpr),
                Expression.Constant(-1));
        }
    }
}
