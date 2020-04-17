using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Keyword : Token
    {
        public Keyword(TokenType type, TextPosition position) : base(type, position) { }

        public override string ToString() => Type.ToString().ToLower(); 
    }
}