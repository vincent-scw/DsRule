// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class NegateExpr : DslExpression
    {
        public DslExpression Expr { get; }
        public Operators Op { get; }

        public NegateExpr(DslExpression expr, Operators op)
        {
            Expr = expr;
            Op = op;
        }

        public override Expression BuildLinqExpression()
        {
            if (Op == Operators.Negate)
            {
                return Expression.Negate(Expr.BuildLinqExpression());
            }

            if (Op == Operators.Not)
            {
                return Expression.Not(Expr.BuildLinqExpression());
            }

            throw new NotSupportedException($"Op {Op} not supported for Negate.");
        }
    }
}
