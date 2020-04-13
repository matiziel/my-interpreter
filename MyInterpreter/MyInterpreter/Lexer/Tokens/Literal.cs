namespace MyInterpreter.Lexer.Tokens
{
    public class Literal : Token
    {
        public string Value { get; private set; }
        public Literal(TokenType type, string value) : base(type) => Value = value;
        
        public override string ToString() => Value;
    }
}