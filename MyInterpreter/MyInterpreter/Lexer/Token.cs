namespace MyInterpreter.Lexer
{
    public enum TokenType
    {
        KEYWORD
    }
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public Token(TokenType type) => Type = type;
    }
}