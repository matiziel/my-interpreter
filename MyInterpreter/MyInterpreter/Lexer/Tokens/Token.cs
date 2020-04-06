namespace MyInterpreter.Lexer.Tokens
{
    public enum TokenType
    {
        NUMBER,
        WHILE,
        FOR,
        IF,
        ELSE,
        IDENTIFIER
    }
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public Token(TokenType type) => Type = type;
    }
}