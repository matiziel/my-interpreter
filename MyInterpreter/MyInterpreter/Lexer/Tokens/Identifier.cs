using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Identifier : Token
    {
        public string Value { get; private set; }
        public Identifier(string value, TextPosition position) : base (TokenType.IDENTIFIER, position) => Value = value; 
        
        public override string ToString() => Value;
    }
}