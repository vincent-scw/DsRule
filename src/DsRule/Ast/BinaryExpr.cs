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

            return Expression.MakeBinary(exprType.Value, Left.BuildLinqExpression(), Right.BuildLinqExpression());
        }

        private ExpressionType? ToLinqExpressionType(Operators op)
        {
            switch (op)
            {
                default:
                    Enum.TryParse(typeof(ExpressionType), op.ToString(), true, out var exprType);
                    return (ExpressionType?)exprType;
            }
        }
    }
}
