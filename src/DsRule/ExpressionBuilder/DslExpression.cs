using System.Linq.Expressions;

namespace DsRule.ExpressionBuilder
{
    internal abstract class DslExpression
    {

        public abstract Expression BuildLinqExpression();

        public static DslExpression Constant(object obj)
        {
            return new ConstantExpr(obj);
        }

        public static DslExpression Negate(DslExpression expr)
        {
            return new NegateExpr(expr);
        }

        public static DslExpression Binary(Operators op, DslExpression lExpr, DslExpression rExpr)
        {
            return new BinaryExpr(op, lExpr, rExpr);
        }
    }
}
