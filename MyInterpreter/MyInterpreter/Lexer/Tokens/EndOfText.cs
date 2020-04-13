namespace MyInterpreter.Lexer.Tokens
{
    public class EndOfText : Token
    {
        public EndOfText() : base(TokenType.EOT) { }

        public override string ToString() => Type.ToString();

    }
}