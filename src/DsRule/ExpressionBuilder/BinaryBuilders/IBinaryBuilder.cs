// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DsRule.ExpressionBuilder.BinaryBuilders
{
    public interface IBinaryBuilder
    {
        bool CanBuild(Operators op, Expression lExpr, Expression rExpr);
        Expression Build(Operators op, Expression lExpr, Expression rExpr);
    }
}
