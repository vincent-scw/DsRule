// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DsRule.ExpressionBuilder
{
    internal enum DateTimeKind
    {
        Now,
        Today
    }

    internal class DateTimeExpr : DslExpression
    {
        private static readonly PropertyInfo _now =
            typeof(DateTime).GetProperty("Now");

        private static readonly PropertyInfo _today =
            typeof(DateTime).GetProperty("Today");

        public DateTimeKind Kind { get; }

        public DateTimeExpr(DateTimeKind dtk)
        {
            Kind = dtk;
        }

        public override Expression BuildLinqExpression()
        {
            
            switch (Kind)
            {
                case DateTimeKind.Now:
                    return Expression.Property(null, _now);
                case DateTimeKind.Today:
                    return Expression.Property(null, _today);
                default: throw new NotSupportedException();
            }
        }
    }
}
