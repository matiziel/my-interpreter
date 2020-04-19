using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Operator : Token
    {
        public string Value { get; private set; }
        public Operator(TokenType type, string value, TextPosition position) : base(type) 
        {
            Value = value; 
            Position = new TextPosition(
                position.Row,
                position.Column - Value.Length,
                position.SourcePosition - Value.Length
            );
        }
        public override string ToString() => Value;
    }
}