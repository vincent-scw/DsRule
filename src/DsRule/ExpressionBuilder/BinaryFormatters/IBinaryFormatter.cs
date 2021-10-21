// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder.BinaryFormatters
{
    public interface IBinaryFormatter
    {
        bool CanFormat(Operators op, Expression lExpr, Expression rExpr);

        void Format(Operators op, ref Expression lExpr, ref Expression rExpr);
    }
}
