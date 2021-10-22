// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class NegateExpr : DslExpression
    {
        public DslExpression Expr { get; }

        public NegateExpr(DslExpression expr)
        {
            Expr = expr;
        }

        public override Expression BuildLinqExpression()
        {
            return Expression.Negate(Expr.BuildLinqExpression());
        }
    }
}
