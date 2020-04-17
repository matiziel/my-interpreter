using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Literal : Token
    {
        public string Value { get; private set; }
        public Literal(TokenType type, string value, TextPosition position) : base(type, position) => Value = value;
        
        public override string ToString() => Value;
    }
}