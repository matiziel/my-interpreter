using MyInterpreter.DataSource;

namespace MyInterpreter.Tokens
{
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public TextPosition Position { get; protected set; }
        protected Token(TokenType type) => Type = type;
    }
}


