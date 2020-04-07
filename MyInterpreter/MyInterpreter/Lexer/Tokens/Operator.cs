namespace MyInterpreter.Lexer.Tokens
{
    public class Operator : Token
    {
        public string Value { get; private set; }
        public Operator(TokenType type, string value) : base (type) => Value = value; 
        
        public override string ToString() => Value;
    }
}