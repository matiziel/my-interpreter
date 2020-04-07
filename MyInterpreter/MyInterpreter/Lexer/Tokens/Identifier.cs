namespace MyInterpreter.Lexer.Tokens
{
    public class Identifier : Token
    {
        public string Value { get; private set; }
        public Identifier(string value) : base (TokenType.IDENTIFIER) => Value = value; 
        
        public override string ToString() => Value;
    }
}