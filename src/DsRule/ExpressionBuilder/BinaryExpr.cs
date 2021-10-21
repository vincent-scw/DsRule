// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using DsRule.ExpressionBuilder.BinaryBuilders;
using DsRule.ExpressionBuilder.BinaryFormatters;
using System;
using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal class BinaryExpr : DslExpression
    {
        public DslExpression Left { get; }
        public DslExpression Right { get; }
        public Operators Op { get; }

        public BinaryExpr(Operators op, DslExpression left, DslExpression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        public override Expression BuildLinqExpression()
        {
            var leftExpr = Left.BuildLinqExpression();
            var rightExpr = Right.BuildLinqExpression();

            var handlers = BinaryHandlerCollection.GetHandlers();
            foreach (var binaryHandler in handlers)
            {
                if (binaryHandler.CanFormat(Op, leftExpr, rightExpr))
                {
                    binaryHandler.Format(Op, ref leftExpr, ref rightExpr);
                }
            }

            var builders = BinaryBuilderCollection.GetBuilders();
            foreach (var binaryBuilder in builders)
            {
                if (binaryBuilder.CanBuild(Op, leftExpr, rightExpr))
                {
                    var expr = binaryBuilder.Build(Op, leftExpr, rightExpr);
                    if (expr != null)
                        return expr;
                }
            }

            throw new NotSupportedException("Cannot build binary expression.");
        }
    }
}
