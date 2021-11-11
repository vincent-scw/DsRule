// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DsRule.ExpressionBuilder.BinaryFormatters
{
    public class ChangeTypeFormatter : IBinaryFormatter
    {
        private static readonly MethodInfo _changeType =
            typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });

        private static readonly MethodInfo _enumParse =
            typeof(Enum).GetMethod("Parse", new Type[] {typeof(Type), typeof(string), typeof(bool)});

        private static readonly Operators[] _excludeOperators = new[] {Operators.In};

        public bool CanFormat(Operators op, Expression lExpr, Expression rExpr)
        {
            if (_excludeOperators.Contains(op))
            {
                return false;
            }

            // When type does not match
            return lExpr.Type != rExpr.Type;
        }

        public void Format(Operators op, ref Expression lExpr, ref Expression rExpr)
        {
            var underlyingType = Nullable.GetUnderlyingType(lExpr.Type) ?? lExpr.Type;
            
            MethodCallExpression mce;
            if (underlyingType.IsEnum)
            {
                mce = Expression.Call(_enumParse,
                    Expression.Constant(underlyingType),
                    rExpr,
                    Expression.Constant(true));
            }
            else
            {
                mce = Expression.Call(_changeType,
                    Expression.Convert(rExpr, typeof(object)),
                    Expression.Constant(underlyingType));
            }

            rExpr = Expression.Convert(mce, lExpr.Type);
        }
    }
}
