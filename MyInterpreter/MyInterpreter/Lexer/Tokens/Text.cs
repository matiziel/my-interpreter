using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Text : Token
    {
        public string Value { get; private set; }
        public Text(string value, TextPosition position) : base(TokenType.STRING)
        {
            Value = value;
            Position = new TextPosition(
                position.Row,
                position.Column - Value.Length - 1,
                position.SourcePosition - Value.Length - 1
            );
        }
        public override string ToString() => Value;
    }
}