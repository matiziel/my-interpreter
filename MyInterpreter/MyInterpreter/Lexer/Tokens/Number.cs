namespace MyInterpreter.Lexer.Tokens
{
    public class Number : Token
    {
        public uint Value { get; private set; }
        public Number(uint value) : base(TokenType.NUMBER) => Value = value;
        
        public override string ToString() => Value.ToString(); 
    }
}