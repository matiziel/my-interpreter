using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Number : Token
    {
        public int Value { get; private set; }
        public Number(int value, TextPosition position) : base(TokenType.NUMBER, position) => Value = value;
        
        public override string ToString() => Value.ToString(); 
    }
}