namespace MyInterpreter.Lexer.Tokens
{
    public enum TokenType
    {
        NUMBER, STRING,
        WHILE, FOR, IF, ELSE, RETURN,
        IDENTIFIER,
        AND, OR, 
        PLUS, MINUS, MULTIPLY, DIVIDE, MODULO, ASSIGN,
        PLUS_ASSIGN, MINUS_ASSIGN, MULTIPLY_ASSIGN, DIVIDE_ASSIGN, MODULO_ASSIGN,
        EQUALS, NOT_EQUAL, NOT, GREATER, LESS, GREATER_EQUAL, LESS_EQUAL
    }
    public abstract class Token
    {
        public TokenType Type { get; private set; }
        protected Token(TokenType type) => Type = type;
        //TODO
        //position in source 
        // position in text
        //toString extensions method?

    }
}


