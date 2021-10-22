// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace DsRule
{
    public static class DsRuleExecutor
    {
        public static TOut Execute<TOut>(string ruleExpression)
        {
            var expr = LinqParser.Parse<TOut>(ruleExpression);
            var compiled = expr.Compile();
            return (TOut)compiled.DynamicInvoke();
        }

        public static TOut Execute<TIn, TOut>(TIn entity, string ruleExpression)
        {
            var expr = LinqParser.Parse<TIn, TOut>(ruleExpression);
            var compiled = expr.Compile();
            return (TOut)compiled.DynamicInvoke(entity);
        }
    }
}
