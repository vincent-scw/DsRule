using DsRule.Tokenizer;
using System;
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

            var tokenListParser = new ExpressionTokenListParser();
            tokenListParser.BuildTokenList();

            var result = tokenListParser
                .BuildExpression(body => Expression.Lambda<Func<TOut>>(Expression.Convert(body.BuildLinqExpression(), typeof(TOut))))
                (tokenList.Value);

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
