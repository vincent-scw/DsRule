using DsLinq.Tokenizer;
using System.Linq;
using Xunit;

namespace DsLinq.UnitTest
{
    public class TokenizerTests
    {
        private readonly ExpressionTokenizer _tokenizer;

        public TokenizerTests()
        {
            _tokenizer = new ExpressionTokenizer();
        }

        [Fact]
        public void TokenizeNumber_ShouldReturnExpectedTokens()
        {
            var tokens = _tokenizer.Tokenize("123 456").ToList();

            Assert.Equal(2, tokens.Count);

            Assert.Equal(ExpressionToken.Number, tokens[0].Kind);
            Assert.Equal("123", tokens[0].ToStringValue());

            Assert.Equal(ExpressionToken.Number, tokens[1].Kind);
            Assert.Equal("456", tokens[1].ToStringValue());
        }
    }
}
