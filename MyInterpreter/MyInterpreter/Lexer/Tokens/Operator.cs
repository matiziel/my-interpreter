using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Operator : Token
    {
        public string Value { get; private set; }
        public Operator(TokenType type, string value, TextPosition position) : base(type, position) => Value = value; 
        
        public override string ToString() => Value;
    }
}