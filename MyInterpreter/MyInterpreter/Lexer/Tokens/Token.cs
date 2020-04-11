using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public Position Position { get; private set; }
        protected Token(TokenType type) => Type = type;
        //TODO
        //position in source 
        // position in text
        //toString extensions method?
    }
}


