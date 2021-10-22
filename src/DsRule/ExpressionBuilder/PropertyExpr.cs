// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class PropertyExpr : DslExpression
    {
        public DslExpression Expr { get; }
        public string PropertyName { get; }

        public PropertyExpr(DslExpression expression, string propertyName)
        {
            Expr = expression;
            PropertyName = propertyName;
        }

        public override Expression BuildLinqExpression()
        {
            return Expression.Property(Expr.BuildLinqExpression(), PropertyName);
        }
    }
}
