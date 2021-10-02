using DsLinq.Ast;
using DsLinq.Tokenizer;
using Superpower;
using Superpower.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsLinq
{
    class ExpressionTokenListParser
    {
        private static readonly TokenListParser<ExpressionToken, Operators> Add =
            Token.EqualTo(ExpressionToken.Plus).Value(Operators.Add);

        private static readonly TokenListParser<ExpressionToken, Operators> Subtract =
            Token.EqualTo(ExpressionToken.Minus).Value(Operators.Substract);

        private static readonly TokenListParser<ExpressionToken, Operators> Multiply =
            Token.EqualTo(ExpressionToken.Asterisk).Value(Operators.Multiply);

        private static readonly TokenListParser<ExpressionToken, Operators> Divide =
            Token.EqualTo(ExpressionToken.ForwardSlash).Value(Operators.Divide);

        private static readonly TokenListParser<ExpressionToken, DslExpression> Number =
            Token.EqualTo(ExpressionToken.Number)
            .Apply(Numerics.IntegerInt32)
            .Select(n => (DslExpression)new ConstantExpr(n));
    }
}
