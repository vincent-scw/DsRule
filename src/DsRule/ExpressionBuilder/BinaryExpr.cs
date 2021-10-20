// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    class BinaryExpr : DslExpression
    {
        public DslExpression Left { get; }
        public DslExpression Right { get; }
        public Operators Op { get; }

        public BinaryExpr(Operators op, DslExpression left, DslExpression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        public override Expression BuildLinqExpression()
        {
            var exprType = ToLinqExpressionType(Op);
            if (exprType == null)
            {
                throw new NotSupportedException();
            }

            var leftExpr = Left.BuildLinqExpression();
            var rightExpr = Right.BuildLinqExpression();

            // If Type does not match, try convert type
            if (leftExpr.Type != rightExpr.Type)
            {
                rightExpr = ExpressionUtils.ChangeType(leftExpr, rightExpr);
            }

            return Expression.MakeBinary(exprType.Value, leftExpr, rightExpr);
        }

        private static ExpressionType? ToLinqExpressionType(Operators op)
        {
            switch (op)
            {
                default:
                    if (Enum.TryParse(typeof(ExpressionType), op.ToString(), true, out var exprType))
                    {
                        return (ExpressionType?)exprType;
                    }

                    return null;
            }
        }
    }
}
