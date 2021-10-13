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
