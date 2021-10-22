using DsRule.ExpressionBuilder;
using DsRule.Tokenizer;
using Superpower;
using Superpower.Parsers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DsRule
{
    internal class LinqParser
    {
        public static LambdaExpression Parse<TOut>(string expr)
        {
            if (!TryParse<TOut>(expr, out var root, out var error))
            {
                throw new ArgumentException(error);
            }

            return root;
        }

        public static bool TryParse<TOut>(string expr, out LambdaExpression root, out string error)
        {
            var tokenParser = new ExpressionTokenListParser();
            
            var parser = tokenParser.BuildExpression(body =>
                Expression.Lambda<Func<TOut>>(Expression.Convert(body.BuildLinqExpression(), typeof(TOut))));

            return TryParse(expr, parser, out root, out error);
        }

        public static LambdaExpression Parse<TIn, TOut>(string expr)
        {
            if (!TryParse<TIn, TOut>(expr, out var root, out var error))
            {
                throw new ArgumentException(error);
            }

            return root;
        }

        public static bool TryParse<TIn, TOut>(string expr, out LambdaExpression root, out string error)
        {
            // Create Parameter for TIn
            var pe = DslExpression.Parameter(Expression.Parameter(typeof(TIn), "p"));

            var tokenParser = new ExpressionTokenListParser();

            tokenParser.PropertyPathStep = Token.EqualTo(ExpressionToken.Period)
                .IgnoreThen(Token.EqualTo(ExpressionToken.Identifier))
                .Then(n => Superpower.Parse.Return<ExpressionToken, Func<DslExpression, DslExpression>>(r =>
                    DslExpression.Property(r.BuildLinqExpression(), n.ToStringValue())));
            tokenParser.IdentifierProperty =
                    Token.EqualTo(ExpressionToken.BuiltInIdentifier).Select(b => 
                            DslExpression.Property(((ParameterExpr)pe).Expr, b.ToStringValue().Substring(1)))
                        .Or(Token.EqualTo(ExpressionToken.Identifier).Select(t => 
                            DslExpression.Property(((ParameterExpr)pe).Expr, t.ToStringValue())));
            tokenParser.PropertyPath =
                (from notfunction in Superpower.Parse.Not(Token.EqualTo(ExpressionToken.Identifier)
                        .IgnoreThen(Token.EqualTo(ExpressionToken.LParen)))
                    from id in tokenParser.IdentifierProperty
                    from path in tokenParser.PropertyPathStep.Many()
                    select path.Aggregate(id, (o, f) => f(o))).Named("property");

            var parser = tokenParser.BuildExpression(body =>
                Expression.Lambda<Func<TIn, TOut>>(
                    Expression.Convert(body.BuildLinqExpression(), typeof(TOut)),
                    (ParameterExpression)pe.BuildLinqExpression()));
            return TryParse(expr, parser, out root, out error);
        }

        private static bool TryParse(string expr, TokenListParser<ExpressionToken, Expression> parser, out LambdaExpression root, out string error)
        {
            if (expr == null)
            {
                throw new ArgumentNullException(nameof(expr));
            }

            var tokenList = ExpressionTokenizer.Instance.TryTokenize(expr);
            if (!tokenList.HasValue)
            {
                error = tokenList.ToString();
                root = null;
                return false;
            }

            var result = parser(tokenList.Value);

            if (!result.HasValue)
            {
                error = result.ToString();
                root = null;
                return false;
            }

            root = result.Value as LambdaExpression;
            error = null;
            return true;
        }
    }
}
