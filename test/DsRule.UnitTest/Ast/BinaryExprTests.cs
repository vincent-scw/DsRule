// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using DsRule.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DsRule.UnitTest.Ast
{
    public class BinaryExprTests
    {
        [Fact]
        public void Test()
        {
            var bx = new BinaryExpr(Operators.Add, new ConstantExpr(1), new ConstantExpr(2));
            var expr = bx.BuildLinqExpression();
        }
    }
}
