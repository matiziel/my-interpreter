using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Text : Token
    {
        public string Value { get; private set; }
        public Text(string value, TextPosition position) : base(TokenType.STRING, position) => Value = value;
        
        public override string ToString() => Value;
    }
}