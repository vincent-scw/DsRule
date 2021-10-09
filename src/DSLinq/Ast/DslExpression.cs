using System.Linq.Expressions;

namespace DsLinq.Ast
{
    internal abstract class DslExpression
    {
        public abstract Expression BuildLinqExpression();
    }
}
