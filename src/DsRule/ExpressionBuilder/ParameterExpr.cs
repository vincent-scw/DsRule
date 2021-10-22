// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class ParameterExpr : DslExpression
    {
        public ParameterExpression Expr { get; }

        public ParameterExpr(ParameterExpression pe)
        {
            Expr = pe;
        }

        public override Expression BuildLinqExpression()
        {
            return Expr;
        }
    }
}
