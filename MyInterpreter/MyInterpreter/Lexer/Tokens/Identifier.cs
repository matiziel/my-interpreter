using MyInterpreter.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Identifier : Token
    {
        public string Value { get; private set; }
        public Identifier(string value, TextPosition position) : base (TokenType.IDENTIFIER)
        {
            Value = value; 
            Position = new TextPosition(position, Value.Length);
        } 
        
        public override string ToString() => Value;
    }
}