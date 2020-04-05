namespace MyInterpreter.Lexer
{
    public enum TokenType
    {
        NUMBER,
        IDENTIFIER,
        WHILE, IF, ELSE, FOR

    }
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public Token(TokenType type) => Type = type;
    }
}