// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using DsRule.Tokenizer;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    class ArrayExpr : DslExpression
    {
        public ExpressionToken LeftToken { get; }
        public ExpressionToken RightToken { get; }
        public DslExpression[] Expressions { get; }

        public ArrayExpr(DslExpression[] expressions, ExpressionToken lToken, ExpressionToken rToken)
        {
            LeftToken = lToken;
            RightToken = rToken;
            Expressions = expressions;
        }

        public override Expression BuildLinqExpression()
        {
            var linqExprs = Expressions.Select(x => x.BuildLinqExpression()).ToList();
            var exprType = linqExprs[0].Type;
            if (linqExprs.Any(e => e.Type != exprType))
            {
                throw new ArgumentException("Expression Type in array does not match.");
            }

            return Expression.NewArrayInit(exprType, linqExprs);
        }
    }
}
