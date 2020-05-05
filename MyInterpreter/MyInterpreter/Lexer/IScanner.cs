using MyInterpreter.Tokens;

namespace MyInterpreter.Lexer
{
    public interface IScanner
    {
        Token CurrentToken { get; }
        Token Next();
    }
}