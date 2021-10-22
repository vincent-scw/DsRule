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

        public static bool Execute<TIn1, TIn2>(TIn1 entity1, TIn2 entity2, string ruleExpression)
        {
            return false;
        }

        public static bool Execute<TIn1, TIn2, TIn3>(TIn1 entity1, TIn2 entity2, TIn3 entity3, string ruleExpression)
        {
            return false;
        }
    }
}
