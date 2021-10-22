// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class ParameterExpr : DslExpression
    {
        public Type Type { get; }
        public string Name { get; }

        public ParameterExpr(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public override Expression BuildLinqExpression()
        {
            return Expression.Parameter(Type, Name);
        }
    }
}
