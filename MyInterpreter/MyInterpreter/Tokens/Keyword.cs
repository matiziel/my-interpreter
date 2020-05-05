using MyInterpreter.DataSource;

namespace MyInterpreter.Tokens
{
    public class Keyword : Token
    {
        public Keyword(TokenType type, TextPosition position) : base(type) =>
            Position = new TextPosition(position, Type.ToString().Length);
        
        public override string ToString() => Type.ToString().ToLower(); 
    }
}