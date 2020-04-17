using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Number : Token
    {
        public uint Value { get; private set; }
        public Number(uint value, TextPosition position) : base(TokenType.NUMBER, position) => Value = value;
        
        public override string ToString() => Value.ToString(); 
    }
}