using MyInterpreter.Lexer.DataSource;

namespace MyInterpreter.Lexer.Tokens
{
    public class Keyword : Token
    {
        public Keyword(TokenType type, TextPosition position) : base(type) 
        { 
            int offset = Type.ToString().Length;
            Position = new TextPosition(
                position.Row,
                position.Column - offset,
                position.SourcePosition - offset
            );
        }

        public override string ToString() => Type.ToString().ToLower(); 
    }
}