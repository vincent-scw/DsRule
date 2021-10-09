using Superpower;
using Superpower.Model;
using System.Collections.Generic;

namespace DsRule.Tokenizer
{
    internal class ExpressionTokenizer : Tokenizer<ExpressionToken>
    {
        private readonly static ExpressionToken[] SimpleOps = new ExpressionToken[128];

        private readonly static HashSet<ExpressionToken> PreRegexTokens = new HashSet<ExpressionToken>
        {
            ExpressionToken.And,
            ExpressionToken.Or,
            ExpressionToken.Not,
            ExpressionToken.LParen,
            ExpressionToken.LBracket,
            ExpressionToken.Comma,
            ExpressionToken.Equal,
            ExpressionToken.NotEqual,
            ExpressionToken.Like,
            ExpressionToken.GreaterThan,
            ExpressionToken.GreaterThanOrEqual,
            ExpressionToken.LessThan,
            ExpressionToken.LessThanOrEqual,
            ExpressionToken.In,
            ExpressionToken.Is
        };

        private readonly static ExpressionKeyword[] Keywords =
        {
            new ExpressionKeyword("and", ExpressionToken.And),
            new ExpressionKeyword("in", ExpressionToken.In),
            new ExpressionKeyword("is", ExpressionToken.Is),
            new ExpressionKeyword("like", ExpressionToken.Like),
            new ExpressionKeyword("not", ExpressionToken.Not),
            new ExpressionKeyword("or", ExpressionToken.Or),
            new ExpressionKeyword("true", ExpressionToken.True),
            new ExpressionKeyword("false", ExpressionToken.False),
            new ExpressionKeyword("null", ExpressionToken.Null)
        };

        static ExpressionTokenizer()
        {
            SimpleOps['+'] = ExpressionToken.Plus;
            SimpleOps['-'] = ExpressionToken.Minus;
            SimpleOps['*'] = ExpressionToken.Asterisk;
            SimpleOps['/'] = ExpressionToken.ForwardSlash;
            SimpleOps['%'] = ExpressionToken.Percent;
            SimpleOps['^'] = ExpressionToken.Caret;
            SimpleOps['<'] = ExpressionToken.LessThan;
            SimpleOps['>'] = ExpressionToken.GreaterThan;
            SimpleOps['='] = ExpressionToken.Equal;
            SimpleOps[','] = ExpressionToken.Comma;
            SimpleOps['.'] = ExpressionToken.Period;
            SimpleOps['('] = ExpressionToken.LParen;
            SimpleOps[')'] = ExpressionToken.RParen;
            SimpleOps['['] = ExpressionToken.LBracket;
            SimpleOps[']'] = ExpressionToken.RBracket;
            SimpleOps['*'] = ExpressionToken.Asterisk;
            SimpleOps['?'] = ExpressionToken.QuestionMark;
        }

        protected override IEnumerable<Result<ExpressionToken>> Tokenize(TextSpan span, TokenizationState<ExpressionToken> state)
        {
            var next = SkipWhiteSpace(span);
            if (!next.HasValue)
                yield break;

            do
            {
                if (char.IsDigit(next.Value))
                {
                    var hex = ExpressionTextParsers.HexInteger(next.Location);
                    if (hex.HasValue)
                    {
                        next = hex.Remainder.ConsumeChar();
                        yield return Result.Value(ExpressionToken.HexNumber, hex.Location, hex.Remainder);
                    }
                    else
                    {
                        var real = ExpressionTextParsers.Real(next.Location);
                        if (!real.HasValue)
                            yield return Result.CastEmpty<TextSpan, ExpressionToken>(real);
                        else
                            yield return Result.Value(ExpressionToken.Number, real.Location, real.Remainder);

                        next = real.Remainder.ConsumeChar();
                    }

                    if (!IsDelimiter(next))
                    {
                        yield return Result.Empty<ExpressionToken>(next.Location, new[] { "digit" });
                    }
                }
                else if (next.Value == '\'')
                {
                    var str = ExpressionTextParsers.ValueContent(next.Location);
                    if (!str.HasValue)
                        yield return Result.CastEmpty<string, ExpressionToken>(str);

                    next = str.Remainder.ConsumeChar();

                    yield return Result.Value(ExpressionToken.String, str.Location, str.Remainder);
                }
                else if (next.Value == '@')
                {
                    var beginIdentifier = next.Location;
                    var startOfName = next.Remainder;
                    do
                    {
                        next = next.Remainder.ConsumeChar();
                    }
                    while (next.HasValue && char.IsLetterOrDigit(next.Value));

                    if (next.Remainder == startOfName)
                    {
                        yield return Result.Empty<ExpressionToken>(startOfName, new[] { "built-in identifier name" });
                    }
                    else
                    {
                        yield return Result.Value(ExpressionToken.BuiltInIdentifier, beginIdentifier, next.Location);
                    }
                }
                else if (char.IsLetter(next.Value) || next.Value == '_')
                {
                    var beginIdentifier = next.Location;
                    do
                    {
                        next = next.Remainder.ConsumeChar();
                    }
                    while (next.HasValue && (char.IsLetterOrDigit(next.Value) || next.Value == '_'));

                    ExpressionToken keyword;
                    if (TryGetKeyword(beginIdentifier.Until(next.Location), out keyword))
                    {
                        yield return Result.Value(keyword, beginIdentifier, next.Location);
                    }
                    else
                    {
                        yield return Result.Value(ExpressionToken.Identifier, beginIdentifier, next.Location);
                    }
                }
                else if (next.Value == '/'
                    && (!state.Previous.HasValue || PreRegexTokens.Contains(state.Previous.Value.Kind)))
                {
                    var regex = ExpressionTextParsers.RegularExpression(next.Location);
                    if (!regex.HasValue)
                        yield return Result.CastEmpty<Unit, ExpressionToken>(regex);

                    yield return Result.Value(ExpressionToken.RegularExpression, next.Location, regex.Remainder);
                    next = regex.Remainder.ConsumeChar();
                }
                else
                {
                    var compoundOp = ExpressionTextParsers.CompoundOperator(next.Location);
                    if (compoundOp.HasValue)
                    {
                        yield return Result.Value(compoundOp.Value, compoundOp.Location, compoundOp.Remainder);
                        next = compoundOp.Remainder.ConsumeChar();
                    }
                    else if (next.Value < SimpleOps.Length && SimpleOps[next.Value] != ExpressionToken.None)
                    {
                        yield return Result.Value(SimpleOps[next.Value], next.Location, next.Remainder);
                        next = next.Remainder.ConsumeChar();
                    }
                    else
                    {
                        yield return Result.Empty<ExpressionToken>(next.Location);
                        next = next.Remainder.ConsumeChar();
                    }
                }

                next = SkipWhiteSpace(next.Location);
            } while (next.HasValue);
        }

        private static bool IsDelimiter(Result<char> next)
        {
            return !next.HasValue ||
                   char.IsWhiteSpace(next.Value) ||
                   next.Value < SimpleOps.Length && SimpleOps[next.Value] != ExpressionToken.None;
        }

        private static bool TryGetKeyword(TextSpan span, out ExpressionToken keyword)
        {
            foreach (var kw in Keywords)
            {
                if (span.EqualsValueIgnoreCase(kw.Text))
                {
                    keyword = kw.Token;
                    return true;
                }
            }

            keyword = ExpressionToken.None;
            return false;
        }

        public static ExpressionTokenizer Instance => new ExpressionTokenizer();
    }
}
