// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    class ConstantExpr : DslExpression
    {
        public object Value { get; }

        public ConstantExpr(object obj)
        {
            Value = obj;
        }

        public override Expression BuildLinqExpression()
        {
            return Expression.Constant(Value);
        }
    }
}
