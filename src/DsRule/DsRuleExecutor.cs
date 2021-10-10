// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsRule
{
    public static class DsRuleExecutor
    {
        public static bool Execute<TIn>(TIn entity, string ruleExpression)
        {
            return false;
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
