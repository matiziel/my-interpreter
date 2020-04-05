namespace MyInterpreter.Lexer
{
    public class Word : Token
    {
        public string Value { get; private set; }
        public Word(TokenType type, string value) : base(type) => Value = value; 
        
        public override string ToString() => Value;
    }
}