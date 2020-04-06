namespace MyInterpreter.Lexer.Tokens
{
    public class Keyword : Token
    {
        public Keyword(TokenType type) : base(type) { }

        public override string ToString() => Type.ToString().ToLower(); 
    }
}