// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using DsRule.ExpressionBuilder;
using System;
using System.Linq.Expressions;
using Xunit;

namespace DsRule.UnitTest.ExpressionBuilder
{
    public class BinaryExprTests
    {
        [Theory]
        [InlineData(Operators.Add, 1, "2", 3)]
        [InlineData(Operators.Subtract, 1, "2", -1)]
        [InlineData(Operators.Add, 1, 1.1d, 2)]
        [InlineData(Operators.Add, 1.1d, 1, 2.1d)]
        internal void NumberTest(Operators op, object pLeft, object pRight, object result)
        {
            var a = (int)Convert.ChangeType(1.1d, typeof(int));

            var bx = new BinaryExpr(op, new ConstantExpr(pLeft), new ConstantExpr(pRight));
            var res = GetResult(bx);
            Assert.Equal(result, res);
        }

        [Theory]
        [InlineData("str1", "str2", "str1str2")]
        [InlineData("str", 1, "str1")]
        internal void StringTest(object pLeft, object pRight, object result)
        {

        }

        private object GetResult(DslExpression expr)
        {
            var lambda = Expression.Lambda(expr.BuildLinqExpression());
            var complied = lambda.Compile();
            return complied.DynamicInvoke();
        }
    }
}
