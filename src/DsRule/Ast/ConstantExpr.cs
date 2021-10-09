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
        public ConstantExpr(object obj)
        {

        }

        public override Expression BuildLinqExpression()
        {
            throw new NotImplementedException();
        }
    }
}
