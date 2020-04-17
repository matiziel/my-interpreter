using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class EndOfText : Token
    {
        public EndOfText(TextPosition position) : base(TokenType.EOT, position) { }

        public override string ToString() => Type.ToString();

    }
}