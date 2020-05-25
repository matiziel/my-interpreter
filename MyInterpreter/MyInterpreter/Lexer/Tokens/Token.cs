using MyInterpreter.DataSource;

namespace MyInterpreter.Lexer.Tokens {
    public abstract class Token {
        public TokenType Type { get; private set; }
        public TextPosition Position { get; protected set; }
        protected Token(TokenType type) => Type = type;
    }
}


