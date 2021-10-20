// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DsRule.Ast
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

            if (leftExpr.Type != rightExpr.Type)
            {
                var methodInfo = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });
                rightExpr = Expression.Convert(Expression.Call(methodInfo, rightExpr, Expression.Constant(leftExpr.Type)), leftExpr.Type);
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
