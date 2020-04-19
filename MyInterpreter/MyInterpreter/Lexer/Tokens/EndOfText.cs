using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class EndOfText : Token
    {
        public EndOfText(TextPosition position) : base(TokenType.EOT) 
            => Position = new TextPosition(position);

        public override string ToString() => Type.ToString();

    }
}