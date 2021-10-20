using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal abstract class DslExpression
    {

        public abstract Expression BuildLinqExpression();
    }
}
