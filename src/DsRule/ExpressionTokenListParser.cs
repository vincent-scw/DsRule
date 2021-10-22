using DsRule.ExpressionBuilder;
using DsRule.Tokenizer;
using Superpower;
using Superpower.Parsers;
using System;
using System.Linq;
using System.Linq.Expressions;
using DateTimeKind = DsRule.ExpressionBuilder.DateTimeKind;

namespace DsRule
{
    internal class ExpressionTokenListParser
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
        public readonly static TokenListParser<ExpressionToken, Operators> Is =
            Token.EqualTo(ExpressionToken.Is).Value(Operators.Is);

        public readonly static TokenListParser<ExpressionToken, DslExpression> Now =
            Token.EqualTo(ExpressionToken.Now).Value(DslExpression.DateTime(DateTimeKind.Now));

        public readonly static TokenListParser<ExpressionToken, DslExpression> Today =
            Token.EqualTo(ExpressionToken.Today).Value(DslExpression.DateTime(DateTimeKind.Today));

        public readonly static TokenListParser<ExpressionToken, DslExpression> True =
            Token.EqualTo(ExpressionToken.True).Value(DslExpression.Constant(true));

        public readonly static TokenListParser<ExpressionToken, DslExpression> False =
            Token.EqualTo(ExpressionToken.True).Value(DslExpression.Constant(false));

        public readonly static TokenListParser<ExpressionToken, DslExpression> Null =
            Token.EqualTo(ExpressionToken.Null).Value(DslExpression.Constant(null));

        public readonly static TokenListParser<ExpressionToken, DslExpression> Number =
            Token.EqualTo(ExpressionToken.Number)
                .Apply(ExpressionTextParsers.Real)
                .Select(n => DslExpression.Constant(Convert.ToDecimal(n.ToStringValue())));

        public readonly static TokenListParser<ExpressionToken, DslExpression> HexNumber =
            Token.EqualTo(ExpressionToken.HexNumber)
                .Apply(ExpressionTextParsers.HexInteger)
                .Select(u => DslExpression.Constant(Convert.ToDecimal(u)));

        public readonly static TokenListParser<ExpressionToken, DslExpression> ValueContent =
            Token.EqualTo(ExpressionToken.String)
                .Apply(ExpressionTextParsers.ValueContent)
                .Select(DslExpression.Constant);


        public TokenListParser<ExpressionToken, Func<DslExpression, DslExpression>> PropertyPathStep { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> IdentifierProperty { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> PropertyPath { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Literal { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Item { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Factor { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Operand { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> InnerTerm { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Term { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Comparand { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Comparison { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Conjunction { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Disjunction { get; set; }
        public TokenListParser<ExpressionToken, DslExpression> Expr { get; set; }

        private void RebuildTokenList()
        {
            Literal =
                ValueContent
                    //.Or(RegularExpression)
                    .Or(Number)
                    .Or(HexNumber)
                    .Or(True)
                    .Or(False)
                    .Or(Null)
                    .Named("literal");

            Item = Literal.Or(Now).Or(Today).OrSkipNull(PropertyPath); //.Or(Function).Or(ArrayLiteral);

            Factor =
                (from lparen in Token.EqualTo(ExpressionToken.LParen)
                    from expr in Parse.Ref(() => Expr)
                    from rparen in Token.EqualTo(ExpressionToken.RParen)
                    select expr)
                .OrSkipNull(Item);

            Operand =
                (from op in Negate.Or(Not)
                    from factor in Factor
                    select DslExpression.Negate(factor, op)).OrSkipNull(Factor).Named("operand");

            InnerTerm =
                Parse.Chain(Power, Operand, DslExpression.Binary);

            Term = Parse.Chain(Multiply.Or(Divide).Or(Modulo), InnerTerm, DslExpression.Binary);

            Comparand = Parse.Chain(Add.Or(Subtract), Term, DslExpression.Binary);

            Comparison = Parse.Chain(LessThan.Or(LessThanOrEqual).Or(GreaterThanOrEqual).Or(GreaterThan).Or(Equal).Or(NotEqual), Comparand, DslExpression.Binary);

            Conjunction = Parse.Chain(And, Comparison, DslExpression.Binary);

            Disjunction = Parse.Chain(Or, Conjunction, DslExpression.Binary);

            Expr = Disjunction;
        }

        public TokenListParser<ExpressionToken, Expression> BuildExpression(Func<DslExpression, Expression> func)
        {
            RebuildTokenList();

            return Expr.AtEnd().Select(func);
        }
    }
}
