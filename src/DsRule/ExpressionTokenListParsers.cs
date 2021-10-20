using DsRule.ExpressionBuilder;
using DsRule.Tokenizer;
using Superpower;
using Superpower.Parsers;
using System;
using System.Linq;

namespace DsRule
{
    internal class ExpressionTokenListParsers
    {
        public readonly static TokenListParser<ExpressionToken, Operators> Add =
            Token.EqualTo(ExpressionToken.Plus).Value(Operators.Add);
        public readonly static TokenListParser<ExpressionToken, Operators> Subtract =
            Token.EqualTo(ExpressionToken.Minus).Value(Operators.Subtract);
        public readonly static TokenListParser<ExpressionToken, Operators> Multiply =
            Token.EqualTo(ExpressionToken.Asterisk).Value(Operators.Multiply);
        public readonly static TokenListParser<ExpressionToken, Operators> Divide =
            Token.EqualTo(ExpressionToken.ForwardSlash).Value(Operators.Divide);
        public readonly static TokenListParser<ExpressionToken, Operators> Modulo =
            Token.EqualTo(ExpressionToken.Percent).Value(Operators.Modulo);
        public readonly static TokenListParser<ExpressionToken, Operators> Power =
            Token.EqualTo(ExpressionToken.Caret) .Value(Operators.Power);
        public readonly static TokenListParser<ExpressionToken, Operators> And =
            Token.EqualTo(ExpressionToken.And).Value(Operators.And);
        public readonly static TokenListParser<ExpressionToken, Operators> Or =
            Token.EqualTo(ExpressionToken.Or) .Value(Operators.Or);
        public readonly static TokenListParser<ExpressionToken, Operators> LessThanOrEqual =
            Token.EqualTo(ExpressionToken.LessThanOrEqual) .Value(Operators.LessThanOrEqual);
        public readonly static TokenListParser<ExpressionToken, Operators> LessThan =
            Token.EqualTo(ExpressionToken.LessThan).Value(Operators.LessThan);
        public readonly static TokenListParser<ExpressionToken, Operators> GreaterThan =
            Token.EqualTo(ExpressionToken.GreaterThan).Value(Operators.GreaterThan);
        public readonly static TokenListParser<ExpressionToken, Operators> GreaterThanOrEqual =
            Token.EqualTo(ExpressionToken.GreaterThanOrEqual).Value(Operators.GreaterThanOrEqual);
        public readonly static TokenListParser<ExpressionToken, Operators> Equal =
            Token.EqualTo(ExpressionToken.Equal).Value(Operators.Equal);
        public readonly static TokenListParser<ExpressionToken, Operators> NotEqual =
            Token.EqualTo(ExpressionToken.NotEqual).Value(Operators.NotEqual);
        public readonly static TokenListParser<ExpressionToken, Operators> Negate =
            Token.EqualTo(ExpressionToken.Minus).Value(Operators.Negate);
        public readonly static TokenListParser<ExpressionToken, Operators> Not =
            Token.EqualTo(ExpressionToken.Not).Value(Operators.Not);
        //public readonly static TokenListParser<ExpressionToken, Operators> Is = 
        //    Token.EqualTo(ExpressionToken.Is).Value(Operators.Is);

        public readonly static TokenListParser<ExpressionToken, DslExpression> Number =
            Token.EqualTo(ExpressionToken.Number)
                .Apply(ExpressionTextParsers.Real)
                .Select(n => (DslExpression)new ConstantExpr(n));

        public readonly static TokenListParser<ExpressionToken, DslExpression> HexNumber =
            Token.EqualTo(ExpressionToken.HexNumber)
                .Apply(ExpressionTextParsers.HexInteger)
                .Select(u => (DslExpression)new ConstantExpr(Convert.ToDecimal(u)));

        public readonly static TokenListParser<ExpressionToken, DslExpression> ValueContent =
            Token.EqualTo(ExpressionToken.String)
                .Apply(ExpressionTextParsers.ValueContent)
                .Select(s => (DslExpression)new ConstantExpr(s));
    }
}
