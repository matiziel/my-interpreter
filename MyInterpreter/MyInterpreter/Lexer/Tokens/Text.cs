namespace MyInterpreter.Lexer.Tokens
{
    public class Text : Token
    {
        public string Value { get; private set; }
        public Text(string value) : base(TokenType.STRING) => Value = value;
        
        public override string ToString() => Value;
    }
}