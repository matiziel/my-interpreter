using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public TextPosition Position { get; private set; }
        protected Token(TokenType type, TextPosition position)
        {
            Type = type;
            Position = new TextPosition(position);
        }
    }
}


