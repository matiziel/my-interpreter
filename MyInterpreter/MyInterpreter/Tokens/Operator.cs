using MyInterpreter.DataSource;

namespace MyInterpreter.Tokens
{
    public class Operator : Token
    {
        public string Value { get; private set; }
        public Operator(TokenType type, string value, TextPosition position) : base(type) 
        {
            Value = value; 
            Position = new TextPosition(position, Value.Length);

        }
        public override string ToString() => Value;
    }
}