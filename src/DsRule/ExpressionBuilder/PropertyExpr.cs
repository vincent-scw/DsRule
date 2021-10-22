// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class PropertyExpr : DslExpression
    {
        public Expression Expr { get; }
        public string PropertyName { get; }

        public PropertyExpr(Expression expression, string propertyName)
        {
            Expr = expression;
            PropertyName = propertyName;
        }

        public override Expression BuildLinqExpression()
        {
            return Expression.Property(Expr, PropertyName);
        }
    }
}
