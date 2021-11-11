// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using DsRule.ExpressionBuilder;
using DsRule.Tokenizer;
using Superpower;

namespace DsRule
{
    internal static class TokenListParserExtensions
    {
        public static TokenListParser<ExpressionToken, DslExpression> OrSkipNull(
            this TokenListParser<ExpressionToken, DslExpression> lhs,
            TokenListParser<ExpressionToken, DslExpression> rhs)
        {
            return rhs == null ? lhs : lhs.Or(rhs);
        }
    }
}
