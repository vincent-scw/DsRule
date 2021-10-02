using System;

namespace DsLinq.Tokenizer
{
    struct ExpressionKeyword
    {
        public string Text { get; set; }
        public ExpressionToken Token { get; set; }

        public ExpressionKeyword(string text, ExpressionToken token)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Token = token;
        }
    }
}
