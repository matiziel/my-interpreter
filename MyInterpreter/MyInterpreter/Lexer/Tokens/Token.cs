namespace MyInterpreter.Lexer.Tokens
{
    public enum TokenType
    {
        NUMBER,
        WHILE, FOR, IF, ELSE,
        RETURN,
        IDENTIFIER,
        AND, OR, 
        PLUS, MINUS, MULTIPLY, DIVIDE, MODULO, ASSIGN,
        PLUS_ASSIGN, MINUS_ASSIGN, MULTIPLY_ASSIGN, DIVIDE_ASSIGN, MODULO_ASSIGN,
        EQUALS, NOT_EQUAL, NOT, GREATER, LESS, GREATER_EQUAL, LESS_EQUAL
    }
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        public Token(TokenType type) => Type = type;
    }
}