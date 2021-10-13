using System;
using System.Linq.Expressions;

namespace DsRule.Ast
{
    internal abstract class DslExpression
    {

        public abstract Expression BuildLinqExpression();
    }
}
