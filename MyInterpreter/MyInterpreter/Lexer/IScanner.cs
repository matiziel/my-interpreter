using MyInterpreter.Lexer.Tokens;

namespace MyInterpreter.Lexer
{
    public interface IScanner
    {
        Token CurrentToken { get; }
        void Next();
    }
}