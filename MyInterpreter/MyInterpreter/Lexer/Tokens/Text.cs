using MyInterpreter.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Text : Token
    {
        public string Value { get; private set; }
        public Text(string value, TextPosition position) : base(TokenType.STRING)
        {
            Value = value;
            Position = new TextPosition(position, Value.Length - 1);
        }
        public override string ToString() => Value;
    }
}