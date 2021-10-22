// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder.BinaryBuilders
{
    public class GenericBuilder : IBinaryBuilder
    {
        public bool CanBuild(Operators op, Expression lExpr, Expression rExpr)
        {
            return true;
        }

        public Expression Build(Operators op, Expression lExpr, Expression rExpr)
        {
            var exprType = ToLinqExpressionType(op);
            if (exprType == null)
            {
                throw new NotSupportedException();
            }

            return Expression.MakeBinary(exprType.Value, lExpr, rExpr);
        }

        private static ExpressionType? ToLinqExpressionType(Operators op)
        {
            switch (op)
            {
                default:
                    if (Enum.TryParse<ExpressionType>(op.ToString(), out var exprType))
                    {
                        return (ExpressionType?)exprType;
                    }

                    return null;
            }
        }
    }
}
